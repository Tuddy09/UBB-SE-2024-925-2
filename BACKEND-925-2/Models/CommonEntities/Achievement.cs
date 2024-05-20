namespace BoardGames.Model.CommonEntities
{
    public enum GameCategory
    {
        SkillIssueBro = 0,
        GameOfLife = 1,
    }

    public class Achievement
    {
        private string name;
        private string description;
        private GameCategory gameCategory;

        public Achievement(string name, string description, GameCategory gameCategory)
        {
            this.name = name;
            this.description = description;
            this.gameCategory = gameCategory;
        }

        public string GetNameOfAchievement()
        {
            return name;
        }

        public string GetDescriptionOfAchievement()
        {
            return description;
        }

        public GameCategory GetAchievementGameCategory()
        {
            return gameCategory;
        }
    }
}