namespace TwoPlayerGames
{
    public class RookPiece : ChessPiece
    {
        public RookPiece(int x, int y, Player2PlayerGame player) : base(x, y, player)
        {
            this.pieceType = "Rook";
        }

        public override bool IsValidMove(int newXPosition, int newYPosition) => newXPosition == this.xPosition || newYPosition == yPosition;
    }
}
