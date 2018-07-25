using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Common;
using io.nem1.sdk.Model.Accounts;
using io.nem1.sdk.Model.Blockchain;
using io.nem1.sdk.Model.Mosaics;
using io.nem1.sdk.Model.Transactions;
using io.nem1.sdk.Model.Transactions.Messages;
using Lykke.Service.BlockchainApi.Contract.Transactions;
using Lykke.Service.Nem.SignService.Models;
using Lykke.Service.Nem.SignService.Settings;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Lykke.Service.Nem.SignService.Controllers
{
    [Route("/api/sign")]
    public class SignController : Controller
    {
        private readonly BlockchainSettings _bcnSettings;
        private readonly PropertyInfo _packetProperty = typeof(SignedTransaction)
            .GetProperty("TransactionPacket", BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.NonPublic);

        public SignController(BlockchainSettings bcnSettings)
        {
            _bcnSettings = bcnSettings;
        }

        [HttpPost()]
        public IActionResult SignTransaction([FromBody] SignTransactionRequest request)
        {
            if (!ModelState.IsValid || 
                !ModelState.IsValidRequest(request, out var context))
            {
                return BadRequest(ModelState);
            }

            var tx = TransferTransaction.Create(
                NetworkType.GetNetwork(_bcnSettings.Network),
                Deadline.CreateMinutes(context.ExpiresInMinutes),
                context.Fee,
                Address.CreateFromEncoded(context.To),
                new List<Mosaic> { Mosaic.CreateFromIdentifier(context.AssetId, context.Amount) },
                EmptyMessage.Create()
            );

            var signedTx = tx.SignWith(KeyPair.CreateFromPrivateKey(request.PrivateKeys.First()));

            dynamic txPacket = _packetProperty.GetValue(signedTx);

            var signedTxContext = new SignedTransactionContext
            {
                Hash = signedTx.Hash,
                Payload = signedTx.Payload,
                Signature = txPacket.signature,
                Signer = signedTx.Signer,
                TransactionType = signedTx.TransactionType
            };

            return Ok(new SignedTransactionResponse
            {
                SignedTransaction = JsonConvert.SerializeObject(signedTxContext).ToBase64()
            });
        }
    }
}
