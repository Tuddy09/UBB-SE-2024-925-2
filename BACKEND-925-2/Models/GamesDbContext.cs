using Microsoft.EntityFrameworkCore;
using TwoPlayerGames;
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
        public DbSet<>
    }
}
