using System.Windows;
using System.Windows.Controls;

namespace BoardGames.View
{
    /// <summary>
    /// Interaction logic for Join_WaitingForPlayersView.xaml
    /// </summary>
    public partial class Join_WaitingForPlayersView : Page
    {
        public Join_WaitingForPlayersView()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new JoinExistingGameView());
        }
    }
}
