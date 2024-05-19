﻿using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using TwoPlayerGames.Core;
using TwoPlayerGames.Domain.Auxiliary;
using TwoPlayerGames.exceptions;
using TwoPlayerGames.Service;
using Brushes = System.Windows.Media.Brushes;

namespace TwoPlayerGames.Pages
{
    /// <summary>
    /// Interaction logic for ObstructionGameGUI.xaml
    /// </summary>
    public partial class ObstructionGameGUI : Page
    {
        private IPlayService playService;
        private InGameService inGameService;
        private BackgroundWorker worker = new BackgroundWorker();
        public Grid ObstructionGrid = new Grid();
        private int column;
        private int row;

        public ObstructionGameGUI()
        {
            playService = Router.PlayService;
            inGameService = Router.InGameService;
            InitializeComponent();
            Loaded += ObstructionGameGUI_Loaded;
            worker.DoWork += Worker_DoWork;
        }

        private void Worker_DoWork(object? sender, DoWorkEventArgs e)
        {
            while (true)
            {
                if (((OnlineGameService)playService).HasData())
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        ObstructionGrid.IsEnabled = false;
                    });
                    playService.PlayOther();
                    SetCurrentTurn();
                    ((OnlineGameService)playService).SetFirstTurn();
                    this.Dispatcher.Invoke(() => { UpdateBoard(); });
                    this.Dispatcher.Invoke(() =>
                    {
                        ObstructionGrid.IsEnabled = true;
                    });
                    if (playService.GetTurn() != Router.UserPlayer.Id)
                    {
                        throw new NotYourTurnException();
                    }
                    if (playService.IsGameOver())
                    {
                        Guid? winner = playService.GetWinner();
                        if (winner == Guid.Empty)
                        {
                            this.Dispatcher.Invoke(() =>
                            {
                                MessageBox.Show("It's a draw!");
                            });
                        }
                        else
                        {
                            if (playService.GetWinner() == Router.UserPlayer.Id)
                            {
                                this.Dispatcher.Invoke(() =>
                                {
                                    MessageBox.Show("You Win!");
                                });
                            }
                            else
                            {
                                this.Dispatcher.Invoke(() =>
                                {
                                    MessageBox.Show("You Lost!");
                                });
                            }
                        }
                        this.Dispatcher.Invoke(() =>
                        {
                            this.NavigationService.Navigate(Router.MenuPage);
                        });
                        return;
                    }
                    break;
                }
            }
            return;
        }

        private void ObstructionGameGUI_Loaded(object sender, RoutedEventArgs e)
        {
            playService = Router.PlayService;
            if (playService.GetTurn() != Router.UserPlayer.Id && Router.OnlineGame)
            {
                worker.RunWorkerAsync();
            }
            // populatePlayersData();
            SetCurrentTurn();
            InitializeBoard();
        }

        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            var confirmationDialog = new NewGameDialog();
            if (confirmationDialog.ShowDialog() == true)
            {
                if (confirmationDialog.DialogResult == true)
                {
                    MessageBox.Show("Starting a new game...");
                    this.NavigationService.Navigate(Router.MenuPage);
                }
                else
                {
                    MessageBox.Show("User declined to start a new game.");
                    confirmationDialog.Close();
                }
            }
        }

        private void PopulatePlayersData()
        {
            PlayerStats playerStats = inGameService.GetStats(Router.UserPlayer);
            Player1Name.Text = playerStats.Player.Name;
            Player1Rank.Text = playerStats.Rank;
            Player1Trophies.Text = playerStats.Trophies.ToString();

            playerStats = inGameService.GetStats(Router.OpponentPlayer);
            Player2Name.Text = playerStats.Player.Name;
            Player2Rank.Text = playerStats.Rank;
            Player2Trophies.Text = playerStats.Trophies.ToString();
        }

        private void SetCurrentTurn()
        {
            if (playService.GetTurn() == Router.UserPlayer.Id)
            {
                this.Dispatcher.Invoke(() => { CurrentPlayerTurn.Text = Router.UserPlayer.Name + "'s turn"; });
            }
            else
            {
                this.Dispatcher.Invoke(() => { CurrentPlayerTurn.Text = Router.OpponentPlayer.Name + "'s turn"; });
            }
        }
        private void MouseMoveHandler(object sender, MouseEventArgs e)
        {
            // Get the x and y coordinates of the mouse pointer.
            System.Windows.Point position = e.GetPosition(this);
            if (Router.ObstructionMode == 5)
            {
                // Get the column number
                column = (int)(Math.Floor((position.X - 235) / 89));
                row = (int)(Math.Floor((position.Y - 100) / 70));
            }
            else if (Router.ObstructionMode == 8)
            {
                column = (int)(Math.Floor((position.X - 235) / 55.25));
                row = (int)(Math.Floor((position.Y - 100) / 44));
            }
            else
            {
                column = (int)(Math.Floor((position.X - 235) / 37));
                row = (int)(Math.Floor((position.Y - 100) / 30));
            }

            // ChangeCellColor(0, column, Colors.Red);
            // 628-235=393/
            // MessageBox.Show($"Mouse X Position: {position.X}\nMouse Y Position: {position.Y}");
        }
        private void InitializeBoard()
        {
            for (int i = 0; i < Router.ObstructionMode; i++)
            {
                ColumnDefinition col = new ColumnDefinition();
                ObstructionGrid.ColumnDefinitions.Add(col);
                RowDefinition row = new RowDefinition();
                ObstructionGrid.RowDefinitions.Add(row);
            }
            for (int i = 0; i < Router.ObstructionMode; i++)
            {
                for (int j = 0; j < Router.ObstructionMode; j++)
                {
                    Border border = new Border();
                    border.BorderBrush = System.Windows.Media.Brushes.Black;
                    border.BorderThickness = new Thickness(1);

                    // Create a TextBlock to represent the content of the cell
                    TextBlock txt = new TextBlock();
                    txt.FontSize = 20;
                    txt.TextAlignment = TextAlignment.Center;
                    txt.Background = Brushes.DarkBlue;

                    txt.Text = string.Empty;
                    txt.AddHandler(MouseDownEvent, new MouseButtonEventHandler(MouseDownHandler), true);
                    txt.AddHandler(MouseMoveEvent, new MouseEventHandler(MouseMoveHandler), true);
                    // Set the content of the border to be the TextBlock
                    border.Child = txt;

                    // Set the column and row indices of the dynamically created Border
                    Grid.SetColumn(border, j);
                    Grid.SetRow(border, i);

                    // Add the border to the dynamic grid
                    ObstructionGrid.Children.Add(border);
                }
            }
            Grid.SetColumn(ObstructionGrid, 1);
            Grid.SetRow(ObstructionGrid, 0);
            parentGrid.Children.Add(ObstructionGrid);
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            if (SettingsPopup.IsOpen == false)
            {
                SettingsPopup.IsOpen = true;
            }
            else
            {
                SettingsPopup.IsOpen = false;
            }
        }
        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            if (InformationPopup.IsOpen == false)
            {
                InformationPopup.IsOpen = true;
            }
            else
            {
                InformationPopup.IsOpen = false;
            }
        }

        private void MouseDownHandler(object sender, MouseButtonEventArgs e)
        {
            object[] arg = [column, row];
            try
            {
                playService.Play(1, arg);
                UpdateBoard();
                if (playService.IsGameOver())
                {
                    Guid? winner = playService.GetWinner();
                    if (winner == null)
                    {
                        MessageBox.Show("It's a draw!");
                    }
                    else
                    {
                        if (playService.GetWinner() == Router.UserPlayer.Id)
                        {
                            MessageBox.Show("You won!");
                        }
                        else
                        {
                            MessageBox.Show("You lost!");
                        }
                    }
                    this.NavigationService.Navigate(Router.MenuPage);
                    return;
                }
                SetCurrentTurn();
                if (Router.OnlineGame)
                {
                    worker.RunWorkerAsync();
                }
            }
            catch (InvalidMoveException)
            {
                MessageBox.Show("Invalid move. Please try again.");
            }
            catch (NotYourTurnException)
            {
                MessageBox.Show("It is not your turn.");
            }
        }
        private void UpdateBoard()
        {
            foreach (IPiece piece in playService.GetBoard())
            {
                string content;

                if (piece.Player.Name == "Null")
                {
                    Color color = Colors.White;
                    ChangeCellColor(piece.YPosition, piece.XPosition, Colors.Red);
                }
                else
                {
                    if (piece.Player.Id == Router.UserPlayer.Id)
                    {
                        content = "X";
                    }
                    else
                    {
                        content = "O";
                    }
                    AddTextToCell(piece.YPosition, piece.XPosition, content);
                }
            }
        }

        private void ChangeCellColor(int row, int column, Color color)
        {
            // Find the Rectangle element representing the cell
            foreach (var child in ObstructionGrid.Children)
            {
                if (child is Border && Grid.GetRow(child as Border) == row && Grid.GetColumn(child as Border) == column)
                {
                    // Update the Fill property of the Rectangle
                    var nephew = (child as Border).Child;
                    (nephew as TextBlock).Background = new SolidColorBrush(color);
                    break; // Exit loop once the cell is found and updated
                }
            }
        }

        private void AddTextToCell(int row, int column, string text)
        {
            foreach (var child in ObstructionGrid.Children)
            {
                if (child is Border && Grid.GetRow(child as Border) == row && Grid.GetColumn(child as Border) == column)
                {
                    // Update the Fill property of the Rectangle
                    var nephew = (child as Border).Child;
                    (nephew as TextBlock).Text = text;
                    break; // Exit loop once the cell is found and updated
                }
            }
        }
    }
}
