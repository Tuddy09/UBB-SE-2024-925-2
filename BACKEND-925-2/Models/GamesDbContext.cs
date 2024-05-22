    using Microsoft.EntityFrameworkCore;
using TwoPlayerGames;
using TwoPlayerGames.Domain.Auxiliary;
using TwoPlayerGames.Domain.DatabaseObjects;
using TwoPlayerGames.Domain.GameObjects;

namespace BACKEND_925_2.Models
{
    public class GamesDbContext : DbContext
    {
        public GamesDbContext(DbContextOptions<GamesDbContext> options) : base(options)
        {
        }
        public DbSet<GameState> GameStates { get; set; }
        public DbSet<Player2PlayerGame> Players { get; set; }
        public DbSet<Games> Games { get; set; }

        public DbSet<GameStats> GameStats { get; set; }

        public DbSet<PlayerQueue> PlayerQueue { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GameStats>()
       .HasKey(gs => new { gs.PlayerId, gs.GameId });

            modelBuilder.Entity<GameStats>()
                .HasOne(gs => gs.Player)
                .WithMany(p => p.GameStats) // Assuming Player2PlayerGame has a list of GameStats
                .HasForeignKey(gs => gs.PlayerId);
            modelBuilder.Entity<GameStats>()
                .HasOne(gs => gs.Game)
                .WithMany(g => g.GameStats) // Assuming Games has a list of GameStats
                .HasForeignKey(gs => gs.GameId);
            modelBuilder.Entity<PlayerQueue>().HasKey(pq => new { pq.PlayerId, pq.GameId });
            modelBuilder.Entity<PlayerQueue>()
                .HasOne(pq => pq.Player)
                .WithMany(p => p.Queues) // Assuming Player2PlayerGame has a list of PlayerQueue
                .HasForeignKey(pq => pq.PlayerId);
            modelBuilder.Entity<PlayerQueue>()
                .HasOne(pq => pq.GameType)
                .WithMany(g => g.Queues) // Assuming Games has a list of PlayerQueue
                .HasForeignKey(pq => pq.GameId);
            modelBuilder.Entity<GameState>()
                .HasKey(gs => new { gs.Id });
        }
    }
}
