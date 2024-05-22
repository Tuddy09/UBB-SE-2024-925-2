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

        public DbSet<GameHistory> GameHistories { get; set; }

        public DbSet<PlayerQueue> PlayerQueue { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GameHistory>().HasKey(g => new { g.Players, g.GameType });
        }
    }
}
