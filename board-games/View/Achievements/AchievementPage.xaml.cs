using System.Windows.Controls;
using BoardGames.Model.CommonEntities;

namespace BoardGames.View.Achievements
{
    /// <summary>
    /// Interaction logic for AchievementPage.xaml
    /// </summary>
    public partial class AchievementPage : UserControl
    {
        public AchievementPage()
        {
            InitializeComponent();
            LoadAchivements();
        }

        private void AddAchievementToList(Achievement achievement)
        {
            AchievementList.Children.Add(
            new AchievementTile
            {
                Achievement = achievement
            });
        }

        private void LoadAchivements()
        {
            // TODO: read this from somewhere else.
            AddAchievementToList(new Achievement("Rich man", "Be the first to reach $100k.", GameCategory.GameOfLife));
            AddAchievementToList(new Achievement("Strategist", "Knock 3 opponent pawns off the final tiles in a match.", GameCategory.SkillIssueBro));
        }
    }
}