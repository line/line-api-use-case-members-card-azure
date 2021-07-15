using System.Threading.Tasks;
using MembersCard.Entities;

namespace MembersCard.Interfaces
{
    /// <summary>
    /// 商品情報に関するサービスのインターフェース
    /// </summary>
    public interface IProductInfoService
    {
        /// <summary>
        /// productIdから商品情報を取得する
        /// </summary>
        /// <param name="productId"></param>
        /// <returns>Product</returns>
        Task<Product> GetItemAsync(int productId);

        /// <summary>
        /// アイテム数を取得する
        /// </summary>
        /// <returns></returns>
        Task<int> GetTableSizeAsync();
    }
}
