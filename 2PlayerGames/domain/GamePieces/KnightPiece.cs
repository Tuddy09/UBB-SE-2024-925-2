using System;
using TwoPlayerGames;

public class KnightPiece : ChessPiece
{
    public KnightPiece(int x, int y, Player player) : base(x, y, player)
    {
        this.pieceType = "Knight";
    }

    public override bool IsValidMove(int newXPosition, int newYPosition)
    {
        // L-shape pattern: 2 squares in one direction and 1 square perpendicular to that direction.
        int xDistance = Math.Abs(newXPosition - xPosition);
        int yDistance = Math.Abs(newYPosition - yPosition);

        return (xDistance == 1 && yDistance == 2) || (xDistance == 2 && yDistance == 1);
    }
}
