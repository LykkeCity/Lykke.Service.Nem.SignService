using Autofac;
using io.nem1.sdk.Model.Blockchain;
using Lykke.Sdk;
using Lykke.Service.Nem.SignService.Controllers;
using Lykke.Service.Nem.SignService.Settings;
using Lykke.SettingsReader;

namespace Lykke.Service.Nem.SignService.Modules
{    
    public class ServiceModule : Module
    {
        private readonly IReloadingManager<AppSettings> _appSettings;

        public ServiceModule(IReloadingManager<AppSettings> appSettings)
        {
            _appSettings = appSettings;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(_appSettings.CurrentValue.NemSignService.Blockchain);
        }
    }
}
