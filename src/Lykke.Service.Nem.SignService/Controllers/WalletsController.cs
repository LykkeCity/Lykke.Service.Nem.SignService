using io.nem1.sdk.Model.Accounts;
using io.nem1.sdk.Model.Blockchain;
using Lykke.Service.BlockchainApi.Contract.Wallets;
using Lykke.Service.Nem.SignService.Settings;
using Microsoft.AspNetCore.Mvc;

namespace Lykke.Service.Nem.SignService.Controllers
{
    [Route("/api/wallets")]
    public class WalletsController : Controller
    {
        private readonly BlockchainSettings _bcnSettings;

        public WalletsController(BlockchainSettings bcnSettings)
        {
            _bcnSettings = bcnSettings;
        }

        [HttpPost]
        public WalletResponse Create()
        {
            var network = NetworkType.GetNetwork(_bcnSettings.Network);
            var account = Account.GenerateNewAccount(network);

            return new WalletResponse
            {
                PublicAddress = account.Address.Plain,
                PrivateKey = account.PrivateKey
            };
        }
    }
}
