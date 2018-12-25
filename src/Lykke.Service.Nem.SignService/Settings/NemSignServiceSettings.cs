using io.nem1.sdk.Model.Blockchain;
using JetBrains.Annotations;

namespace Lykke.Service.Nem.SignService.Settings
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class NemSignServiceSettings
    {
        public string HotWalletAddress { get; set; }
    }
}
