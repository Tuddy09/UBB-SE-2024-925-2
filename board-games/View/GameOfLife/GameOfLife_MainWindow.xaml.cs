using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BoardGames.Controller;
using BoardGames.Model.GameOfLife;
using BoardGames.View;

namespace BoardGames
{
    /// <summary>
    /// Interaction logic for GameOfLife_MainWindow.xaml
    /// </summary>
    public partial class GameOfLife_MainWindow
    {
        private const int NumberOfTiles = 80;
        private const int NumberOfEventTilesByCategory = 10;
        private const int TotalNumberOfEventTiles = 3 * NumberOfEventTilesByCategory;
        private const int NumberOfGreenTiles = 8;
        private const string TileNameCommonRoot = "Tile";
        private int spinnerValue;
        private GameOfLifeBoard gameOfLifeBoard = new GameOfLifeBoard();
        private BitmapImage spinnerDisplay;
        // new BitmapImage(new Uri("../../Resources/SpinnerValues/Spinner1.png", UriKind.Relative));
        private const string CommonFileRoot = "../../Resources/SpinnerValues/Spinner";

        private readonly GameOfLifeController controller = new GameOfLifeController();

        public GameOfLife_MainWindow()
        {
            InitializeComponent();
            Loaded += GameOfLife_MainWindow_Loaded;
        }

        /// <summary>
        /// Generates a list of unique random integers from 1 to 80 (inclusive)
        /// </summary>
        /// <param name="numberOfIndices">The number of random integers to generate.</param>
        /// <returns>A list of unique random integers.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="numberOfIndices"/> is greater than the total number of tiles or smaller than 0.
        /// </exception>
        private List<int> GenerateRandomIndices(int numberOfIndices)
        {
            try
            {
                var indices = controller.GenerateRandomIndices(numberOfIndices, NumberOfTiles);
                return indices;
            }
            catch (ArgumentOutOfRangeException e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
        }

        private void GameOfLife_MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            int nextIndex = 0;
            List<int> indicesToColor = GenerateRandomIndices(TotalNumberOfEventTiles + NumberOfGreenTiles);
            List<int> indicesOfGreenTiles = indicesToColor.GetRange(nextIndex, NumberOfGreenTiles);
            ColorTiles(indicesOfGreenTiles, Color.FromRgb(150, 187, 32));
            nextIndex = NumberOfGreenTiles;
            List<int> indicesOfRedTiles = indicesToColor.GetRange(nextIndex, NumberOfEventTilesByCategory);
            ColorTiles(indicesOfRedTiles, Color.FromRgb(229, 51, 61));
            nextIndex = nextIndex + NumberOfEventTilesByCategory;
            List<int> indicesOfBlueTiles = indicesToColor.GetRange(nextIndex, NumberOfEventTilesByCategory);
            ColorTiles(indicesOfBlueTiles, Color.FromRgb(43, 163, 171));
            nextIndex = nextIndex + NumberOfEventTilesByCategory;
            List<int> indicesOfOrangeTiles = indicesToColor.GetRange(nextIndex, NumberOfEventTilesByCategory);
            ColorTiles(indicesOfOrangeTiles, Color.FromRgb(241, 151, 54));
        }

        /// <summary>
        /// Colors the tiles with specified indices using the given brush color.
        /// </summary>
        /// <param name="tileIndexesToColor">The indices of the tiles to be colored.</param>
        /// <param name="givenBrushColor">The color to fill the tiles with.</param>
        private void ColorTiles(List<int> tileIndexesToColor, Color givenBrushColor)
        {
            SolidColorBrush currentBrush = new SolidColorBrush(givenBrushColor);
            string currentTileName;
            foreach (int tileIndex in tileIndexesToColor)
            {
                currentTileName = TileNameCommonRoot + tileIndex.ToString();
                Path currentTile = (Path)FindName(currentTileName);
                currentTile.Fill = currentBrush;
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new HostJoinView());
        }

        private void SpinnerActionArea_MouseUp(object sender, MouseButtonEventArgs e)
        {
            spinnerValue = gameOfLifeBoard.SpinSpinner();
            string currentSpinnerDisplay = CommonFileRoot + spinnerValue.ToString();
            spinnerDisplay = new BitmapImage(new Uri(currentSpinnerDisplay, UriKind.Relative));
            ImageControl.Source = spinnerDisplay;
        }

        private void ActionArea_Click(object sender, RoutedEventArgs e)
        {
            spinnerValue = gameOfLifeBoard.SpinSpinner();
            string currentSpinnerDisplay = CommonFileRoot + spinnerValue.ToString();
            currentSpinnerDisplay += ".png";
            spinnerDisplay = new BitmapImage(new Uri(currentSpinnerDisplay, UriKind.Relative));
            // spinnerDisplay = new BitmapImage(new Uri("D:/board-games/board-games/board-games/Resources/SpinnerValues/Spinner10.png", UriKind.Absolute));
            ImageControl.Source = spinnerDisplay;
        }
    }
}
