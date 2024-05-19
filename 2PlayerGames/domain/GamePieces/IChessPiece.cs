using System;

namespace TwoPlayerGames
{
    public interface IChessPiece : IPiece
    {
        void UpdatePosition(int newXPosition, int newYPosition);
        bool IsValidMove(int newXPosition, int newYPosition);
        bool MovedFromInitialPosition { get; set; }

        string GetPieceType();
    }
}