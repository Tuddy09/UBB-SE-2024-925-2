using BACKEND_925_2.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TwoPlayerGames.Repository;

namespace TwoPlayerGames.Repo
{
    public class ChessRepository : BaseGameRepository, IGameRepo
    {
        private readonly GamesDbContext _context;
        public ChessRepository(GamesDbContext gamesDbContext) : base(gamesDbContext)
        {
            _context = gamesDbContext;
        }

        public override Dictionary<Guid, IGame> GetGames()
        {
            Dictionary<Guid, IGame> games = new Dictionary<Guid, IGame>();
            Guid GameId = _context.Games.Find("Chess").Id;
            GameState gameState = _context.GameStates.Find(GameId);

            IGame chessGame = LoadGameFromUnfinishedState(gameState);
            games.Add(gameState.Id, chessGame);
            return games;
        }
    }
}

