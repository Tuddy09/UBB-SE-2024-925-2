using BACKEND_925_2.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TwoPlayerGames;
using TwoPlayerGames.Domain.GameObjects;
using TwoPlayerGames.exceptions;
using TwoPlayerGames.Repo;
using TwoPlayerGames.Repository;

namespace BACKEND_925_2.Service
{
    public class ObstructionService : IGameService
    {
        private ObstructionGame obstructionGame;
        private List<Player2PlayerGame> players;
        private IGameRepo obstructionRepo;
        private readonly GamesDbContext _context;

        public ObstructionService(Guid gameStateID, Player2PlayerGame player1, Player2PlayerGame player2, int width, int height, IGameRepo repo)
        {
            obstructionRepo = repo;
            players = [player1, player2];
            obstructionGame = (ObstructionGame?)obstructionRepo.GetGameById(gameStateID);
            if (obstructionGame == null)
            {
                obstructionGame = new ObstructionGame(player1, player2, width, height);
                if (player2.Name != "Bot")
                {
                    obstructionRepo.AddGame(obstructionGame);
                }
            }
            else
            {
                Random random = new Random();
                int turn = random.Next(0, 2);
                obstructionGame.GameState.Turn = turn;
            }
        }

        public IGame Play(int nrParameters, object[] parameters)
        {
            int x = Convert.ToInt32(parameters[0]);
            int y = Convert.ToInt32(parameters[1]);
            Player2PlayerGame player = GetCurrentPlayer();

            PlaceSymbol(x, y);

            BlockSurroundingSquares(x, y);

            if (!CheckCurrentState())
            {
                obstructionGame.GameState.Winner = player;
            }

            SwitchTurn();
            obstructionGame.SaveGame();
            if (player.Name != "Bot")
            {
                obstructionRepo.UpdateGame(obstructionGame);
            }

            return obstructionGame;
        }

        private void PlaceSymbol(int x, int y)
        {
            if (obstructionGame.Board.GetPiece(x, y) != null)
            {
                throw new InvalidMoveException();
            }

            IPiece piece = new ObstructionPiece(x, y, GetCurrentPlayer()); // TODO sa verific simpbolu bun pt current player
            obstructionGame.Board.AddPiece(piece);
        }

        private void BlockSurroundingSquares(int x, int y)
        {
            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    int newX = x + dx;
                    int newY = y + dy;
                    if (newX >= 0 && newX < obstructionGame.Board.GetWidth && newY >= 0 && newY < obstructionGame.Board.GetHeight && (newX != x || newY != y))
                    {
                        IPiece piece = new ObstructionPiece(newX, newY, Player2PlayerGame.Null());
                        obstructionGame.Board.AddPiece(piece);
                    }
                }
            }
        }

        private void SwitchTurn()
        {
            obstructionGame.CurrentPlayerIndex = (obstructionGame.CurrentPlayerIndex + 1) % 2;
        }

        private Player2PlayerGame GetCurrentPlayer()
        {
            return players[obstructionGame.GameState.Turn];
        }

        private bool CheckCurrentState()
        {
            if (((ObstructionBoard)obstructionGame.Board).IsFull() || !HasValidMovesLeft())
            {
                return false;
            }
            return true;
        }

        public bool HasValidMovesLeft()
        {
            for (int x = 0; x < obstructionGame.Board.GetWidth; x++)
            {
                for (int y = 0; y < obstructionGame.Board.GetHeight; y++)
                {
                    if (obstructionGame.Board.GetPiece(x, y) == null)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public IGame GetGame()
        {
            return obstructionGame;
        }

        public void SetGame(IGame game)
        {
            obstructionGame = (ObstructionGame)game;
        }
    }
}

