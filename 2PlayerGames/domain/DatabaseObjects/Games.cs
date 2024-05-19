using System;

namespace TwoPlayerGames
{
    public class Games
    {
        private Guid id;
        private string name;
        private string category;

        public string Name { get => name; set => name = value; }
        public Guid Id { get => id; set => id = value; }
        public string Category { get => category; set => category = value; }

        public Games(string name, string category)
        {
            this.id = Guid.NewGuid();
            this.name = name;
            this.category = category;
        }

        public Games(Guid id, string name, string category)
        {
            this.id = id;
            this.name = name;
            this.category = category;
        }

        public Games()
        {
            this.id = Guid.Empty;
            this.name = string.Empty;
            this.category = string.Empty;
        }
    }
}