namespace System
{
    /// <summary>
    /// DateTimeの拡張機能
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// JST化する
        /// </summary>
        /// <param name="d">JST化する元のDateTimeオブジェクト</param>
        /// <returns>JST化したDateTimeオブジェクト</returns>
        public static DateTime ToJst(this DateTime d)
            => TimeZoneInfo.ConvertTime(d.ToUniversalTime(), GetJstTimeZoneInfo());

        /// <summary>
        /// JSTのTimeZoneInfoを取得
        /// </summary>
        /// <returns>JSTのTimeZoneInfo</returns>
        private static TimeZoneInfo GetJstTimeZoneInfo()
        {
            try
            {
                return TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time");
            }
            catch
            {
                return TimeZoneInfo.FindSystemTimeZoneById("Asia/Tokyo");
            }
        }
    }
}
