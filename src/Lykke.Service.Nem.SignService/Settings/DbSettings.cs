using Lykke.SettingsReader.Attributes;

namespace Lykke.Service.Nem.SignService.Settings
{
    public class DbSettings
    {
        [AzureTableCheck]
        public string LogsConnString { get; set; }
    }
}
