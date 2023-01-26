using MailGraphAnalysis.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using MailGraphAnalysis.Data.Repository;
using MailGraphAnalysis.Contracts.Repo;
using MailGraphAnalysis.Contracts.Persistence;

namespace MailGraphAnalysis.Data
{
    public class DataSample
    {

        public static async Task InitializeAsync(IServiceScope serviceScope)
        {

            IServiceProvider scopeServiceProvider = serviceScope.ServiceProvider;
            await scopeServiceProvider.GetRequiredService<DataContext>().Database.MigrateAsync();
            var context = scopeServiceProvider.GetRequiredService<DataContext>();

            var сoinFromAPI = scopeServiceProvider.GetRequiredService<ICoinFromAPI>();

            if (!context.Coins.Any())
            {
                var coinsName = new List<String>
                {
                    "bitcoin",
                    "ethereum",
                    //"solana",
                    //"cosmos",
                    //"polkadot",
                    "binance-peg-xrp",
                    "binancecoin",
                    "filecoin"
                };

                var newCoins = await сoinFromAPI.TakeCoinsNameFromAPIAsync(coinsName);
                context.Coins.AddRange(newCoins);

                await context.SaveChangesAsync();
            }

            if (!context.CoinRate.Any())
            {
                var сoinRepository = scopeServiceProvider.GetRequiredService<ICoinRepository>();
                var coins = await сoinRepository.GetAllAsync();

                var coinExchanges = await сoinFromAPI.TakeCoinsFromAPIAsync(coins);

                context.CoinRate.AddRange(coinExchanges);

                await context.SaveChangesAsync();
            }
        }
    }

}
