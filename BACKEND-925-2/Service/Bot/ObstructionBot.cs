using System;
using BACKEND_925_2.Service;
using TwoPlayerGames;
using TwoPlayerGames.Domain.GameObjects;

namespace BACKEND_925_2.Service.Bot
{
    public class ObstructionBot : Player2PlayerGame
    {
        private readonly string difficulty;
        private readonly IGameService gameService;

        public ObstructionBot(string difficulty, IGameService gameService)
        {
            this.difficulty = difficulty;
            this.gameService = gameService;
        }

        public void CalculateMove(ObstructionGame game)
        {
            int width = game.Board.GetWidth;
            int height = game.Board.GetHeight;

            int bestX = 0;
            int bestY = 0;
            int maxPriority = int.MinValue;

            for (int row = 0; row < width; row++)
            {
                for (int column = 0; column < height; column++)
                {
                    if (IsMoveValid(game, row, column))
                    {
                        int priority = CalculatePriority(game, row, column);
                        if (priority > maxPriority)
                        {
                            maxPriority = priority;
                            bestX = row;
                            bestY = column;
                        }
                    }
                }
            }

            gameService.Play(2, [bestX, bestY]);
        }

        public int CalculatePriority(ObstructionGame game, int x, int y)
        {
            int priority = Math.Min(Math.Min(x, game.Board.GetWidth - x - 1), Math.Min(y, game.Board.GetHeight - y - 1));

            return priority;
        }

        public static bool IsMoveValid(ObstructionGame game, int x, int y)
        {
            return x >= 0 && x < game.Board.GetWidth && y >= 0 && y < game.Board.GetHeight && game.Board.GetPiece(x, y) == null;
        }
    }
}
