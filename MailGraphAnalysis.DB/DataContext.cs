using Microsoft.EntityFrameworkCore;
using MailGraphAnalysis.DB.Models;

namespace MailGraphAnalysis.DB
{
    public class DataContext : DbContext
    {
        public DbSet<Letter> Letters { get; set; }
        public DbSet<CoinExchange> CoinExchanges { get; set; }
        public DbSet<Coin> Coins { get; set; }

        public DataContext(DbContextOptions<DataContext> options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CoinExchange>()
                .HasOne(sc => sc.Coin)
                .WithMany(s => s.CoinExchanges)
                .HasForeignKey(sc => sc.CoinId)
                .HasPrincipalKey(sc => sc.Id)
                .OnDelete(DeleteBehavior.Cascade);

            //.HasOne(u => u.Coin)
            //.WithMany(c => c.CoinExchanges)
            //.OnDelete(DeleteBehavior.Cascade);
            //Cascade: the dependent entity is deleted along with the master
            //SetNull: property - the foreign key in the dependent entity is set to null
            //Restrict: the dependent entity does not change in any way when the main entity is deleted

            modelBuilder.Entity<Coin>()
                .Property(u => u.AddedCoin).HasDefaultValue(false);

            modelBuilder.Entity<Coin>()
                .HasIndex(u => u.Name).IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
