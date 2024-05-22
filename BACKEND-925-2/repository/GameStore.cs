using BACKEND_925_2.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using TwoPlayerGames.Repo;

namespace TwoPlayerGames.Repository
{
    public class GameStore
    {
        private readonly GamesDbContext _context;
        GameStore(GamesDbContext gamesDbContext)
        {
            _context = gamesDbContext;
            Games = InitializeGamesFromDatabase();
        }

        public static Dictionary<string, Games> Games { get; set; }

        private Dictionary<string, Games> InitializeGamesFromDatabase()
        {
            Dictionary<string, Games> dictionaryOfGames = new Dictionary<string, Games>();
            foreach (var game in _context.Games)
            {
                dictionaryOfGames.Add(game.Name, game);
            }
            return dictionaryOfGames;
        }

        public static Games GetGameById(Guid id)
        {
            return Games.Values.FirstOrDefault(x => x.Id.Equals(id));
        }
    }
}
