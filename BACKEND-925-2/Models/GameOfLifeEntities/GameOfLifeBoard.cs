using BoardGames.Model.CommonEntities;

namespace BoardGames.Model.GameOfLife
{
    public class GameOfLifeBoard : Games
    {
        private readonly Spinner spinner;
        private List<Player> players;

        public GameOfLifeBoard()
        {
            spinner = new Spinner();
            players = new List<Player>();
        }

        public override List<Player> GetPlayers()
        {
            return players;
        }

        public int SpinSpinner()
        {
            return spinner.RollSpinner();
        }
    }
}
