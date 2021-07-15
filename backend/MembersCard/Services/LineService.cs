using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MembersCard.Entities;
using MembersCard.Interfaces;
using MembersCard.Model;
using MembersCard.Options;
using Microsoft.Extensions.Options;

namespace MembersCard.Services
{
    public class LineService : ILineService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly LineOptions _lineOptions;

        public LineService(
            IHttpClientFactory httpClientFactory,
            IOptionsMonitor<LineOptions> lineOptions)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _lineOptions = lineOptions?.CurrentValue ?? throw new ArgumentNullException(nameof(lineOptions));
        }

        public async Task<LineIdTokenPayload> VerifyIdTokenAsync(string idToken)
        {
            if (string.IsNullOrWhiteSpace(idToken))
            {
                return null;
            }

            // https://developers.line.biz/ja/reference/line-login/#verify-id-token
            var client = _httpClientFactory.CreateClient("line");
            var dict = new Dictionary<string, string>();
            dict.Add("id_token", idToken);
            dict.Add("client_id", _lineOptions.LiffChannelId);
            var response = await client.PostAsync("/oauth2/v2.1/verify", new FormUrlEncodedContent(dict));
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return await response.Content.ReadAsAsync<LineIdTokenPayload>();
        }

        public async Task SendPushMessage(string channelAccessToken, string userId, Product product, string language)
        {
            // サービスメッセ―ジで代引き手数料と支払手数料を表示させないため以下を0にする
            var obj = new ModifiedProduct(product, language);
            var flaxMassage = new FlexMessagePayload(obj, language, _lineOptions.LiffId);
            var json = JsonSerializer.Serialize(new
            {
                to = userId,
                messages = new[] { flaxMassage },
            }, new JsonSerializerOptions { IgnoreNullValues = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            var client = _httpClientFactory.CreateClient("line");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", channelAccessToken);
            await client.PostAsync("/v2/bot/message/push", new StringContent(json, Encoding.UTF8, "application/json"));

        }

        public async Task<string> GetChannelAccessTokenAsync(string channelId, string channelSecret)
        {
            if (string.IsNullOrWhiteSpace(channelId) || string.IsNullOrWhiteSpace(channelSecret))
            {
                return null;
            }

            var client = _httpClientFactory.CreateClient("line");
            var formData = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "grant_type", "client_credentials" },
                { "client_id", channelId },
                { "client_secret", channelSecret },
            });
            var response = await client.PostAsync("/v2/oauth/accessToken", formData);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var payload = await response.Content.ReadAsAsync<LineChannelAccessTokenPayload>();
            return payload?.AccessToken;
        }
    }
}
