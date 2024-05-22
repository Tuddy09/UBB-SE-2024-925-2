namespace TwoPlayerGames.Domain.Auxiliary
{
    public class GameHistory
    {
        public Player2PlayerGame[] Players { get; set;}
        public Games GameType { get; set; }
        public Player2PlayerGame Winner { get; set; }


        public GameHistory()
        {

        }
    }
}
