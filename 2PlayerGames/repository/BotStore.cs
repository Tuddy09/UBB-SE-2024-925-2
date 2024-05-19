    using System;
using TwoPlayerGames.exceptions;
using TwoPlayerGames.Service.Bot;

namespace TwoPlayerGames.Repository
{
    public class BotStore
    {
        public static IBot GetBotForTheGivenGameType(string gameType, string difficulty, Guid gameStateId, Player player)
        {
            if (difficulty != "easy" && difficulty != "medium" && difficulty != "hard")
            {
                throw new InvalidDifficultyException();
            }

            switch (gameType)
            {
                case "Connect4":
                    return new Connect4Bot(difficulty, gameStateId, player);
                case "Chess":
                    return new Connect4Bot(difficulty, gameStateId, player);
                case "Obstruction":
                    return new Connect4Bot(difficulty, gameStateId, player);
                case "Darts":
                    return new Connect4Bot(difficulty, gameStateId, player);
                default:
                    throw new InvalidGameException();
            }
        }
    }
}
