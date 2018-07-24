using Lykke.HttpClientGenerator;

namespace Lykke.Service.Nem.SignService.Client
{
    public class NemSignServiceClient : INemSignServiceClient
    {
        //public IControllerApi Controller { get; }
        
        public NemSignServiceClient(IHttpClientGenerator httpClientGenerator)
        {
            //Controller = httpClientGenerator.Generate<IControllerApi>();
        }
        
    }
}
