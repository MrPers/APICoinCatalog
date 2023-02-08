using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Сoin.Contracts.Services;
//using ChoETL;

namespace Сoin.Data
{
    public class DataSample
    {
        public static async Task InitializeAsync(IServiceScope serviceScope)
        {

            IServiceProvider scopeServiceProvider = serviceScope.ServiceProvider;
            await scopeServiceProvider.GetRequiredService<DataContext>().Database.MigrateAsync();
            var context = scopeServiceProvider.GetRequiredService<DataContext>();
            var _coinService = scopeServiceProvider.GetRequiredService<ICoinService>();

            if (!context.Coins.Any() & !context.CoinRates.Any())
            {
                var coinsName = new List<String>
                {
                    "bitcoin",
                    "ethereum"
                };

                foreach (var name in coinsName)
                {
                    await _coinService.AddСoinСoinExchangesAsync(name);
                }
            }

        }
    }

}
