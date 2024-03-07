using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace APIRoulette.Entities
{
    public class BetDbContext:DbContext
    {
        public BetDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Bet> Bet { get; set; }
        public DbSet<Roulette> Roulette { get; set; }
        public DbSet<ConfigBet> ConfigBet { get; set; }
    }
   
}
