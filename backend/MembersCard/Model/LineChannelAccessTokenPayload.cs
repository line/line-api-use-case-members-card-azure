using Newtonsoft.Json;

namespace MembersCard.Model
{
    /// <summary>
    /// LINEチャネルアクセストークン取得APIのレスポンスパース用
    /// </summary>
    public class LineChannelAccessTokenPayload
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("expires_in")]
        public long ExpiresIn { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }
    }
}
