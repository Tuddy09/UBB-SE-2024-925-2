using System;
using TwoPlayerGames;

public class QueenPiece : ChessPiece
{
    public QueenPiece(int x, int y, Player2PlayerGame player) : base(x, y, player)
    {
        this.pieceType = "Queen";
    }

    public override bool IsValidMove(int newXPosition, int newYPosition)
    {
        // Queens can move horizontally, vertically, or diagonally
        int xDistance = Math.Abs(newXPosition - xPosition);
        int yDistance = Math.Abs(newYPosition - yPosition);

        return (xPosition == newXPosition || yPosition == newYPosition || xDistance == yDistance);
    }
}
