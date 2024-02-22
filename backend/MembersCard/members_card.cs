using System;
using System.Threading.Tasks;
using System.Web.Http;
using MembersCard.Entities;
using MembersCard.Interfaces;
using MembersCard.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace MembersCard
{
    public class MembersCard
    {
        private readonly ILineService _lineService;
        private readonly ILineChannelService _lineChannelService;
        private readonly IUserInfoService _userInfoService;
        private readonly IProductInfoService _productInfoService;

        public MembersCard(
            ILineService lineService,
            ILineChannelService lineChannelService,
            IUserInfoService userInfoService,
            IProductInfoService productInfoService)
        {
            _lineService = lineService ?? throw new ArgumentNullException(nameof(lineService));
            _lineChannelService = lineChannelService ?? throw new ArgumentNullException(nameof(lineChannelService));
            _userInfoService = userInfoService ?? throw new ArgumentNullException(nameof(userInfoService));
            _productInfoService = productInfoService ?? throw new ArgumentNullException(nameof(productInfoService));
        }
        [FunctionName("members_card")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "members_card")]
            MembersCardRequest req,
            ILogger log)
        {
            log.LogInformation("start");

            if (string.IsNullOrWhiteSpace(req.Mode))
            {
                log.LogError("リクエストパラメータが空です");
                return new BadRequestErrorMessageResult($"必須入力エラー: {req.Mode}");
            }

            var idToken = req.IdToken;
            var payload = await _lineService.VerifyIdTokenAsync(idToken);
            if (payload == null)
            {
                log.LogWarning("idToken is invalid.");
                return new UnauthorizedResult();
            }

            var userId = payload.Sub;
            var mode = req.Mode;

            User result;

            try
            {
                if (mode == "init")
                {
                    result = await _userInfoService.CreateNewUserAsync(userId);
                }
                else if(mode == "buy")
                {
                    // LINEチャネルアクセストークンの取得
                    var lineChannel = await _lineChannelService.GetAsync();
                    if (lineChannel == null)
                    {
                        log.LogError("LINEチャネルアクセストークンがDBに登録されていません");
                        return new BadRequestObjectResult("LINEメッセージが送信できないため、購入を中断しました");
                    }
                    else if (lineChannel.IsExpired())
                    {
                        // 新しいチャネルアクセストークンを取得
                        var channelAccessToken = await _lineService.GetChannelAccessTokenAsync(lineChannel.ChannelId, lineChannel.ChannelSecret);
                        if (string.IsNullOrWhiteSpace(channelAccessToken))
                        {
                            log.LogError("新しいチャネルアクセストークンの取得に失敗しました|channelId={channelId}", lineChannel.ChannelId);
                            return new BadRequestObjectResult("LINEメッセージが送信できないため、購入を中断しました");
                        }

                        // チャネルアクセストークンを更新
                        lineChannel.SetNewChannelAccessToken(channelAccessToken);
                        var upsertResult = await _lineChannelService.UpsertAsync(lineChannel);
                        if (upsertResult == null)
                        {
                            // ログのみ出力し、処理は続行する(トークンは取得できているため)
                            log.LogWarning("チャネルアクセストークンのデータ更新に失敗しました|channelId={channelId}", lineChannel.ChannelId);
                        }
                    }

                    result = await Buy(userId, req.Language, lineChannel.ChannelAccessToken);
                }
                else
                {
                    return new BadRequestErrorMessageResult($"Error mode value: {mode}");
                }
            }
            catch (Exception e)
            {
                log.LogError(e.Message);
                return new InternalServerErrorResult();
            }

            return new OkObjectResult(result);
        }

        private async Task<User> Buy(string userId, string language, string channelAccessToken)
        {
            // 購入商品のランダム取得
            var size = await _productInfoService.GetTableSizeAsync();
            var rand = new Random();
            // NOTE:サンプル商品を増やさないと決まった場合、tableSize部分に直接数値を入れた方が良い(パフォーマンス向上)
            var productId = rand.Next(1, size);
            var productInfo = await _productInfoService.GetItemAsync(productId);

            //付与ポイントの取得
            var userInfo = await _userInfoService.GetAsync(userId);
            var beforeAwardedPoint = userInfo.Point;
            var addPoint = Math.Floor((decimal)productInfo.UnitPrice * new decimal(0.05));
            var afterAwardedPoint = beforeAwardedPoint + (int) addPoint;

            // 更新期限日の取得
            var today = DateTime.UtcNow.ToJst();
            var expirationDate = today.AddYears(1).ToString("yyyy/MM/dd");

            // 更新
            await _userInfoService.UpdatePointExpirationDateAsync(userId, afterAwardedPoint, expirationDate);

            userInfo.PointExpirationDate = expirationDate;
            userInfo.Point = afterAwardedPoint;

            // メッセージ送信
            await _lineService.SendPushMessage(channelAccessToken, userId, productInfo, language);

            return userInfo;
        }
    }
}
