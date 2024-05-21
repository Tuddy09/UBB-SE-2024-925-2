using Microsoft.EntityFrameworkCore;

namespace BACKEND_925_2.Models
{
    public class GamesDbContext : DbContext
    {
        public GamesDbContext(DbContextOptions<GamesDbContext> options) : base(options)
        {
        }
    }
}
