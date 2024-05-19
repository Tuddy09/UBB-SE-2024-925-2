using System.Windows;
using System.Windows.Controls;

namespace BoardGames.View.SkillIssueBro.Board
{
    /// <summary>
    /// Interaction logic for RollButton.xaml
    /// </summary>
    public partial class RollButton : UserControl
    {
        public event EventHandler ButtonClicked;
        public RollButton()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ButtonClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
