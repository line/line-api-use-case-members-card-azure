using System;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;

namespace MembersCard.Services
{
    public abstract class CosmosDbService
    {
        private readonly string databaseName = "LineApiUseCaseMembersCard";
        protected readonly CosmosClient client;

        /// <summary>
        /// コンテナ名
        /// </summary>
        protected virtual string ContainerName { get; }

        /// <summary>
        /// パーティションキー名
        /// </summary>
        protected virtual string PartitionKeyName { get; }

        /// <summary>
        ///  Time to Live（秒）
        /// </summary>
        protected virtual int DefaultTimeToLive => -1;

        protected CosmosDbService(CosmosClient client)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
        }

        /// <summary>
        /// コンテナを取得
        /// </summary>
        /// <returns>各リポジトリクラスで参照対象のコンテナ</returns>
        protected async Task<Container> GetContainerAsync()
        {
            Database database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
            Container container = await database.CreateContainerIfNotExistsAsync(new ContainerProperties
            {
                Id = ContainerName,
                PartitionKeyPath = PartitionKeyName,
                DefaultTimeToLive = DefaultTimeToLive
            });
            return container;
        }
    }
}
