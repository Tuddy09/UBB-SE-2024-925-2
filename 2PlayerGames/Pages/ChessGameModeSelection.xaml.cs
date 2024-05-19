using System.Windows;
using System.Windows.Controls;
using TwoPlayerGames.Core;

namespace TwoPlayerGames.Pages
{
    /// <summary>
    /// Interaction logic for ChessGameModeSelection.xaml
    /// </summary>
    public partial class ChessGameModeSelection : Page
    {
        public ChessGameModeSelection()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void BulletButton_Click(object sender, RoutedEventArgs e)
        {
            Router.ChessMode = "BULLET";
            this.NavigationService.Navigate(Router.OpponentPage);
        }

        private void BlitzButton_Click(object sender, RoutedEventArgs e)
        {
            Router.ChessMode = "BLITZ";
            this.NavigationService.Navigate(Router.OpponentPage);
        }

        private void RapidButton_Click(object sender, RoutedEventArgs e)
        {
            Router.ChessMode = "RAPID";
            this.NavigationService.Navigate(Router.OpponentPage);
        }
    }
}
