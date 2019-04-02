using System.Text;
using System.Net.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using io.nem1.sdk.Model.Accounts;
using io.nem1.sdk.Model.Blockchain;
using McMaster.Extensions.CommandLineUtils;
using Newtonsoft.Json;

namespace AccountGenerator
{
    public class Program
    {
        public static Task Main(string[] args) =>
            CommandLineApplication.ExecuteAsync<Program>(args);

        [Option(Description = "Sign facade URL. Mandatory.")]
        [Required]
        [Url]
        public string Url { get; set; }

        [Option(Description = "Sign facade API key with import address permissions. Mandatory.")]
        [Required]
        public string Key { get; set; }

        [Option(Description = "Network, either 'mainnet' or 'testnet'. Optional, `mainnet` by default.")]
        [AllowedValues("mainnet", "testnet", IgnoreCase = true)]
        public string Network { get; set; }

        public async Task OnExecute()
        {
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("ApiKey", Key);

            var acc = Account.GenerateNewAccount(NetworkType.GetNetwork(Network ?? "mainnet"));

            var requestBody = JsonConvert.SerializeObject
            (
                new
                {
                    acc.PrivateKey,
                    PublicAddress = acc.Address.Plain
                }
            );

            var response = await httpClient.PostAsync($"{Url.TrimEnd('/')}/api/NEM/wallets/import", new StringContent(requestBody, Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();

            Console.WriteLine($"Imported NEM {Network} address: {acc.Address.Plain}");
        }
    }
}
