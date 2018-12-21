using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Common;
using io.nem1.sdk.Model.Accounts;
using io.nem1.sdk.Model.Blockchain;
using io.nem1.sdk.Model.Transactions;
using Lykke.Service.BlockchainApi.Contract.Wallets;
using Lykke.Service.BlockchainApi.Sdk;

namespace Lykke.Service.Nem.SignService.Services
{
    public class NemSignService : IBlockchainSignService
    {
        readonly Address _hotWalletAddress;

        public NemSignService(string hotWalletAddress)
        {
            _hotWalletAddress = Address.CreateFromEncoded(hotWalletAddress);
        }

        public Task<WalletResponse> CreateWalletAsync()
        {
            return Task.FromResult(new WalletResponse
            {
                PublicAddress = $"{_hotWalletAddress.Plain}+{Guid.NewGuid()}",
                PrivateKey = "DUMMY_PRIVATE_KEY"
            });
        }

        public Task<(string hash, string signedTransaction)> SignTransactionAsync(
            string transactionContext, IReadOnlyList<string> privateKeys)
        {
            if (privateKeys.Count != 1)
            {
                throw new ArgumentException("Only single private key is allowed");
            }

            var key = KeyPair.CreateFromPrivateKey(privateKeys[0]);
            var signedTransaction = TransferTransaction.FromJson(transactionContext).SignWith(key);
            var result = (
                signedTransaction.Hash,
                signedTransaction.ToJson().ToBase64()
            );

            return Task.FromResult(result);
        }

        public bool ValidatePrivateKey(string key)
        {
            try
            {
                return KeyPair.CreateFromPrivateKey(key) != null;
            }
            catch
            {
                return false;
            }
        }

        public bool ValidateHotWalletPrivateKey(string key)
        {
            try
            {
                return Account.CreateFromPrivateKey(key, _hotWalletAddress.NetworkByte).Address.Plain == _hotWalletAddress.Plain;
            }
            catch
            {
                return false;
            }
        }
    }
}