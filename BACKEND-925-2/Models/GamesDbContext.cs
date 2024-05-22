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
        public DbSet<Player> Players { get; set; }
        public DbSet<Games> Games { get; set; }

        public DbSet<GameStats> GameStats { get; set; }

        public DbSet<GameHistory> GameHistories { get; set; }

        public DbSet<PlayerQueue> PlayerQueue { get; set; }

    }
}
