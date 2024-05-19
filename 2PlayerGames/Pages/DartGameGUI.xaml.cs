using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TwoPlayerGames.Core;
using TwoPlayerGames.Domain.Auxiliary;
using TwoPlayerGames.Service;

namespace TwoPlayerGames.Pages
{
    /// <summary>
    /// Interaction logic for DartGameGUI.xaml
    /// </summary>
    public partial class DartGameGUI : Page
    {
        private IPlayService playService;
        private InGameService inGameService;

        public DartGameGUI()
        {
            InitializeComponent();
            playService = Router.PlayService;
            inGameService = Router.InGameService;
            Loaded += DartGameGUI_Loaded;
        }

        private void DartGameGUI_Loaded(object sender, RoutedEventArgs e)
        {
            while (!playService.IsGameOver())
            {
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
            PlayerStats playerStats = inGameService.GetStats(Router.UserPlayer);
            Player1Name.Text = playerStats.Player.Name;
            Player1Rank.Text = playerStats.Rank;
            Player1Trophies.Text = playerStats.Trophies.ToString();

            playerStats = inGameService.GetStats(Router.OpponentPlayer);
            Player2Name.Text = playerStats.Player.Name;
            Player2Rank.Text = playerStats.Rank;
            Player2Trophies.Text = playerStats.Trophies.ToString();

        }

        private void SetCurrentTurn()
        {
            CurrentPlayerTurn.Text = playService.GetTurn().ToString();
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
