namespace BoardGames.Model.CommonEntities
{
    public class Player
    {
        public int id { get; set; }
        public string name { get; set; }

        public Player(int playerId, string playerName)
        {
            id = playerId;
            name = playerName;
        }

        public string GetPlayerName()
        {
            return name;
        }
        public int GetPlayerId()
        {
            return id;
        }
    }
}
