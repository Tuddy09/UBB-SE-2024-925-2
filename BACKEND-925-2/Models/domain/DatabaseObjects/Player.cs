using System;

namespace TwoPlayerGames
{
    [Serializable]
    public class Player2PlayerGame
    {
        private Guid id;
        private string name;
        private string? ip;
        private int? port;

        public string Name { get => name; set => name = value; }
        public Guid Id { get => id; set => id = value; }
        public string? Ip { get => ip; set => ip = value; }
        public int? Port { get => port; set => port = value; }

        public Player2PlayerGame(string name, string? ip, int? port)
        {
            this.id = Guid.NewGuid();
            this.name = name;
            this.ip = ip;
            this.port = port;
        }

        public Player2PlayerGame(Guid id, string name, string? ip, int? port)
        {
            this.id = id;
            this.name = name;
            this.ip = ip;
            this.port = port;
        }

        public Player2PlayerGame()
        {
            this.id = Guid.Empty;
            this.name = string.Empty;
            this.ip = string.Empty;
            this.port = 0;
        }

        public override string ToString()
        {
            return this.name;
        }

        public (int nrParameters, object[] parameters) Play(IGame newGame)
        {
            throw new NotImplementedException();
        }

        public static Player2PlayerGame Null()
        {
            return new Player2PlayerGame(Guid.Empty, "Null", "Null", 0);
        }

        public static Player2PlayerGame Bot()
        {
            return new Player2PlayerGame(Guid.Empty, "Bot", "Null", 0);
        }
        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            Player2PlayerGame p = (Player2PlayerGame)obj;
            return this.id == p.Id;
        }
    }
}