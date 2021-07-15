using System;

namespace MembersCard.Entities
{
    /// <summary>
    /// LINEチャネル
    /// </summary>
    /// <remarks>チャネルアクセストークンの更新が必要なため、データストアに保持</remarks>
    public class LineChannel : BaseEntity
    {
        private static int LINE_CHANNEL_ACCESS_TOKEN_LIMIT_DAYS = 20; // 取得から20日を更新期限とする

        public string ChannelId { get; set; }
        public string ChannelSecret { get; set; }
        public string ChannelAccessToken { get; set; }
        public DateTimeOffset LimitDate { get; set; }
        public DateTimeOffset UpdatedTime { get; set; }

        /// <summary>
        /// 期限切れ判定
        /// </summary>
        /// <returns>期限切れかどうか</returns>
        public bool IsExpired() => LimitDate < DateTimeOffset.UtcNow;

        /// <summary>
        /// 新しいチャネルアクセストークンをセットし、期限を更新する
        /// </summary>
        /// <param name="channelAccessToken">新しいチャネルアクセストークン</param>
        public void SetNewChannelAccessToken(string channelAccessToken)
        {
            ChannelAccessToken = channelAccessToken;
            var now = DateTimeOffset.UtcNow;
            UpdatedTime = now;
            LimitDate = now.AddDays(LINE_CHANNEL_ACCESS_TOKEN_LIMIT_DAYS);
        }
    }
}
