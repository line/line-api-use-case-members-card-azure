using System.Threading.Tasks;
using MembersCard.Entities;
using MembersCard.Interfaces;
using Microsoft.Azure.Cosmos;
using System.Linq;
using Microsoft.Azure.Cosmos.Linq;

namespace MembersCard.Services
{
    /// <summary>
    /// IProductInfoServiceの実装クラス
    /// </summary>
    public class ProductInfoService : CosmosDbService, IProductInfoService
    {
        protected override string ContainerName => "product";
        protected override string PartitionKeyName => "/productId";

        public ProductInfoService(CosmosClient client) : base(client)
        {
        }
        public async Task<Product> GetItemAsync(int productId)
        {
            Product product;
            var container = await GetContainerAsync();
            using (var setIterator = container.GetItemLinqQueryable<Product>()
                .Where(i => i.ProductId == productId)
                .ToFeedIterator())
            {
                var result = await setIterator.ReadNextAsync();
                product = result?.FirstOrDefault();
            }

            return product;
        }

        public async Task<int> GetTableSizeAsync()
        {
            var container = await GetContainerAsync();

            var count = await container.GetItemLinqQueryable<Product>().CountAsync();

            return count;
        }
    }
}
