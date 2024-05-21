namespace BoardGames.Model.CommonEntities
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Player(int playerId, string playerName)
        {
            Id = playerId;
            Name = playerName;
        }

        public string GetPlayerName()
        {
            return Name;
        }
        public int GetPlayerId()
        {
            return Id;
        }
    }
}
