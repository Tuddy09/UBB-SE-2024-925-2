using System.Windows.Controls;
using System.Windows.Media;

namespace BoardGames.View.GameOfLife.Pawns
{
    /// <summary>
    /// Interaction logic for PawnTemplate.xaml
    /// </summary>
    public partial class PawnTemplate : UserControl
    {
        public PawnTemplate()
        {
            InitializeComponent();
        }
        public void ChangeBackgroundColor(SolidColorBrush newColor)
        {
            PawnOutline.Fill = newColor;
        }
    }
}
