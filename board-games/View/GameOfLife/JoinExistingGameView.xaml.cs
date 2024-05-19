using System.Windows;
using System.Windows.Controls;

namespace BoardGames.View
{
    /// <summary>
    /// Interaction logic for JoinExistingGameView.xaml
    /// </summary>
    public partial class JoinExistingGameView : Page
    {
        public JoinExistingGameView()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new HostJoinView());
        }

        private void JoinGameButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new GameOfLife_MainWindow());
        }
    }
}
