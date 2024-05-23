using System;

namespace TwoPlayerGames
{
    public class GameState
    {
        public Guid Id { get; set; }
        public List<Player2PlayerGame> Players { get; set; }
        public Games GameType { get; set; }
        public Player2PlayerGame? Winner { get; set; }
        public string StateJson { get; set; }
        public int Turn { get; set; }
        public int TimePlayed { get; set; }


        public GameState(Player2PlayerGame player1, Player2PlayerGame player2, Games gameType)
        {
            this.Id = Guid.NewGuid();
            this.Players = new List<Player2PlayerGame>(2);
            this.Players[0] = player1;
            this.Players[1] = player2;
            this.GameType = gameType;
            this.Winner = null;
            this.Turn = 0;
            this.TimePlayed = 0;
            this.StateJson = string.Empty;
        }

        public GameState(Player2PlayerGame player1, Player2PlayerGame player2, Games gameType, int turn)
        {
            this.Id = Guid.NewGuid();
            this.Players = new List<Player2PlayerGame>(2);
            this.Players[0] = player1;
            this.Players[1] = player2;
            this.GameType = gameType;
            this.Winner = null;
            this.Turn = turn;
            this.TimePlayed = 0;
            this.StateJson = string.Empty;
        }

        public GameState()
        {
            this.Id = Guid.Empty;
            this.Players = new List<Player2PlayerGame>(2);
            this.Players[0] = new Player2PlayerGame();
            this.Players[1] = new Player2PlayerGame();
            this.GameType = new Games();
            this.Winner = null;
            this.Turn = 0;
            this.TimePlayed = 0;
            this.StateJson = string.Empty;
        }

        public GameState(Guid gameStateId, Player2PlayerGame player1, Player2PlayerGame player2, Games gameType, int turn, int timePlayed, Player2PlayerGame? winner, string jsonString)
        {
            this.Id = gameStateId;
            this.Players = new List<Player2PlayerGame>(2);
            this.Players[0] = player1;
            this.Players[1] = player2;
            this.GameType = gameType;
            this.Winner = winner;
            this.Turn = turn;
            this.TimePlayed = timePlayed;
            this.StateJson = jsonString;
        }
    }
}