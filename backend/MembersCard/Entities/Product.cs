using System.Collections.Generic;

namespace MembersCard.Entities
{
    /// <summary>
    /// 商品情報
    /// </summary>
    public class Product : BaseEntity
    {
        public double Fee { get; set; }
        public string ImgUrl { get; set; }
        public double Postage { get; set; }
        public int ProductId { get; set; }
        public Dictionary<string, string> productName { get; set; }
        public double UnitPrice { get; set; }

        public double SubTotal(double discount)
        {
            return UnitPrice + Postage + Fee - discount;
        }
    }
}
