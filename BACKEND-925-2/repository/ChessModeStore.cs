using System;
using TwoPlayerGames.Domain.Enums;

namespace TwoPlayerGames.Repository
{
    public class ChessModeStore
    {
        public static ChessModes GetMode(string mode)
        {
            switch (mode)
            {
                case "RAPID":
                    return ChessModes.RAPID;
                case "BLITZ":
                    return ChessModes.BLITZ;
                case "BULLET":
                    return ChessModes.BULLET;
                default:
                    throw new Exception("Invalid mode");
            }
        }
    }
}
