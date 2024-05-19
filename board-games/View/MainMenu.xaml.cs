using System.Windows;
using System.Windows.Controls;
using BoardGames.View.Achievements;
using BoardGames.View.SkillIssueBro.Menus;

namespace BoardGames.View
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Page
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void OnSiBButtonClicked(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SkillIssueBroMainMenu());
        }

        private void OnGoLButtonClicked(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new StartView());
        }

        private void OnMyAchievementsClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AchievementPage());
        }
    }
}
