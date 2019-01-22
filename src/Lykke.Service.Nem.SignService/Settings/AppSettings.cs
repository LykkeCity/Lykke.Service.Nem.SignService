using JetBrains.Annotations;
using Lykke.Sdk.Settings;

namespace Lykke.Service.Nem.SignService.Settings
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class AppSettings : BaseAppSettings
    {
        public NemSignServiceSettings NemSignService { get; set; }
    }
}
