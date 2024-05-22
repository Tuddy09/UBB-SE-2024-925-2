using System;
using System.Collections.Generic;

namespace TwoPlayerGames.Repository
{
    public class ChessPieceStore
    {
        private static Dictionary<string, string> blackPiecesStore = new Dictionary<string, string>
        {
            { "Pawn", "../Images/PawnB.png" },
            { "Rook", "../Images/RookB.png" },
            { "Knight", "../Images/KnightB.png" },
            { "Bishop", "../Images/BishopB.png" },
            { "Queen", "../Images/QueenB.png" },
            { "King", "/Images/KingB.png" }
        };

        private static Dictionary<string, string> whitePiecesStore = new Dictionary<string, string>
        {
            { "Pawn", "../Images/PawnW.png" },
            { "Rook", "../Images/RookW.png" },
            { "Knight", "../Images/KnightW.png" },
            { "Bishop", "../Images/BishopW.png" },
            { "Queen", "../Images/QueenW.png" },
            { "King", "../Images/KingW.png" }
        };
        public static string GetPieceByTypeAndColor(string pieceType, string color)
        {
            if (color == "white")
            {
                return whitePiecesStore[pieceType];
            }
            else
            {
                return blackPiecesStore[pieceType];
            }
        }
    }
}
