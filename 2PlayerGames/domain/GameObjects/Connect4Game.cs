using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using _2PlayerGames.Core;

namespace TwoPlayerGames.Domain.GameObjects
{
    public class Connect4Game : IGame
    {
        private IBoard board;
        private GameState gameState;
        private int currentPlayer;

        public Connect4Game(Player player1, Player player2)
        {
            this.board = new Connect4Board();
            this.currentPlayer = 0;
            this.gameState = new GameState(player1, player2, GameStore.Games["Connect4"]);
            this.SaveGame();
        }

        public Connect4Game(GameState unifinishedGameState)
        {
            this.board = new Connect4Board();
            this.gameState = unifinishedGameState;
            this.LoadGame();
        }

        [JsonIgnore]
        public IBoard Board { get => board; set => board = value; }
        [JsonIgnore]
        public GameState GameState { get => gameState; set => gameState = value; }

        [JsonIgnore]
        public Player CurrentPlayer => this.gameState.Players[this.currentPlayer];

        [JsonProperty]
        public Connect4Board JsonBoard { get => (Connect4Board)this.board; set => this.board = value; }

        [JsonProperty]
        public int CurrentPlayerIndex { get => this.currentPlayer; set => this.currentPlayer = value; }

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
