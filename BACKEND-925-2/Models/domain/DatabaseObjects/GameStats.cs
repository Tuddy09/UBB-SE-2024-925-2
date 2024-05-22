namespace TwoPlayerGames.Domain.DatabaseObjects
{
    public class GameStats
    {
        private Player2PlayerGame player;
        private Games game;
        private int eloRating;
        private int highestElo;
        private int totalMatches;
        private int totalWins;
        private int totalDraws;
        private int totalPlayTime;
        private int totalNumberOfTurn;

        public Player2PlayerGame Player { get => player; set => this.player = value; }
        public Games Game { get => game; set => this.game = value; }
        public int EloRating { get => eloRating; set => this.eloRating = value; }
        public int HighestElo { get => highestElo; set => this.highestElo = value; }
        public int TotalMatches { get => totalMatches; set => this.totalMatches = value; }
        public int TotalWins { get => totalWins; set => this.totalWins = value; }
        public int TotalDraws { get => totalDraws; set => this.totalDraws = value; }
        public int TotalPlayTime { get => totalPlayTime; set => this.totalPlayTime = value; }
        public int TotalNumberOfTurn { get => totalNumberOfTurn; set => this.totalNumberOfTurn = value; }


        public GameStats()
        {

        }
        public GameStats(Player2PlayerGame player, Games game)
        {
            this.player = player;
            this.game = game;
            this.eloRating = 420;
            this.highestElo = 420;
            this.totalMatches = 0;
            this.totalWins = 0;
            this.totalDraws = 0;
            this.totalPlayTime = 0;
            this.totalNumberOfTurn = 0;
        }

        public GameStats(Player2PlayerGame player, Games game, int eloRating, int highestElo, int totalMathces, int totalWins, int totalDraws, int totalPlayTime, int totalNumberOfTurn)
        {
            this.player = player;
            this.game = game;
            this.eloRating = eloRating;
            this.highestElo = highestElo;
            this.totalMatches = totalMathces;
            this.totalWins = totalWins;
            this.totalDraws = totalDraws;
            this.totalPlayTime = totalPlayTime;
            this.totalNumberOfTurn = totalNumberOfTurn;
        }
    }
}
