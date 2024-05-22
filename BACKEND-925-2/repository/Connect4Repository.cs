using BACKEND_925_2.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TwoPlayerGames.Repository;

namespace TwoPlayerGames.Repo
{
    public class Connect4Repository : BaseGameRepository, IGameRepo
    {
        private readonly GamesDbContext _context;
        public Connect4Repository(GamesDbContext gamesDbContext) : base(gamesDbContext)
        {
            _context = gamesDbContext;
        }

        public override Dictionary<Guid, IGame> GetGames()
        {
            Dictionary<Guid, IGame> games = new Dictionary<Guid, IGame>();
            Guid GameId = _context.Games.Find("Connect4").Id;
            GameState gameState = _context.GameStates.Find(GameId);

            IGame connect4Game = LoadGameFromUnfinishedState(gameState);
            games.Add(gameState.Id, connect4Game);
            return games;
        }
    }
}
