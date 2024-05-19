using System;
using System.Windows;
using System.Windows.Controls;
using TwoPlayerGames.Core;
using TwoPlayerGames.Repository;

namespace TwoPlayerGames
{
    /// <summary>
    /// Interaction logic for MainWindow1.xaml
    /// </summary>
    public partial class MainWindow1 : UserControl
    {

        public MainWindow1()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;

        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Router.UserPlayer = new Player(Guid.NewGuid(), "David", "0.0.0.0", 69);
            PlayerRepository.AddPlayer(Router.UserPlayer);
            MainFrame.NavigationService.Navigate(Router.MenuPage);
        }

    }
}