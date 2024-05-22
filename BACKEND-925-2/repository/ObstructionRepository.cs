using BACKEND_925_2.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TwoPlayerGames.Repository;

namespace TwoPlayerGames.Repo
{
    public class ObstructionRepository : BaseGameRepository, IGameRepo
    {
        private readonly GamesDbContext _context;
        public ObstructionRepository(GamesDbContext gamesDbContext) : base(gamesDbContext)
        {
            _context = gamesDbContext;
        }

        public override Dictionary<Guid, IGame> GetGames()
        {
            Dictionary<Guid, IGame> games = new Dictionary<Guid, IGame>();
            Guid GameId = _context.Games.Find("Obstruction").Id;
            GameState gameState = _context.GameStates.Find(GameId);

            IGame obstructionGame = LoadGameFromUnfinishedState(gameState);
            games.Add(gameState.Id, obstructionGame);
            return games;
        }
    }
}
