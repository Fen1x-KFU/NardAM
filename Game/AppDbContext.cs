using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.ApplicationServices;

namespace Game
{
    public class AppDbContext : DbContext
    {
        public DbSet<UserGame> Users { get; set; }

        public DbSet<Player> Players { get; set; }

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserGame>()
            .Property(u => u.IsReady)
            .HasColumnType("text"); // Явно указываем тип в БД

            // Указываем, что Dice — это "owned type" у Player
            modelBuilder.Entity<Player>()
                .OwnsOne(p => p.DicePlayer); // "Player имеет один Dice"
            
            modelBuilder.Entity<Player>()
            .HasOne(p => p.UserGame)
            .WithMany()
            .HasForeignKey(p => p.UserId); // EF автоматически создаст поле UserGameId
        }
    }
}
