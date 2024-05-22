using System;
using TwoPlayerGames;

public class BishopPiece : ChessPiece
{
    public BishopPiece(int x, int y, Player player) : base(x, y, player)
    {
        this.pieceType = "Bishop";
    }

    public override bool IsValidMove(int newXPosition, int newYPosition)
    {
        return Math.Abs(newXPosition - xPosition) == Math.Abs(newYPosition - yPosition);
    }
}