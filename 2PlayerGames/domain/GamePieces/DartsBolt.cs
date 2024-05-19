using Newtonsoft.Json;

namespace TwoPlayerGames
{
    public class DartsBolt : IPiece
    {
        private int xPosition;
        private int yPosition;
        private Player player;

        [JsonProperty]
        public int XPosition
        {
            get { return xPosition; }
            set { xPosition = value; }
        }

        [JsonProperty]
        public int YPosition
        {
            get { return yPosition; }
            set { yPosition = value; }
        }

        [JsonProperty]
        public Player Player
        {
            get { return player; }
            set { player = value; }
        }

        public DartsBolt(int xPosition, int yPosition, Player player)
        {
            this.xPosition = xPosition;
            this.yPosition = yPosition;
            this.player = player;
        }
    }
}