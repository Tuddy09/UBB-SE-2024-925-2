using System;

namespace TwoPlayerGames
{
    public abstract class ChessPiece : IChessPiece
    {
        protected int xPosition;
        protected int yPosition;
        protected Player2PlayerGame player;
        protected bool movedFromInitialPosition;
        protected string pieceType;

        public int XPosition
        {
            get { return xPosition; }
            set { xPosition = value; }
        }

        public int YPosition
        {
            get { return yPosition; }
            set { yPosition = value; }
        }

        public Player2PlayerGame Player
        {
            get => player;
            set => player = value;
        }

        public bool MovedFromInitialPosition
        {
            get { return movedFromInitialPosition; }
            set { movedFromInitialPosition = value; }
        }

        public string PieceType { get => pieceType; }

        public ChessPiece(int x, int y, Player2PlayerGame player)
        {
            this.xPosition = x;
            this.yPosition = y;
            this.player = player;
            this.movedFromInitialPosition = false;
            this.pieceType = string.Empty;
        }

        public void UpdatePosition(int newXPosition, int newYPosition)
        {
            this.xPosition = newXPosition;
            this.yPosition = newYPosition;
            this.movedFromInitialPosition = true;
        }

        public abstract bool IsValidMove(int newXPosition, int newYPosition);

        public string GetPieceType()
        {
            return this.pieceType;
        }
    }
}
