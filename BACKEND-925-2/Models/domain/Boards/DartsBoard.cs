using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using TwoPlayerGames;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TwoPlayerGames
{
    public class DartsBoard : IBoard
    {
        private int width;
        private int height;
        private int centerX;
        private int centerY;
        private int boardRadius;
        private int bullseyeRadius;
        private List<IPiece> dartBolts;
        private int[] playerScores;
        private int count;

        public static Dictionary<int, int> DartScores = DartsBoard.InitializeDartScores();

        [JsonIgnore]
        public List<IPiece> Board { get => dartBolts; set => dartBolts = value; }

        [JsonProperty]
        private List<DartsBolt> JsonBoard
        {
            get
            {
                List<DartsBolt> dartsBolts = new List<DartsBolt>();
                foreach (IPiece bolt in this.dartBolts)
                {
                    dartsBolts.Add((DartsBolt)bolt);
                }
                return dartsBolts;
            }
        }

        public int GetWidth { get => width; set => width = value; }
        public int GetHeight { get => height; set => height = value; }
        public int[] Scores { get => playerScores; set => playerScores = value; }
        public int Count { get => count; set => count = value; }

        public int CenterX { get => centerX; }

        public int CenterY { get => centerY; }

        public int BoardRadius { get => boardRadius; }

        public int BullseyeRadius { get => bullseyeRadius; }

        public DartsBoard()
        {
            this.width = 500;
            this.height = 500;
            this.boardRadius = Math.Min(this.width, this.width) / 2;
            this.centerX = this.width / 2;
            this.centerY = this.height / 2;
            this.bullseyeRadius = this.boardRadius / 10;
            this.dartBolts = new List<IPiece>();
            this.playerScores = new int[2];
            this.playerScores[0] = 301;
            this.playerScores[1] = 301;
            this.count = 0;
        }

        private static Dictionary<int, int> InitializeDartScores()
        {
            Dictionary<int, int> dartScores = new Dictionary<int, int>();
            dartScores.Add(0, 20);
            dartScores.Add(1, 1);
            dartScores.Add(2, 18);
            dartScores.Add(3, 4);
            dartScores.Add(4, 13);
            dartScores.Add(5, 6);
            dartScores.Add(6, 10);
            dartScores.Add(7, 15);
            dartScores.Add(8, 2);
            dartScores.Add(9, 17);
            dartScores.Add(10, 3);
            dartScores.Add(11, 19);
            dartScores.Add(12, 7);
            dartScores.Add(13, 16);
            dartScores.Add(14, 8);
            dartScores.Add(15, 11);
            dartScores.Add(16, 14);
            dartScores.Add(17, 9);
            dartScores.Add(18, 12);
            dartScores.Add(19, 5);
            return dartScores;
        }

        public void AddPiece(IPiece bolt)
        {
            this.dartBolts.Add(bolt);
        }

        public IPiece? GetPiece(int xPosition, int yPosition)
        {
            return this.dartBolts.FirstOrDefault(bolt => bolt.XPosition == xPosition && bolt.YPosition == yPosition);
        }

        public void GetFromJToken(JToken jsonObject)
        {
            DartsBoard dartsBoard = jsonObject.ToObject<DartsBoard>();
            foreach (JToken token in jsonObject["JsonBoard"])
            {
                DartsBolt dart = token.ToObject<DartsBolt>();
                dartsBoard.Board.Add(dart);
            }
            this.Board = dartsBoard.Board;
            this.Scores = dartsBoard.Scores;
            this.Count = dartsBoard.Count;
            dartsBoard = null;
        }
    }
}