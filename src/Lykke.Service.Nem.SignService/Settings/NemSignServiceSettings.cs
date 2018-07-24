using JetBrains.Annotations;
using Lykke.SettingsReader.Attributes;

namespace Lykke.Service.Nem.SignService.Settings
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class NemSignServiceSettings
    {
        public DbSettings Db { get; set; }
        public BlockchainSettings Blockchain { get; set; }
    }
}
