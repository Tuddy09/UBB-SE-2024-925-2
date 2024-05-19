using BoardGames.Model.CommonEntities;
using BoardGames.Model.SkillIssueBroEntities;
using Board_games.Model.Interfaces;

namespace BoardGames.Controller
{
    public class SkillIssueBroGameController
    {
        private GameBoard gameBoard;
        private List<Player> players;
        private List<GameTile> gameTiles;
        private List<Pawn> gamePawns;

        private static int generatedPawnIds = 0;
        private int currentPlayerIndex;

        public delegate void PawnKilledEventHandler(object sender);
        public event PawnKilledEventHandler PawnKilled;

        public SkillIssueBroGameController(List<Player> players)
        {
            this.players = players;
            gameTiles = GenerateTiles();
            gamePawns = new List<Pawn>();
            GeneratePawns();

            // id is subject to change; can do an insert first and then retrieve the id (bcuz identity)
            // and create the object
            currentPlayerIndex = DetermineStartingPlayerIndex();    // This is unused later in the code.
            gameBoard = new GameBoard(gameTiles, gamePawns, this.players);
        }

        protected virtual void OnPawnKilled()
        {
            if (PawnKilled != null)
            {
                PawnKilled(this);
            }
        }

        private List<Pawn> GenerateBluePawns()
        {
            List<Pawn> bluePawns = new List<Pawn>();
            // 4 pawns on tiles 0-3
            for (int index = 0; index < 4; index++)
            {
                Pawn newPawn = new Pawn(generatedPawnIds, gameTiles[index]);
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
                Pawn newPawn = new Pawn(generatedPawnIds, gameTiles[index]);
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
                Pawn newPawn = new Pawn(generatedPawnIds, gameTiles[i]);
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
                Pawn newPawn = new Pawn(generatedPawnIds, gameTiles[i]);
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

            switch (players.Count)
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
                bluePawn.SetAssociatedPlayer(players[0]);
                gamePawns.Add(bluePawn);
            }

            foreach (Pawn yellowPawn in yellowPawns)
            {
                yellowPawn.SetAssociatedPlayer(players[1]);
                gamePawns.Add(yellowPawn);
            }

            if (players.Count > 2)
            {
                foreach (Pawn greenPawn in greenPawns)
                {
                    greenPawn.SetAssociatedPlayer(players[2]);
                    gamePawns.Add(greenPawn);
                }
            }
            if (players.Count > 3)
            {
                foreach (Pawn redPawn in redPawns)
                {
                    redPawn.SetAssociatedPlayer(players[3]);
                    gamePawns.Add(redPawn);
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
        public List<Pawn> GetPawns()
        {
            /*
             * Pawns are in order Blue x 4, Yellow x 4, Green x 4, Red x 4
             */
            return gamePawns;
        }
        public void SetPawns(List<Pawn> newPawns)
        {
            this.gamePawns = newPawns;
        }
            public int RollDice()
        {
            return gameBoard.GetDice().RollDice();
        }

        private int ComputeNewTileId(string pawnColor, int currentTileId, int diceValue)
        {
            // 16 first path tile
            if (currentTileId <= 3)
            {
                // blue corner
                if (diceValue == 12)
                {
                    return 16;
                }

                return diceValue + 16 - 1;
            }
            else if (currentTileId <= 7)
            {
                // yellow corner
                if (diceValue == 12)
                {
                    return 26;
                }

                return diceValue + 26 - 1;
            }
            else if (currentTileId <= 11)
            {
                if (diceValue == 12)
                {
                    return 36;
                }

                return diceValue + 36 - 1;
            }
            else if (currentTileId <= 15)
            {
                if (diceValue == 12)
                {
                    return 46;
                }
                return diceValue + 46 - 1;
            }

            // compute possible new tile
            int newTileId = currentTileId + diceValue;

            // should enter cross
            switch (pawnColor)
            {
                case "b":
                    if (currentTileId >= 56 && currentTileId <= 59)
                    {
                        if (newTileId <= 59)
                        {
                            return newTileId;
                        }
                        return currentTileId;
                    }
                    if (currentTileId <= 55 && newTileId > 55)
                    {
                        if (newTileId <= 59)
                        {
                            return newTileId;
                        }
                        else
                        {
                            return currentTileId;
                        }
                    }
                    break;
                case "y":
                    if (currentTileId >= 60 && currentTileId <= 63)
                    {
                        if (newTileId <= 63)
                        {
                            return newTileId;
                        }
                        return currentTileId;
                    }
                    if (currentTileId <= 25 && newTileId > 25)
                    {
                        if (newTileId - 26 + 60 <= 63)
                        {
                            return newTileId - 26 + 60;
                        }
                        else
                        {
                            return currentTileId;
                        }
                    }
                    break;

                case "g":
                    if (currentTileId >= 64 && currentTileId <= 67)
                    {
                        if (newTileId <= 67)
                        {
                            return newTileId;
                        }
                        return currentTileId;
                    }
                    if (currentTileId <= 35 && newTileId > 35)
                    {
                        if (newTileId - 36 + 64 <= 67)
                        {
                            return newTileId - 36 + 64;
                        }
                        else
                        {
                            return currentTileId;
                        }
                    }
                    break;

                case "r":
                    if (currentTileId >= 68 && currentTileId <= 71)
                    {
                        if (newTileId <= 71)
                        {
                            return newTileId;
                        }
                        return currentTileId;
                    }
                    if (currentTileId <= 45 && newTileId > 45)
                    {
                        if (newTileId - 46 + 68 <= 71)
                        {
                            return newTileId - 46 + 68;
                        }
                        else
                        {
                            return currentTileId;
                        }
                    }
                    break;
            }

            // Return tile only in valid range.
            return (newTileId > 55) ? (newTileId % 56) + 16 : newTileId;
        }

        private string PawnColor(int pawnId)
        {
            if (pawnId < 4)
            {
                return "b";
            }
            if (pawnId < 8)
            {
                return "y";
            }
            if (pawnId < 12)
            {
                return "g";
            }
            return "r";
        }

        private int DetermineNextPlayerIndex()
        {
            return (currentPlayerIndex + 1) % players.Count;
        }

        private int DetermineStartingPlayerIndex()
        {
            Random random = new Random();
            int playerIndex = random.Next(0, players.Count - 1);

            return playerIndex;
        }

        public int DeterminePawnIdBasedOnColumnAndRow(int column, int row)
        {
            foreach (Pawn pawn in gamePawns)
            {
                ITile occupiedTile = pawn.GetOccupiedTile();
                if (occupiedTile.GetCenterXPosition() == column && occupiedTile.GetCenterYPosition() == row)
                {
                    return pawn.GetPawnId();
                }
            }
            return -1;
        }

        public Tile FindEmptyHomeTileInRange(int minId, int maxId)
        {
            List<int> occupiedTiles = new List<int>();
            foreach (Pawn pawn in gamePawns)
            {
                ITile occupiedTile = pawn.GetOccupiedTile();
                occupiedTiles.Add(occupiedTile.GetTileId());
            }

            for (int index = minId; index <= maxId; index++)
            {
                if (!occupiedTiles.Contains(index))
                {
                    return gameTiles[index];
                }
            }
            throw new Exception("Can't revive pawn??");
        }

        private void KillPawn(int pawnId)
        {
            // current player s pawn stepped on this one so it dies
            string pawnColor = PawnColor(pawnId);
            Tile newTile = null;
            switch (pawnColor)
            {
                case "b":
                    newTile = FindEmptyHomeTileInRange(0, 3);
                    break;
                case "y":
                    newTile = FindEmptyHomeTileInRange(4, 7);
                    break;
                case "g":
                    newTile = FindEmptyHomeTileInRange(8, 11);
                    break;
                default:
                    newTile = FindEmptyHomeTileInRange(12, 15);
                    break;
            }

            gamePawns[pawnId].ChangeTile(newTile);

            gameBoard.UpdatePawns(gamePawns);

            OnPawnKilled();
        }

        public void MovePawn(int pawnId, int leftDiceValue, int rightDiceValue, int playerId)
        {
            int diceValue = leftDiceValue + rightDiceValue;
            if (diceValue == 0)
            {
                throw new Exception("Can't move pawn yet");
            }
            if (gamePawns[pawnId].GetPlayer().GetPlayerId() != playerId)
            {
                throw new Exception("Not your pawn :(");
            }

            int currentTileId = gamePawns[pawnId].GetOccupiedTile().GetTileId();

            if (currentTileId < 16)
            {
                // pawn still on home tiles
                if (rightDiceValue != 6 || leftDiceValue != 6)
                {
                    throw new Exception("You have to roll two 6s!");
                }
            }

            int newTileId = ComputeNewTileId(PawnColor(pawnId), currentTileId, diceValue);

            if (newTileId == currentTileId)
            {
                throw new Exception("Pawn cannot go further");
            }

            GameTile newTile = gameTiles[newTileId];

            // Eliminate pawn on the same tile if there is any
            int enemyPawnId = DeterminePawnIdBasedOnColumnAndRow(newTile.GetGridColumnInded(), newTile.GetGridRowIndex());
            if (enemyPawnId != -1)
            {
                Pawn enemyPawn = gamePawns[enemyPawnId];
                if (enemyPawn.GetPlayer().GetPlayerId() != playerId)
                {
                    KillPawn(enemyPawnId);
                }
            }
            gamePawns[pawnId].ChangeTile(newTile);
            gameBoard.UpdatePawns(gamePawns);
        }

        public void MovePawnBasedOnClick(int column, int row, int leftDiceValue, int rightDiceValue)
        {
            int pawnId = DeterminePawnIdBasedOnColumnAndRow(column, row);

            MovePawn(pawnId, leftDiceValue, rightDiceValue, players[currentPlayerIndex].GetPlayerId());
        }

        public void ChangeCurrentPlayer()
        {
            currentPlayerIndex = DetermineNextPlayerIndex();
        }

        public string GetCurrentPlayerColor()
        {
            switch (currentPlayerIndex)
            {
                case 0:
                    return "b";
                case 1:
                    return "y";
                case 2:
                    return "g";
                case 3:
                    return "r";
                default:
                    return "none";
            }
        }
    }
}
