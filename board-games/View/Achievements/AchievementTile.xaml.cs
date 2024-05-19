using System.Windows;
using System.Windows.Controls;
using BoardGames.Model.CommonEntities;

namespace BoardGames.View.Achievements
{
    /// <summary>
    /// Interaction logic for AchievementTile.xaml
    /// </summary>
    public partial class AchievementTile : UserControl
    {
        public AchievementTile()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty AchievementData = DependencyProperty.Register(
            "Achievement",
            typeof(Achievement), typeof(AchievementTile),
            new PropertyMetadata(null, OnAchievementChanged));

        internal Achievement Achievement
        {
            get { return (Achievement)GetValue(AchievementData); }
            set { SetValue(AchievementData, value); }
        }

        private static void OnAchievementChanged(DependencyObject self, DependencyPropertyChangedEventArgs eventArgs)
        {
            ((AchievementTile)self).UpdateText();
        }

        private void UpdateText()
        {
            if (Achievement != null)
            {
                TitleTextBlock.Text = Achievement.GetNameOfAchievement();
                DescriptionTextBlock.Text = Achievement.GetDescriptionOfAchievement();
                GameTextBlock.Text = Achievement.GetAchievementGameCategory().ToString();
            }
        }
    }
}