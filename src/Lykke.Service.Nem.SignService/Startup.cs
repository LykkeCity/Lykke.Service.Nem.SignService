using System;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using Lykke.Logs.Loggers.LykkeSlack;
using Lykke.Sdk;
using Lykke.Sdk.Health;
using Lykke.Sdk.Middleware;
using Lykke.Service.BlockchainApi.Sdk;
using Lykke.Service.Nem.SignService.Settings;
using Lykke.Service.Nem.SignService.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Lykke.Service.Nem.SignService
{
    [UsedImplicitly]
    public class Startup
    {
        private readonly LykkeSwaggerOptions _swaggerOptions = new LykkeSwaggerOptions
        {
            ApiTitle = "NemSignService API",
            ApiVersion = "v1"
        };

        [UsedImplicitly]
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            return services.BuildServiceProvider<AppSettings>(options =>
            {
                options.SwaggerOptions = _swaggerOptions;

                options.Logs = logs =>
                {
                    logs.AzureTableName = "NemSignServiceLog";
                    logs.AzureTableConnectionStringResolver = settings => settings.NemSignService.Db.LogsConnString;
                };

                options.Extend = (sc, settings) =>
                {
                    sc.AddBlockchainSignService(_ => new NemSignService(settings.CurrentValue.NemSignService.HotWalletAddress));
                };
            });
        }

        [UsedImplicitly]
        public void Configure(IApplicationBuilder app)
        {
            app.UseLykkeConfiguration(options =>
            {
                options.SwaggerOptions = _swaggerOptions;
            });
        }
    }
}
