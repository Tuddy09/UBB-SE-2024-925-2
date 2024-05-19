using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MainProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void project1_btn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
            TwoPlayerGames.MainWindow1 mainWindow1 = new TwoPlayerGames.MainWindow1();
            if (mainWindow != null)
            {
                mainWindow.contentContainer.Content = mainWindow1;
            }
        }

        private void project2_btn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
            BoardGames.MainWindow2 mainWindow2 = new BoardGames.MainWindow2();
            if (mainWindow != null)
            {
                mainWindow.contentContainer.Content = mainWindow2;
            }
        }
    }
}