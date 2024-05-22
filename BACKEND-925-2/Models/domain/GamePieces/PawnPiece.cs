using System;
using TwoPlayerGames;

public class PawnPiece : ChessPiece
{
    public PawnPiece(int x, int y, Player2PlayerGame player) : base(x, y, player)
    {
        this.pieceType = "Pawn";
    }

    public override bool IsValidMove(int newXPosition, int newYPosition)
    {
        // standard position
        if (newXPosition == xPosition && Math.Abs(newYPosition - yPosition) == 1)
        {
            return true;
        }
        // 2 squares if not moved
        else if (newXPosition == xPosition && Math.Abs(newYPosition - yPosition) == 2 && !movedFromInitialPosition)
        {
            return true;
        }
        // Pawns standard capture
        else if (Math.Abs(newXPosition - xPosition) == 1 && (newYPosition == yPosition + 1 || newYPosition == yPosition - 1))
        {
            return true;
        }
        else
        {
            return false;
            // throw exception
        }
    }
}