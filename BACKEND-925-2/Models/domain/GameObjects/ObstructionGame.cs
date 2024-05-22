using System;
using System.Text.Json;
using TwoPlayerGames.Repository;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TwoPlayerGames.Domain.GameObjects
{
    [Serializable]
    public class ObstructionGame : IGame
    {
        private IBoard board;
        private GameState gameState;
        private int currentPlayer;

        public ObstructionGame(Player2PlayerGame player1, Player2PlayerGame player2, int width, int heigth)
        {
            this.board = new ObstructionBoard(width, heigth);
            this.currentPlayer = 0;
            this.gameState = new GameState(player1, player2, GameStore.Games["Obstruction"]);
            this.SaveGame();
        }

        public ObstructionGame(GameState unifinishedGameState)
        {
            this.board = new ObstructionBoard();
            this.gameState = unifinishedGameState;
            this.LoadGame();
        }

        [JsonIgnore]
        public IBoard Board { get => board; set => board = value; }
        [JsonIgnore]
        public GameState GameState { get => gameState; set => gameState = value; }

        [JsonIgnore]
        public Player2PlayerGame CurrentPlayer => this.gameState.Players[this.currentPlayer];

        [JsonProperty]
        public ObstructionBoard JsonBoard { get => (ObstructionBoard)this.board; set => this.board = value; }

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
