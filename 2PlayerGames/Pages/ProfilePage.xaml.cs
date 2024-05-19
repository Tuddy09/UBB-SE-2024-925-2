using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using TwoPlayerGames.Core;
using TwoPlayerGames.Service;

namespace TwoPlayerGames.Pages
{
    /// <summary>
    /// Interaction logic for ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        private PlayerProfileService playerProfileService;
        public ProfilePage()
        {
            playerProfileService = new PlayerProfileService();
            InitializeComponent();
            Loaded += ProfilePage_Loaded;
        }

        private void ProfilePage_Loaded(object sender, RoutedEventArgs e)
        {
            PopulateTextBlocks();
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(Router.MenuPage);
        }

        private void HistoryButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(Router.HistoryPage);
        }

        private void ChessStatsButton_Click(object sender, RoutedEventArgs e)
        {
            Router.GameType = "chess";
            this.NavigationService.Navigate(Router.StatsPage);
        }

        private void Connect4StatsButton_Click(object sender, RoutedEventArgs e)
        {
            Router.GameType = "connect";
            this.NavigationService.Navigate(Router.StatsPage);
        }

        private void ObstructionStatsButton_Click(object sender, RoutedEventArgs e)
        {
            Router.GameType = "obstruction";
            this.NavigationService.Navigate(Router.StatsPage);
        }

        private void DartsStatsButton_Click(object sender, RoutedEventArgs e)
        {
            Router.GameType = "darts";
            this.NavigationService.Navigate(Router.StatsPage);
        }

        private void PopulateTextBlocks()
        {
            // Example: Retrieving data from a data source
            List<string> textData = playerProfileService.GetProfileStats(Router.UserPlayer);

            // Assuming you have text blocks named textBlock1, textBlock2, textBlock3, etc.
            // and you want to populate them dynamically
            for (int i = 0; i < textData.Count; i++)
            {
                // Example: Populate each text block with corresponding data
                switch (i)
                {
                    case 0:
                        badgeTextInfo.Text = textData[i];
                        break;
                    case 1:
                        trophiesTextInfo.Text = textData[i];
                        break;
                    case 2:
                        favouriteGameInfo.Text = textData[i];
                        break;
                }
            }
        }
    }
}
