using BACKEND_925_2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TwoPlayerGames.Domain.GameObjects;
using TwoPlayerGames.exceptions;
using TwoPlayerGames.Repository;

namespace TwoPlayerGames.Repo
{
    public abstract class BaseGameRepository : IGameRepo
    {
        private readonly GamesDbContext _context;
        private Dictionary<Guid, IGame> games;

        public BaseGameRepository(GamesDbContext gamesDbContext)
        {
            _context = gamesDbContext;
            games = GetGames();
        }

        public void UpdateRepo()
        {
            games = GetGames();
        }
        public abstract Dictionary<Guid, IGame> GetGames();

        public IGame? GetGameById(Guid id)
        {
            UpdateRepo();
            if (games != null)
            {
                try
                {
                    return games[id];
                }
                catch (KeyNotFoundException)
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public void AddGame(IGame game)
        {
            games.Add(game.GameState.Id, game);
            _context.GameStates.Add(game.GameState);
        }

        public void UpdateGame(IGame game)
        {
            _context.GameStates.Update(game.GameState); 
        }

        public void RemoveGame(Guid id)
        {
            games.Remove(id);
        }

        public IGame LoadGameFromUnfinishedState(GameState unifinishedGameState)
        {
            IGame game = unifinishedGameState.GameType.Name switch
            {
                "Darts" => new DartsGame(unifinishedGameState),
                "Obstruction" => new ObstructionGame(unifinishedGameState),
                "Connect4" => new Connect4Game(unifinishedGameState),
                "Chess" => new ChessGame(unifinishedGameState),
                _ => throw new GameTypeNotFoundException("Game type not found")
            };
            return game;
        }

        public IGame GetGameFromDatabase(Guid id)
        {
            var gameState = _context.GameStates
                .Include(gs => gs.Players[0])
                .Include(gs => gs.Players[1])
                .Include(gs => gs.GameType)
                .FirstOrDefault(gs => gs.Id == id);

            if (gameState == null)
            {
                return null;
            }

            IGame game = gameState.GameType.Name switch
            {
                "Darts" => new DartsGame(gameState),
                "Obstruction" => new ObstructionGame(gameState),
                "Connect4" => new Connect4Game(gameState),
                "Chess" => new ChessGame(gameState),
                _ => throw new GameTypeNotFoundException("Game type not found")
            };

            return game;
        }
    }
}
