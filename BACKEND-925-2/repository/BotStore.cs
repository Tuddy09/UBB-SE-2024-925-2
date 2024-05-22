using BACKEND_925_2.Models;
using BACKEND_925_2.Service.Bot;
using System;
using TwoPlayerGames.exceptions;

namespace TwoPlayerGames.Repository
{
    public class BotStore
    {
        public static IBot GetBotForTheGivenGameType(string gameType, string difficulty, Guid gameStateId, Player2PlayerGame player, GamesDbContext gamesDbContext)
        {
            if (difficulty != "easy" && difficulty != "medium" && difficulty != "hard")
            {
                throw new InvalidDifficultyException();
            }

            switch (gameType)
            {
                case "Connect4":
                    return new Connect4Bot(difficulty, gameStateId, player, gamesDbContext);
                case "Chess":
                    return new Connect4Bot(difficulty, gameStateId, player, gamesDbContext);
                case "Obstruction":
                    return new Connect4Bot(difficulty, gameStateId, player, gamesDbContext);
                case "Darts":
                    return new Connect4Bot(difficulty, gameStateId, player, gamesDbContext);
                default:
                    throw new InvalidGameException();
            }
        }
    }
}
