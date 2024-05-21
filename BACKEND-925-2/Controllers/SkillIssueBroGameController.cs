using BACKEND_925_2.Service;
using BoardGames.Model.CommonEntities;
using BoardGames.Model.SkillIssueBroEntities;
using Microsoft.AspNetCore.Mvc;

namespace BoardGames.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillIssueBroGameController : ControllerBase
    {

        private readonly GameState _gameState;

        public delegate void PawnKilledEventHandler(object sender);
        public event PawnKilledEventHandler PawnKilled;

        public SkillIssueBroGameController(GameState gameState)
        {
            _gameState = gameState;
        }

        protected virtual void OnPawnKilled()
        {
            if (PawnKilled != null)
            {
                PawnKilled(this);
            }
        }



        // GET: api/SkillIssueBroGame/GetPawns
        [HttpGet]
        [Route("GetPawns")]
        public ActionResult<IEnumerable<Pawn>> GetPawns()
        {
            /*
             * Pawns are in order Blue x 4, Yellow x 4, Green x 4, Red x 4
             */
            // return as json objects
            return _gameState.GamePawns;
        }

        // GET: api/SkillIssueBroGame/RollDice
        [HttpGet]
        [Route("RollDice")]
        public int RollDice()
        {
            return _gameState.GameBoard.GetDice().RollDice();
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
            return (_gameState.currentPlayerIndex + 1) % _gameState.Players.Count;
        }

        private int DetermineStartingPlayerIndex()
        {
            Random random = new Random();
            int playerIndex = random.Next(0, _gameState.Players.Count - 1);

            return playerIndex;
        }

        private int DeterminePawnIdBasedOnColumnAndRow(int column, int row)
        {
            foreach (Pawn pawn in _gameState.GamePawns)
            {
                Tile occupiedTile = pawn.GetOccupiedTile();
                if (occupiedTile.GetCenterXPosition() == column && occupiedTile.GetCenterYPosition() == row)
                {
                    return pawn.GetPawnId();
                }
            }
            return -1;
        }

        private Tile FindEmptyHomeTileInRange(int minId, int maxId)
        {
            List<int> occupiedTiles = new List<int>();
            foreach (Pawn pawn in _gameState.GamePawns)
            {
                Tile occupiedTile = pawn.GetOccupiedTile();
                occupiedTiles.Add(occupiedTile.GetTileId());
            }

            for (int index = minId; index <= maxId; index++)
            {
                if (!occupiedTiles.Contains(index))
                {
                    return _gameState.GameTiles[index];
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

            _gameState.GamePawns[pawnId].ChangeTile(newTile);

            _gameState.GameBoard.UpdatePawns(_gameState.GamePawns);

            OnPawnKilled();
        }

        private void MovePawn(int pawnId, int leftDiceValue, int rightDiceValue, int playerId)
        {
            int diceValue = leftDiceValue + rightDiceValue;
            if (diceValue == 0)
            {
                throw new Exception("Can't move pawn yet");
            }
            if (_gameState.GamePawns[pawnId].GetPlayer().GetPlayerId() != playerId)
            {
                throw new Exception("Not your pawn :(");
            }

            int currentTileId = _gameState.GamePawns[pawnId].GetOccupiedTile().GetTileId();

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

            GameTile newTile = _gameState.GameTiles[newTileId];

            // Eliminate pawn on the same tile if there is any
            int enemyPawnId = DeterminePawnIdBasedOnColumnAndRow(newTile.GetGridColumnInded(), newTile.GetGridRowIndex());
            if (enemyPawnId != -1)
            {
                Pawn enemyPawn = _gameState.GamePawns[enemyPawnId];
                if (enemyPawn.GetPlayer().GetPlayerId() != playerId)
                {
                    KillPawn(enemyPawnId);
                }
            }
            _gameState.GamePawns[pawnId].ChangeTile(newTile);
            _gameState.GameBoard.UpdatePawns(_gameState.GamePawns);
        }

        // GET: api/SkillIssueBroGame/MovePawnBasedOnClick
        [HttpGet]
        [Route("MovePawnBasedOnClick")]
        public ActionResult MovePawnBasedOnClick(int column, int row, int leftDiceValue, int rightDiceValue)
        {
            try
            {
                int pawnId = DeterminePawnIdBasedOnColumnAndRow(column, row);

                MovePawn(pawnId, leftDiceValue, rightDiceValue, _gameState.Players[_gameState.currentPlayerIndex].GetPlayerId());

                return Ok("Pawn moved successfully");
            }
            catch (Exception ex)
            {
                // send the exception message to the client\
                return Ok(ex.Message);
            }

        }

        // GET: api/SkillIssueBroGame/ChangeCurrentPlayer
        [HttpGet]
        [Route("ChangeCurrentPlayer")]
        public void ChangeCurrentPlayer()
        {
            _gameState.currentPlayerIndex = DetermineNextPlayerIndex();
        }

        // GET: api/SkillIssueBroGame/GetCurrentPlayerColor
        [HttpGet]
        [Route("GetCurrentPlayerColor")]
        public IActionResult GetCurrentPlayerColor()
        {
            try
            {
                string color;
                switch (_gameState.currentPlayerIndex)
                {
                    case 0: color = "b"; break;
                    case 1: color = "y"; break;
                    case 2: color = "g"; break;
                    case 3: color = "r"; break;
                    default:
                        return StatusCode(StatusCodes.Status500InternalServerError, "Invalid player index.");
                }

                return Ok(color); // Return result with 200 OK status
            }
            catch (Exception ex)
            {
                // Log the exception (using your preferred logging mechanism)
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing the request.");
            }
        }
    }
}
