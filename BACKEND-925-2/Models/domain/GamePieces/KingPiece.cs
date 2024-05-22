using System;

namespace TwoPlayerGames
{
    public class KingPiece : ChessPiece
    {
        public KingPiece(int x, int y, Player player) : base(x, y, player)
        {
            this.pieceType = "King";
        }

        public override bool IsValidMove(int newXPosition, int newYPosition)
        {
            // Kings can move one square in any direction
            int xDistance = Math.Abs(newXPosition - xPosition);
            int yDistance = Math.Abs(newYPosition - yPosition);

            return xDistance <= 1 && yDistance <= 1;
        }
    }
}
