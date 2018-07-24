using Common;
using io.nem1.sdk.Model.Accounts;
using Lykke.Service.BlockchainApi.Contract.Transactions;
using Lykke.Service.Nem.SignService.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace Microsoft.AspNetCore.Mvc
{
    public static class ModelStateExtensions
    {
        public static bool IsValidRequest(this ModelStateDictionary modelState, SignTransactionRequest request, out TransactionContext context)
        {
            context = null;

            // if model in invalid then we can't check request

            if (modelState.IsValid)
            {
                foreach (var k in request.PrivateKeys)
                {
                    try
                    {
                        KeyPair.CreateFromPrivateKey(k);
                    }
                    catch
                    {
                        modelState.AddModelError(k, "Invalid private key");
                    }
                }

                try
                {
                    context = JsonConvert.DeserializeObject<TransactionContext>(request.TransactionContext.Base64ToString());
                }
                catch
                {
                    modelState.AddModelError(nameof(SignTransactionRequest.TransactionContext),
                        "Invalid transaction context");
                }
            }

            return modelState.IsValid;
        }
    }
}
