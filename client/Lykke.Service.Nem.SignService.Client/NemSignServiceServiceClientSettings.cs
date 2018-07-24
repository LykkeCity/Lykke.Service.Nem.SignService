using Lykke.SettingsReader.Attributes;

namespace Lykke.Service.Nem.SignService.Client 
{
    /// <summary>
    /// Nem.SignService client settings.
    /// </summary>
    public class NemSignServiceServiceClientSettings 
    {
        /// <summary>Service url.</summary>
        [HttpCheck("api/isalive")]
        public string ServiceUrl {get; set;}
    }
}
