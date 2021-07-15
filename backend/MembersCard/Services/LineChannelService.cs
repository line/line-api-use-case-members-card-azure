using System;
using System.Linq;
using System.Threading.Tasks;
using MembersCard.Entities;
using MembersCard.Interfaces;
using MembersCard.Options;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.Extensions.Options;

namespace MembersCard.Services
{
    public class LineChannelService : CosmosDbService, ILineChannelService
    {
        protected override string ContainerName => "lineChannel";
        protected override string PartitionKeyName => "/channelId";

        private readonly LineOptions _lineOptions;

        public LineChannelService(
            CosmosClient client,
            IOptionsMonitor<LineOptions> lineOptions)
            : base(client)
        {
            _lineOptions = lineOptions.CurrentValue ?? throw new ArgumentNullException(nameof(lineOptions));
        }

        public async Task<LineChannel> GetAsync()
        {
            LineChannel lineChannel;
            var container = await GetContainerAsync();
            using (var setIterator = container.GetItemLinqQueryable<LineChannel>()
                .Where(l => l.ChannelId == _lineOptions.ChannelId)
                .ToFeedIterator())
            {
                var result = await setIterator.ReadNextAsync();
                lineChannel = result?.FirstOrDefault();
            }

            return lineChannel;
        }

        public async Task<LineChannel> UpsertAsync(LineChannel lineChannel)
        {
            if (lineChannel == null)
            {
                return null;
            }

            var container = await GetContainerAsync();
            var itemResponse = await container.UpsertItemAsync(lineChannel, new PartitionKey(lineChannel.ChannelId));

            return itemResponse;
        }
    }
}
