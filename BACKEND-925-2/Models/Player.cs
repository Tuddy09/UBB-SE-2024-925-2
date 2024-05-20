namespace BACKEND_925_2.Models
{
    public class Player
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Ip { get; set; }
        public int? Port { get; set; }

        public Player(Guid id, string name, string? ip, int? port)
        {
            Id = id;
            Name = name;
            Ip = ip;
            Port = port;
        }

        public Player()
        {
            Id = Guid.Empty;
            Name = string.Empty;
            Ip = string.Empty;
            Port = 0;
        }

        public override string ToString()
        {
            return Name;
        }

        public static Player Null()
        {
            return new Player(Guid.Empty, "Null", "Null", 0);
        }

    }
}
