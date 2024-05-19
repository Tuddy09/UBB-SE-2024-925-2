using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BoardGames.View.GameOfLife
{
    /// <summary>
    /// Interaction logic for PawnStyle.xaml
    /// </summary>
    public partial class PawnStyle : Page
    {
        // !!! After choosing pawn style, change should be sent to DB!
        private readonly Dictionary<string, Color> pawnColors = new ()
        {
            { "Red", Color.FromRgb(228, 6, 19) },
            { "Orange", Color.FromRgb(255, 133, 0) },
            { "Yellow", Color.FromRgb(255, 228, 0) },
            { "Green", Color.FromRgb(0, 148, 64) },
            { "Blue", Color.FromRgb(83, 74, 153) },
            { "Pink", Color.FromRgb(224, 0, 143) },
        };
        private int displayedColorIndex = 0;

        public PawnStyle()
        {
            InitializeComponent();
            Loaded += PawnStyle_Loaded;
        }

        private void PawnStyle_Loaded(object sender, RoutedEventArgs e)
        {
            ChangeDisplayedPawnColor(displayedColorIndex);
        }

        private void ArrowRightButton_Click(object sender, RoutedEventArgs e)
        {
            displayedColorIndex = (displayedColorIndex + 1) % pawnColors.Count;
            ChangeDisplayedPawnColor(displayedColorIndex);
        }
        private void ArrowLeftButton_Click(object sender, RoutedEventArgs e)
        {
            displayedColorIndex = (displayedColorIndex - 1 + pawnColors.Count) % pawnColors.Count;
            ChangeDisplayedPawnColor(displayedColorIndex);
        }
        private void ChangeDisplayedPawnColor(int index)
        {
            Color newColor = pawnColors.ElementAt(index).Value;
            SolidColorBrush currentBrush = new SolidColorBrush(newColor);
            DisplayedPawn.ChangeBackgroundColor(currentBrush);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Settings());
        }
    }
}
