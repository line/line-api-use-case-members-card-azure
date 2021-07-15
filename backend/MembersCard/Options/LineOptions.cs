namespace MembersCard.Options
{
    /// <summary>
    /// LINEチャネルに関する設定
    /// </summary>
    public class LineOptions
    {
        /// <summary>
        /// LINEチャネルID
        /// </summary>
        public string ChannelId { get; set; }

        /// <summary>
        /// LINEログインチャネルID
        /// </summary>
        public string LiffChannelId { get; set; }

        /// <summary>
        /// LINEチャネルシークレット
        /// </summary>
        public string ChannelSecret { get; set; }

        /// <summary>
        /// LIFF Id
        /// </summary>
        public string LiffId { get; set; }
    }
}
