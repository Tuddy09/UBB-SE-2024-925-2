using System.Windows;
using System.Windows.Controls;
using BoardGames.View;

namespace BoardGames
{
    /// <summary>
    /// Interaction logic for MainWindow2.xaml
    /// </summary>
    public partial class MainWindow2 : UserControl
    {
        public MainWindow2()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new MainMenu());
        }
    }
}