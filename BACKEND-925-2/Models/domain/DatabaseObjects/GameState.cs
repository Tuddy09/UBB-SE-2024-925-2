using System;

namespace TwoPlayerGames
{
    public class GameState
    {
        public Guid Id { get; set; }
        public List<Player2PlayerGame> players { get; set; }
        public Games gameType { get; set; }
        public Player2PlayerGame? winnerPlayer { get; set; }
        public string stateJson { get; set; }
        public int turn { get; set; }
        public int timePlayed { get; set; }

        public List<Player2PlayerGame> Players { get => players; set => players = value; }

        public Games GameType { get => gameType; set => gameType = value; }
        public string StateJson { get => stateJson; set => stateJson = value; }

        public Player2PlayerGame? Winner { get => winnerPlayer; set => winnerPlayer = value; }
        public int Turn { get => turn; set => turn = value; }

        public int TimePlayed { get => timePlayed; set => timePlayed = value; }

        public GameState(Player2PlayerGame player1, Player2PlayerGame player2, Games gameType)
        {
            this.Id = Guid.NewGuid();
            this.players = new List<Player2PlayerGame>(2);
            this.players[0] = player1;
            this.players[1] = player2;
            this.gameType = gameType;
            this.winnerPlayer = null;
            this.turn = 0;
            this.timePlayed = 0;
            this.stateJson = string.Empty;
        }

        public GameState(Player2PlayerGame player1, Player2PlayerGame player2, Games gameType, int turn)
        {
            this.Id = Guid.NewGuid();
            this.players = new List<Player2PlayerGame>(2);
            this.players[0] = player1;
            this.players[1] = player2;
            this.gameType = gameType;
            this.winnerPlayer = null;
            this.turn = turn;
            this.timePlayed = 0;
            this.stateJson = string.Empty;
        }

        public GameState()
        {
            this.Id = Guid.Empty;
            this.players = new List<Player2PlayerGame>(2);
            this.players[0] = new Player2PlayerGame();
            this.players[1] = new Player2PlayerGame();
            this.gameType = new Games();
            this.winnerPlayer = null;
            this.turn = 0;
            this.timePlayed = 0;
            this.stateJson = string.Empty;
        }

        public GameState(Guid gameStateId, Player2PlayerGame player1, Player2PlayerGame player2, Games gameType, int turn, int timePlayed, Player2PlayerGame? winner, string jsonString)
        {
            this.Id = gameStateId;
            this.players = new List<Player2PlayerGame>(2);
            this.players[0] = player1;
            this.players[1] = player2;
            this.gameType = gameType;
            this.winnerPlayer = winner;
            this.turn = turn;
            this.timePlayed = timePlayed;
            this.stateJson = jsonString;
        }
    }
}