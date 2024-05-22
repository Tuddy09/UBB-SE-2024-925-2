using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TwoPlayerGames.Core;
using TwoPlayerGames.Domain.Auxiliary;

namespace TwoPlayerGames.Pages
{
    /// <summary>
    /// Interaction logic for DartGameGUI.xaml
    /// </summary>
    public partial class DartGameGUI : Page
    {

        public DartGameGUI()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:5001/api/2PlayerGames/CreatePlayService/");
                client.PostAsJsonAsync($"?playServiceType=OfflineGameService", Router.PlayService);
            }
            InitializeComponent();
            Loaded += DartGameGUI_Loaded;
        }

        private void DartGameGUI_Loaded(object sender, RoutedEventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:5070/");
                while (client.GetAsync("api/2PlayerGames/IsGameOver").Result.Content.ReadAsStringAsync().Result == "False")
                { }
            }
        }

        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            var confirmationDialog = new NewGameDialog();
            if (confirmationDialog.ShowDialog() == true)
            {
                if (confirmationDialog.DialogResult == true)
                {
                    MessageBox.Show("Starting a new game...");
                    this.NavigationService.Navigate(Router.MenuPage);
                }
                else
                {
                    MessageBox.Show("User declined to start a new game.");
                    confirmationDialog.Close();
                }
            }
        }

        private void PopulatePlayersData()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:5070/api/");
                PlayerStats playerStats = JsonConvert.DeserializeObject<PlayerStats>(client.GetAsync("2PlayerGames/GetStats").Result.Content.ReadAsStringAsync().Result);
                Player1Name.Text = playerStats.Player.Name;
                Player1Rank.Text = playerStats.Rank;
                Player1Trophies.Text = playerStats.Trophies.ToString();

                playerStats = JsonConvert.DeserializeObject<PlayerStats>(client.GetAsync("2PlayerGames/GetStats").Result.Content.ReadAsStringAsync().Result);
                Player2Name.Text = playerStats.Player.Name;
                Player2Rank.Text = playerStats.Rank;
                Player2Trophies.Text = playerStats.Trophies.ToString();
            }

        }

        private void SetCurrentTurn()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:5070/api/");
                CurrentPlayerTurn.Text = client.GetAsync("2PlayerGames/GetTurn").Result.Content.ReadAsStringAsync().Result;
            }
        }

        private void Slider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
        }

        private void SettingButtonClick()
        {
        }

        private void InitializeBoard()
        {
            Grid dynamicGrid = new Grid();

            for (int i = 0; i < 500; i++)
            {
                ColumnDefinition col = new ColumnDefinition();
                dynamicGrid.ColumnDefinitions.Add(col);
                RowDefinition row = new RowDefinition();
                dynamicGrid.RowDefinitions.Add(row);
            }
            for (int i = 0; i < 500; i++)
            {
                for (int j = 0; j < 500; j++)
                {
                    TextBlock txt = new TextBlock();
                    txt.Background = Brushes.DodgerBlue;

                    txt.Text = string.Empty;

                    // Set the column and row indices of the dynamically created Border
                    Grid.SetColumn(txt, j);
                    Grid.SetRow(txt, i);

                    // Add the border to the dynamic grid
                    dynamicGrid.Children.Add(txt);
                }
            }
            Grid.SetColumn(dynamicGrid, 1);
            Grid.SetRow(dynamicGrid, 0);
            parentGrid.Children.Add(dynamicGrid);
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            if (SettingsPopup.IsOpen == false)
            {
                SettingsPopup.IsOpen = true;
            }
            else
            {
                SettingsPopup.IsOpen = false;
            }
        }
        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            if (InformationPopup.IsOpen == false)
            {
                InformationPopup.IsOpen = true;
            }
            else
            {
                InformationPopup.IsOpen = false;
            }
        }
    }
}
