using Base.Data;
using Letter.Entity;
using Microsoft.EntityFrameworkCore;

namespace Letter.Data
{
    public class DataContext : BaseDataContext
    {
        public DbSet<LetterEntity> Letters { get; set; }

        public DataContext(DbContextOptions<DataContext> options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //.HasOne(u => u.Coin)
            //.WithMany(c => c.CoinRate)
            //.OnDelete(DeleteBehavior.Cascade);
            //Cascade: the dependent entity is deleted along with the master
            //SetNull: property - the foreign key in the dependent entity is set to null
            //Restrict: the dependent entity does not change in any way when the main entity is deleted

            //modelBuilder.Entity<Coin>()
            //    .Property(u => u.AddedCoin).HasDefaultValue(false);

        }
    }


}
