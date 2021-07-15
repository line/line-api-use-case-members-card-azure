using System.Collections.Generic;

namespace MembersCard.Common
{
    public class CommonConst
    {
        public static readonly int DATA_LIMIT_TIME = 60 * 60 * 12;

        public static readonly Dictionary<string, string> MESSAGE_ALT_TEXT = new Dictionary<string, string>()
        {
            {"ja", "お買い上げありがとうございます。電子レシートを発行します。"}
        };

        public static readonly Dictionary<string, string> MESSAGE_NOTES = new Dictionary<string, string>()
        {
            {"ja", "※LINE API Use Caseサイトのデモアプリケーションであるため、実際の課金は行われません"}
        };

        public static readonly Dictionary<string, string> MESSAGE_POSTAGE = new Dictionary<string, string>()
        {
            {"ja", "送料（税抜）"}
        };

        public static readonly Dictionary<string, string> MESSAGE_FEE = new Dictionary<string, string>()
        {
            {"ja", "決算手数料（税抜）"}
        };

        public static readonly Dictionary<string, string> MESSAGE_DISCOUNT = new Dictionary<string, string>()
        {
            {"ja", "値引き"}
        };

        public static readonly Dictionary<string, string> MESSAGE_SUBTOTAL = new Dictionary<string, string>()
        {
            {"ja", "小計（税抜）"}
        };

        public static readonly Dictionary<string, string> MESSAGE_TAX = new Dictionary<string, string>()
        {
            {"ja", "消費税"}
        };

        public static readonly Dictionary<string, string> MESSAGE_TOTAL = new Dictionary<string, string>()
        {
            {"ja", "お会計金額"}
        };

        public static readonly Dictionary<string, string> MESSAGE_AWARD_POINTS = new Dictionary<string, string>()
        {
            {"ja", "付与ポイント"}
        };

        public static readonly Dictionary<string, string> MESSAGE_THANKS = new Dictionary<string, string>()
        {
            {"ja", "商品のご購入ありがとうございます。\n本メッセージは、Use Case STOREおよびUse Case GROUPの店舗で商品をご購入されたお客様にお届けしています。"}
        };

        public static readonly Dictionary<string, string> MESSAGE_VIEW = new Dictionary<string, string>()
        {
            {"ja", "会員証を表示"}
        };
    }
}
