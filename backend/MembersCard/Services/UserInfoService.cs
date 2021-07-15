using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MembersCard.Interfaces;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.Extensions.Logging;
using User = MembersCard.Entities.User;

namespace MembersCard.Services
{
    public class UserInfoService : CosmosDbService, IUserInfoService
    {
        protected override string ContainerName => "user";
        protected override string PartitionKeyName => "/userId";
        protected override int DefaultTimeToLive => 60 * 60 * 24;

        public UserInfoService(
            CosmosClient client,
            ILogger log) : base(client)
        {
        }

        public async Task<User> CreateNewUserAsync(string userId)
        {
            var barCodeNum = await CreateBarCodeNum();

            var expirationDate = "";
            var point = 0;
            var item = new User()
            {
                Id = Guid.NewGuid().ToString(),
                UserId = userId,
                BarcodeNum = barCodeNum,
                PointExpirationDate = expirationDate,
                Point = point,
                Ttl = DefaultTimeToLive
            };
            await PutAsync(item);

            return item;
        }

        public async Task<User> GetAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return null;
            }

            User user;
            var container = await GetContainerAsync();
            using (var setIterator = container.GetItemLinqQueryable<User>()
                .Where(i => i.UserId == userId)
                .ToFeedIterator())
            {
                var result = await setIterator.ReadNextAsync();
                user = result?.FirstOrDefault();
            }

            return user;
        }

        public async Task PutAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException();
            }

            var container = await GetTtlContainerAsync();
            var result = await container.UpsertItemAsync(user, new PartitionKey(user.UserId));

            if (result == null)
            {
                throw new Exception("UserInfo Put Cosmos Error.");
            }

            var statusCode = (int) result.StatusCode;
            if (statusCode >= 400)
            {
                throw new Exception("UserInfo Put Error.");
            }
        }

        public async Task UpdatePointExpirationDateAsync(string userId, int point, string expirationDate)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentNullException();
            }

            var target = await GetAsync(userId);
            target.Point = point;
            target.PointExpirationDate = expirationDate;

            await PutAsync(target);
        }

        public async Task<List<long>> QueryIndexBarcodeNum(long barcodeNum)
        {
            List<long> list = new List<long>();
            var container = await GetContainerAsync();
            using (var setIterator = container.GetItemLinqQueryable<User>()
                .Where(i => i.BarcodeNum == barcodeNum)
                .ToFeedIterator())
            {
                var result = await setIterator.ReadNextAsync();
                if (result.Any()) list.Add(result.FirstOrDefault().BarcodeNum);
            }

            return list;
        }

        private async Task<Container> GetTtlContainerAsync()
        {
            var container = await base.GetContainerAsync();
            var ctx = await container.ReadContainerAsync();

            if (ctx.Resource.DefaultTimeToLive == DefaultTimeToLive) return container;

            ctx.Resource.DefaultTimeToLive = DefaultTimeToLive;
            return await client.GetContainer(container.Database.Id, ContainerName).ReplaceContainerAsync(ctx.Resource);

        }

        private async Task<long> CreateBarCodeNum()
        {
            var random = new Random();
            var barCodeNum = random.NextLong((long) Math.Pow(10, 12), (long) Math.Pow(10, 13));
            var items = await QueryIndexBarcodeNum(barCodeNum);

            // バーコードが重複した場合、１回までリトライしバーコード生成を行う。
            if (items.Any())
                return random.NextLong((long) Math.Pow(10, 12), (long) Math.Pow(10, 13));

            return barCodeNum;
        }
    }
}
