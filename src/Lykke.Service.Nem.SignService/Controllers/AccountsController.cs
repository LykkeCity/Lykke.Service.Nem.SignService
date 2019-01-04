using io.nem1.sdk.Model.Accounts;
using io.nem1.sdk.Model.Blockchain;
using Microsoft.AspNetCore.Mvc;

namespace Lykke.Service.Nem.SignService.Controllers
{
    [ApiController]
    [Route("/api/accounts")]
    public class AccountsController : ControllerBase
    {
        [HttpPost("{network}")]
        public object Create(string network)
        {
            var acc = Account.GenerateNewAccount(NetworkType.GetNetwork(network));

            return new
            {
                acc.PrivateKey,
                acc.Address
            };
        }
    }
}
