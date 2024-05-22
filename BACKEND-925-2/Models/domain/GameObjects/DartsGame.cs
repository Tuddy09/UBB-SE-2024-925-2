using System;
using System.Text.Json;
using TwoPlayerGames.Repository;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TwoPlayerGames
{
    [Serializable]
    public class DartsGame : IGame
    {
        private IBoard board;
        private int currentPlayer;
        private GameState gameState;

        [JsonIgnore]
        public GameState GameState { get => gameState; set => gameState = value; }

        [JsonIgnore]
        public Player CurrentPlayer { get => this.gameState.Players[this.currentPlayer]; }

        [JsonIgnore]
        public IBoard Board { get => board; set => board = (DartsBoard)value; }

        [JsonProperty]
        private DartsBoard JsonBoard { get => (DartsBoard)this.board; set => this.board = value; }

        [JsonProperty]
        public int CurrentPlayerIndex { get => this.currentPlayer; }

        public DartsGame(Player player1, Player player2)
        {
            this.board = new DartsBoard();
            this.currentPlayer = 0;
            this.gameState = new GameState(player1, player2, GameStore.Games["Darts"]);
            SaveGame();
        }

        public DartsGame(GameState unifinishedGameState)
        {
            this.gameState = unifinishedGameState;
            this.board = new DartsBoard();
            this.LoadGame();
        }

        public string SaveGame()
        {
            string jsonString = JsonConvert.SerializeObject(this, Formatting.Indented);
            this.gameState.StateJson = jsonString;
            return jsonString;
        }

        public void LoadGame()
        {
            string json = this.gameState.StateJson;
            JObject obj = JsonConvert.DeserializeObject<JObject>(json);
            this.GetFromJObject(obj);
        }

        private void GetFromJObject(JObject obj)
        {
            this.board.GetFromJToken(obj["JsonBoard"]);
            this.currentPlayer = obj["CurrentPlayerIndex"].ToObject<int>();
        }
    }
}