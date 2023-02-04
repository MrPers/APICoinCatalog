using MailGraphAnalysis.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using MailGraphAnalysis.Data.Repository;
using MailGraphAnalysis.Contracts.Repo;
using MailGraphAnalysis.Contracts.Persistence;
using MailGraphAnalysis.Contracts.Services;
using ChoETL;
using System.Text;

namespace MailGraphAnalysis.Data
{
    public class DataSample
    {

        public static async Task InitializeAsync(IServiceScope serviceScope)
        {

            IServiceProvider scopeServiceProvider = serviceScope.ServiceProvider;
            await scopeServiceProvider.GetRequiredService<DataContext>().Database.MigrateAsync();
            var context = scopeServiceProvider.GetRequiredService<DataContext>();
            var _coinService = scopeServiceProvider.GetRequiredService<ICoinService>();

            if (!context.Coins.Any() & !context.CoinRate.Any())
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

            //StringBuilder sb = new StringBuilder();
            //var comm = await context.Coins.ToListAsync();

            //using (var parser = new ChoJSONWriter(sb))
            //        parser.Write(comm);

            //Console.WriteLine(sb.ToString());

        }
    }

}
