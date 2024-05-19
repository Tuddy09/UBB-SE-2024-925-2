using System.Windows;
using System.Windows.Controls;

namespace BoardGames.View.GameOfLife
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Page
    {
        public Settings()
        {
            InitializeComponent();
            // !!! HERE we should bring the existing settings from controller to GUI
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new StartView());
        }

        private void ChoosePawnStyleButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PawnStyle());
        }

        private void ChooseBoardStyleButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new BoardStyle());
        }

        private void ChooseCardSetStyleButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CardSetStyle());
        }
    }
}
