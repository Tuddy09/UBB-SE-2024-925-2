using BoardGames.Model.CommonEntities;
using BoardGames.Model.SkillIssueBroEntities;

namespace BACKEND_925_2.Service
{
    public class GameState
    {
        public List<Player> Players { get; private set; }
        public List<Pawn> GamePawns { get; private set; }
        public List<GameTile> GameTiles { get; private set; }
        public GameBoard GameBoard { get; private set; }

        public static int generatedPawnIds = 0;
        public int currentPlayerIndex { get; set; }

        public GameState()
        {
            Players = new List<Player>
        {
            new Player(1, "Egg"),
            new Player(2, "Mario"),
            new Player(3, "Gigi"),
            new Player(4, "Flower")
        };
            GameTiles = GenerateTiles();
            GamePawns = new List<Pawn>();
            GeneratePawns();

            GameBoard = new GameBoard(GameTiles, GamePawns, Players);
        }

        private List<Pawn> GenerateBluePawns()
        {
            List<Pawn> bluePawns = new List<Pawn>();
            // 4 pawns on tiles 0-3
            for (int index = 0; index < 4; index++)
            {
                Pawn newPawn = new Pawn(generatedPawnIds, GameTiles[index]);
                generatedPawnIds++;
                bluePawns.Add(newPawn);
            }

            return bluePawns;
        }

        private List<Pawn> GenerateYellowPawns()
        {
            List<Pawn> yellowPawns = new List<Pawn>();
            for (int index = 4; index < 8; index++)
            {
                Pawn newPawn = new Pawn(generatedPawnIds, GameTiles[index]);
                generatedPawnIds++;
                yellowPawns.Add(newPawn);
            }
            return yellowPawns;
        }

        private List<Pawn> GenerateGreenPawns()
        {
            List<Pawn> greenPawns = new List<Pawn>();
            for (int i = 8; i < 12; i++)
            {
                Pawn newPawn = new Pawn(generatedPawnIds, GameTiles[i]);
                generatedPawnIds++;
                greenPawns.Add(newPawn);
            }
            return greenPawns;
        }

        private List<Pawn> GenerateRedPawns()
        {
            List<Pawn> redPawns = new List<Pawn>();
            for (int i = 12; i < 16; i++)
            {
                Pawn newPawn = new Pawn(generatedPawnIds, GameTiles[i]);
                generatedPawnIds++;
                redPawns.Add(newPawn);
            }
            return redPawns;
        }

        private void GeneratePawns()
        {
            // associate pawns depending on the number of players
            List<string> colors = new List<string> { "Blue", "Yellow", "Green", "Red" };
            List<Pawn> bluePawns, yellowPawns, greenPawns, redPawns;
            bluePawns = new List<Pawn>();
            yellowPawns = new List<Pawn>();
            greenPawns = new List<Pawn>();
            redPawns = new List<Pawn>();

            switch (Players.Count)
            {
                case 2:
                    bluePawns = GenerateBluePawns();
                    yellowPawns = GenerateYellowPawns();
                    break;
                case 3:
                    bluePawns = GenerateBluePawns();
                    yellowPawns = GenerateYellowPawns();
                    greenPawns = GenerateGreenPawns();
                    break;
                case 4:
                    bluePawns = GenerateBluePawns();
                    yellowPawns = GenerateYellowPawns();
                    greenPawns = GenerateGreenPawns();
                    redPawns = GenerateRedPawns();
                    break;
            }
            foreach (Pawn bluePawn in bluePawns)
            {
                bluePawn.SetAssociatedPlayer(Players[0]);
                GamePawns.Add(bluePawn);
            }

            foreach (Pawn yellowPawn in yellowPawns)
            {
                yellowPawn.SetAssociatedPlayer(Players[1]);
                GamePawns.Add(yellowPawn);
            }

            if (Players.Count > 2)
            {
                foreach (Pawn greenPawn in greenPawns)
                {
                    greenPawn.SetAssociatedPlayer(Players[2]);
                    GamePawns.Add(greenPawn);
                }
            }
            if (Players.Count > 3)
            {
                foreach (Pawn redPawn in redPawns)
                {
                    redPawn.SetAssociatedPlayer(Players[3]);
                    GamePawns.Add(redPawn);
                }
            }
        }

        private List<GameTile> GenerateTiles()
        {
            List<GameTile> gameTiles =
            [
                // the blue corner
                new GameTile(0, 9, 0), // id, row, column
                new GameTile(1, 9, 1),
                new GameTile(2, 10, 0),
                new GameTile(3, 10, 1),

                // the yellow corner
                new GameTile(4, 0, 0),
                new GameTile(5, 0, 1),
                new GameTile(6, 1, 0),
                new GameTile(7, 1, 1),

                // the green corner
                new GameTile(8, 0, 9),
                new GameTile(9, 0, 10),
                new GameTile(10, 1, 9),
                new GameTile(11, 1, 10),

                // the red corner
                new GameTile(12, 9, 9),
                new GameTile(13, 9, 10),
                new GameTile(14, 10, 9),
                new GameTile(15, 10, 10),
            ];

            int index;
            int count = 16;
            for (index = 10; index >= 6; index--)
            {
                gameTiles.Add(new GameTile(count++, index, 4));
            }
            for (index = 3; index >= 0; index--)
            {
                gameTiles.Add(new GameTile(count++, 6, index));
            }
            for (index = 5; index >= 4; index--)
            {
                gameTiles.Add(new GameTile(count++, index, 0));
            }
            for (index = 1; index <= 4; index++)
            {
                gameTiles.Add(new GameTile(count++, 4, index));
            }
            for (index = 3; index >= 0; index--)
            {
                gameTiles.Add(new GameTile(count++, index, 4));
            }
            for (index = 5; index <= 6; index++)
            {
                gameTiles.Add(new GameTile(count++, 0, index));
            }
            for (index = 1; index <= 4; index++)
            {
                gameTiles.Add(new GameTile(count++, index, 6));
            }
            for (index = 7; index <= 10; index++)
            {
                gameTiles.Add(new GameTile(count++, 4, index));
            }
            for (index = 5; index <= 6; index++)
            {
                gameTiles.Add(new GameTile(count++, index, 10));
            }
            for (index = 9; index >= 6; index--)
            {
                gameTiles.Add(new GameTile(count++, 6, index));
            }
            for (index = 7; index <= 10; index++)
            {
                gameTiles.Add(new GameTile(count++, index, 6));
            }
            gameTiles.Add(new GameTile(count++, 10, 5));
            // the crosses
            // the blue cross
            for (index = 9; index >= 6; index--)
            {
                gameTiles.Add(new GameTile(count++, index, 5));
            }
            // the yellow cross
            for (index = 1; index <= 4; index++)
            {
                gameTiles.Add(new GameTile(count++, 5, index));
            }
            // the green cross
            for (index = 1; index <= 4; index++)
            {
                gameTiles.Add(new GameTile(count++, index, 5));
            }
            // the red cross
            for (index = 9; index >= 6; index--)
            {
                gameTiles.Add(new GameTile(count++, 5, index));
            }

            return gameTiles;
        }
    }

}
