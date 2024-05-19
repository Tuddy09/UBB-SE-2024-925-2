using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using BoardGames.View.GameOfLife;

namespace BoardGames.View
{
    /// <summary>
    /// Interaction logic for StartView.xaml
    /// </summary>
    public partial class StartView
    {
        public StartView()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new HostJoinView());
        }

        private void StartButton_MouseEnter(object sender, MouseEventArgs e)
        {
            StartButton.BorderBrush = Brushes.DarkBlue;
            StartButton.Background = Brushes.Transparent;
        }

        private void StartButton_MouseLeave(object sender, MouseEventArgs e)
        {
            StartButton.BorderBrush = Brushes.Transparent;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Should go to Choose Game Page
            NavigationService.Navigate(new MainMenu());
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Settings());
        }
    }
}
