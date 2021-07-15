using System;
using MembersCard;
using MembersCard.Interfaces;
using MembersCard.Options;
using MembersCard.Services;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]

namespace MembersCard
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            // 環境変数の設定
            builder.Services.AddOptions<LineOptions>()
                .Configure<IConfiguration>((settings, configuration) =>
                {
                    settings.ChannelId = configuration.GetValue<string>("LineChannelId");
                    settings.LiffChannelId = configuration.GetValue<string>("LiffChannelId");
                    settings.ChannelSecret = configuration.GetValue<string>("LineChannelSecret");
                    settings.LiffId = configuration.GetValue<string>("LineLiffId");
                });

            // インターフェースの設定
            builder.Services.AddScoped<IUserInfoService, UserInfoService>();
            builder.Services.AddScoped<IProductInfoService, ProductInfoService>();
            builder.Services.AddScoped<ILineService, LineService>();
            builder.Services.AddScoped<ILineChannelService, LineChannelService>();

            // Cosmosの設定
            builder.Services.AddSingleton(services =>
            {
                var configuration = services.GetService<IConfiguration>() ??
                                    throw new ArgumentNullException("configuration");
                var account = configuration.GetValue<string>("CosmosDbAccount");
                var key = configuration.GetValue<string>("CosmosDbKey");
                var connectionString = $"AccountEndpoint={account};AccountKey={key};";
                return new CosmosClient(connectionString, new CosmosClientOptions
                {
                    SerializerOptions = new CosmosSerializationOptions
                    {
                        IgnoreNullValues = true,
                        Indented = false,
                        PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
                    }
                });
            });

            // HttpClientの設定
            builder.Services.AddHttpClient("line", c => c.BaseAddress = new Uri("https://api.line.me"));
        }
    }
}
