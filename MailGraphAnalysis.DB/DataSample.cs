using MailGraphAnalysis.DB.Business;
using MailGraphAnalysis.DB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using AutoMapper;

namespace MailGraphAnalysis.DB
{
    public class DataSample
    {

        public static async Task InitializeAsync(IServiceScope serviceScope)
        {

            IServiceProvider scopeServiceProvider = serviceScope.ServiceProvider;
            await scopeServiceProvider.GetRequiredService<DataContext>().Database.MigrateAsync();
            var context = scopeServiceProvider.GetRequiredService<DataContext>();
            //var mapper = scopeServiceProvider.GetRequiredService<Mapper>();

            //var rt = new CoinExchangeRepository(context, mapper);

            if (!context.Coins.Any())
            {
                //coins = new List<Coin> {
                //    new Coin{ Name = "BTC"},
                //    new Coin{ Name = "ETH"},
                //    new Coin{ Name = "SOL"},
                //    new Coin{ Name = "DOT"},
                //};

                var newCoins = await CoinFromAPI.TakeCoinsNameFromAPIAsync();

                context.Coins.AddRange(newCoins);

                await context.SaveChangesAsync();
            }

            if (!context.CoinExchanges.Any())
            {


                var coinsName = new List<String>
                {
                    "BTC",
                    "ETH",
                    "SOL",
                    "DOT",
                };

                List <Coin> coins = await context.Set<Coin>().ToListAsync();

                var coinExchanges = await CoinFromAPI.TakeCoinsFromAPIAsync(coins);

                context.CoinExchanges.AddRange(coinExchanges);

                await context.SaveChangesAsync();
            }
        }
    }

}
