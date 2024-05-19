using System.Windows;
using System.Windows.Controls;

namespace BoardGames.View
{
    /// <summary>
    /// Interaction logic for Host_WaitingForPlayersView.xaml
    /// </summary>
    public partial class Host_WaitingForPlayersView : Page
    {
        private int connectedPlayers = 0;
        private int playerNumber;
        private string gameID = string.Empty;
        public Host_WaitingForPlayersView(int playerNumber)
        {
            InitializeComponent();
            this.playerNumber = playerNumber;
            Loaded += Host_WaitingForPlayersView_Loaded;
        }

        private void Host_WaitingForPlayersView_Loaded(object sender, RoutedEventArgs e)
        {
            // _connectedPlayers = GetConnectedPlayers();
            ConnectedPlayersTextBlock.Text = connectedPlayers.ToString() + "/" + playerNumber.ToString();
            // _gameID = GetGameID();
            CodeTextBlock.Text = "#" + gameID.ToString();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new GameOfLife_MainWindow());
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ChoosePlayerNumberView());
            // !!! When backing out of "waiting for players...", we should close that
            // game room; we should STOP WAITING FOR PLAYERS and make the code UNAVAILABLE
        }
    }
}
