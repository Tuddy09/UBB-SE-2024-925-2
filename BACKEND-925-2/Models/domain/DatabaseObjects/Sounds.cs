using System;

namespace TwoPlayerGames
{
    public class Sounds
    {
        private Guid id;
        private string name;
        private string category;
        private string path;

        public string Name { get => name; set => name = value; }
        public Guid Id { get => id; set => id = value; }
        public string Category { get => category; set => category = value; }

        public string Path { get => path; set => path = value; }

        public Sounds(string name, string category, string path)
        {
            this.id = Guid.NewGuid();
            this.name = name;
            this.category = category;
            this.path = path;
        }

        public Sounds(Guid id, string name, string category, string path)
        {
            this.id = id;
            this.name = name;
            this.category = category;
            this.path = path;
        }

        public Sounds()
        {
            this.id = Guid.Empty;
            this.name = string.Empty;
            this.category = string.Empty;
            this.path = string.Empty;
        }
    }
}
