using System;
using System.Text.Json;
using TwoPlayerGames.Domain.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TwoPlayerGames.Repository;

namespace TwoPlayerGames.Domain.GameObjects
{
    [Serializable]
    public class ChessGame : IGame
    {
        private IBoard board;
        private GameState gameState;
        private int currentPlayer;

        public ChessGame(Player player1, Player player2, ChessModes mode, int startPlayer)
        {
            this.board = new ChessBoard([player1, player2], mode);
            this.currentPlayer = startPlayer;
            this.gameState = new GameState(player1, player2, GameStore.Games["Chess"], startPlayer);
            SaveGame();
        }

        public ChessGame(GameState unifinishedGameState)
        {
            board = new ChessBoard();
            this.gameState = unifinishedGameState;
            this.LoadGame();
        }

        [JsonIgnore]
        public GameState GameState { get => gameState; set => gameState = value; }

        [JsonIgnore]
        public Player CurrentPlayer { get => this.gameState.Players[this.currentPlayer]; }

        [JsonIgnore]
        public IBoard Board { get => board; set => board = value; }

        [JsonProperty]
        private ChessBoard JsonBoard { get => (ChessBoard)this.board; set => this.board = value; }

        [JsonProperty]
        public int CurrentPlayerIndex { get => this.currentPlayer; set => currentPlayer = value; }
        public string SaveGame()
        {
            string jsonString = JsonConvert.SerializeObject(this, Formatting.Indented);
            this.gameState.StateJson = jsonString;
            return jsonString;
        }

        public void LoadGame()
        {
            string json = this.gameState.StateJson;
            JObject jsonObject = JsonConvert.DeserializeObject<JObject>(json);
            this.GetFromJObject(jsonObject);
        }
        private void GetFromJObject(JObject jsonObject)
        {
            this.board.GetFromJToken(jsonObject["JsonBoard"]);
            this.currentPlayer = jsonObject["CurrentPlayerIndex"].ToObject<int>();
        }
    }
}
