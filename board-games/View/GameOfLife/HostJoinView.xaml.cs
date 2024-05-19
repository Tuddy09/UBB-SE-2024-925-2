using System.Windows;

namespace BoardGames.View
{
    /// <summary>
    /// Interaction logic for HostJoinView.xaml
    /// </summary>
    public partial class HostJoinView
    {
        // StartView instance = new StartView();
        // Should we store PAGE INSTANCES, or create a new one for every navigation?
        // Does it deppend on the window?
        public HostJoinView()
        {
            InitializeComponent();
        }

        private void HostGameButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ChoosePlayerNumberView());
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new StartView());
        }

        private void JoinExistingGameButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new JoinExistingGameView());
        }
    }
}
