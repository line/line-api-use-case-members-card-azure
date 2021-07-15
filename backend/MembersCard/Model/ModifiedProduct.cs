using System;
using System.Text.Json.Serialization;
using MembersCard.Entities;

namespace MembersCard.Model
{
    public class ModifiedProduct
    {
        public string Date { get; set; }
        [JsonPropertyName("product_name")]
        public string ProductName { get; set; }
        [JsonPropertyName("product_price")]
        public string ProductPrice { get; set; }
        public string Postage { get; set; }
        public string Fee { get; set; }
        public string Discount { get; set; }
        public string Subtotal { get; set; }
        public string Tax { get; set; }
        public string Total { get; set; }
        public string Point { get; set; }
        [JsonPropertyName("img_url")]
        public string ImgUrl { get; set; }

        public ModifiedProduct(Product product, string language, double discount = 0)
        {
            var subtotal = product.SubTotal(discount);
            var tax = CalculateTax(subtotal);
            var point = CalculatePoint(product.UnitPrice);

            Date = DateTime.Now.ToJst().ToString("yyyy/MM/dd HH:mm:ss");
            ProductName = product.productName[language];
            ProductPrice = SeparateComma(product.UnitPrice);
            Postage = SeparateComma(product.Postage);
            Fee = SeparateComma(product.Fee);
            Discount = SeparateComma(discount);
            Subtotal = SeparateComma(subtotal);
            Tax = SeparateComma(tax);
            Total = SeparateComma(subtotal + tax);
            Point = SeparateComma(point);
            ImgUrl = product.ImgUrl;
        }

        private int CalculatePoint(double unitPrice)
        {
            return (int) Math.Floor((decimal)unitPrice * new decimal(0.05));
        }
        private int CalculateTax(double subtotal)
        {
            return (int) Math.Floor((decimal)subtotal * new decimal(0.1));
        }
        private string SeparateComma(int num)
        {
            return num.ToString("N0");
        }

        private string SeparateComma(double num)
        {
            return num.ToString("N0");;
        }
    }
}
