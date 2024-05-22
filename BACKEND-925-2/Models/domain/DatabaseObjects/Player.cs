using System;
using TwoPlayerGames.Domain.DatabaseObjects;

namespace TwoPlayerGames
{
    public class Player2PlayerGame
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Ip { get; set; }
        public int Port { get; set; }
        public List<GameStats> GameStats { get; set; }
        public List<PlayerQueue> Queues { get; set; }


        public Player2PlayerGame(string name, string ip, int port)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.Ip = ip;
            this.Port = port;
        }

        public Player2PlayerGame(Guid id, string name, string ip, int port)
        {
            this.Id = id;
            this.Name = name;
            this.Ip = ip;
            this.Port = port;
        }

        public Player2PlayerGame()
        {
            this.Id = Guid.Empty;
            this.Name = string.Empty;
            this.Ip = string.Empty;
            this.Port = 0;
        }

        public override string ToString()
        {
            return this.Name;
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
            return this.Id == p.Id;
        }
    }
}