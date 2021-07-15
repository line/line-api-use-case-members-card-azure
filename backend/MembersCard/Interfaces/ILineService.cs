using System.Threading.Tasks;
using MembersCard.Entities;
using MembersCard.Model;

namespace MembersCard.Interfaces
{
    /// <summary>
    /// LINEに関するサービスのインターフェース
    /// </summary>
    public interface ILineService
    {
        /// <summary>
        /// IDトークン検証APIの実行
        /// </summary>
        /// <param name="idToken">IDトークン</param>
        /// <returns>IDトークン検証APIのレスポンス（IDトークンのペイロード）</returns>
        Task<LineIdTokenPayload> VerifyIdTokenAsync(string idToken);

        /// <summary>
        /// LineMessageの送信
        /// </summary>
        /// <param name="channelAccessToken">チャンネルアクセストークン</param>
        /// <param name="userId">LineのユーザーID</param>
        /// <param name="product">商品情報</param>
        /// <param name="language">表示言語</param>
        /// <returns></returns>
        Task SendPushMessage(string channelAccessToken, string userId, Product product, string language);

        /// <summary>
        /// チャネルアクセストークン発行APIの実行
        /// </summary>
        /// <param name="channelId">チャネルID</param>
        /// <param name="channelSecret">チャネルシークレット</param>
        /// <returns>チャネルアクセストークン</returns>
        Task<string> GetChannelAccessTokenAsync(string channelId, string channelSecret);
    }
}
