using System.Text.Json.Serialization;

namespace MembersCard.Entities
{
    /// <summary>
    /// ユーザー
    /// </summary>
    public class User : BaseEntity
    {
        public string UserId { get; set; }
        public long BarcodeNum { get; set; }
        public string PointExpirationDate { get; set; }
        public int Point { get; set; }
        public int? Ttl { get; set; }
    }
}
