using System.Windows;
using System.Windows.Controls;

namespace BoardGames.View
{
    /// <summary>
    /// Interaction logic for ChoosePlayerNumberView.xaml
    /// </summary>
    public partial class ChoosePlayerNumberView : Page
    {
        // !!!! TODO: add a CONTINUE or START button here as well!!!!
        private int playerNumber = 0;
        public ChoosePlayerNumberView()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new HostJoinView());
        }

        private void UpArrowButton_Click(object sender, RoutedEventArgs e)
        {
            if (playerNumber + 1 <= 6)
            {
                playerNumber++;
                PlayerNumberTextBlock.Text = playerNumber.ToString();
            }
        }

        private void DownArrowButton_Click(object sender, RoutedEventArgs e)
        {
            if (playerNumber - 1 >= 0)
            {
                playerNumber--;
                PlayerNumberTextBlock.Text = playerNumber.ToString();
            }
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Host_WaitingForPlayersView(playerNumber));
        }
    }
}
