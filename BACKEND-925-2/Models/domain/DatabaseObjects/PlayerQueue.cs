using TwoPlayerGames.Domain.Enums;

namespace TwoPlayerGames.Domain.DatabaseObjects
{
    public class PlayerQueue
    {
        private Player2PlayerGame player;
        private Games gameType;
        private int eloRating;
        private ChessModes? chessMode;
        private int? obstructionWidth;
        private int? obstructionHeight;

        public Player2PlayerGame Player { get => player; set => player = value; }
        public Games GameType { get => gameType; set => gameType = value; }
        public int EloRating { get => eloRating; set => eloRating = value; }
        public ChessModes? ChessMode { get => chessMode; set => chessMode = value; }
        public int? ObstractionWidth { get => obstructionWidth; set => obstructionWidth = value; }
        public int? ObstractionHeight { get => obstructionHeight; set => obstructionHeight = value; }

        public PlayerQueue(Player2PlayerGame player, Games gameType, int elo, ChessModes? chessMode, int? obstructionWidth, int? obstructionHeigth)
        {
            this.player = player;
            this.gameType = gameType;
            this.eloRating = elo;
            this.chessMode = chessMode;
            this.obstructionWidth = obstructionWidth;
            this.obstructionHeight = obstructionHeigth;
        }

        public PlayerQueue()
        {
            this.player = new Player2PlayerGame();
            this.gameType = new Games();
            this.eloRating = 0;
            this.chessMode = null;
            this.obstructionWidth = null;
            this.obstructionHeight = null;
        }
    }
}
