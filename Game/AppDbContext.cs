using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.ApplicationServices;

namespace Game
{
    internal class AppDbContext : DbContext
    {
        public DbSet<UserGame> Users { get; set; }

        public AppDbContext()
        {
            Database.EnsureCreated();
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseNpgsql("Host=localhost;Database=BackgammonDB;Username=postgres;Password=4286")
                    .EnableSensitiveDataLogging();
            }
        }
    }
}
