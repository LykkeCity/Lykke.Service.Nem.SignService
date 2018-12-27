using System;
using JetBrains.Annotations;
using Lykke.Sdk;
using Lykke.Service.BlockchainApi.Sdk;
using Lykke.Service.Nem.SignService.Services;
using Lykke.Service.Nem.SignService.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Lykke.Service.Nem.SignService
{
    [UsedImplicitly]
    public class Startup
    {
        private readonly LykkeSwaggerOptions _swaggerOptions = new LykkeSwaggerOptions
        {
            ApiTitle = "Nem Sign Service",
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
                    logs.UseEmptyLogging();
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
