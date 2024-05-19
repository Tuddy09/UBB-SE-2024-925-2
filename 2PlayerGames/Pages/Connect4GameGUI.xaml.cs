﻿using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using TwoPlayerGames.Core;
using TwoPlayerGames.exceptions;
using TwoPlayerGames.Service;

namespace TwoPlayerGames.Pages
{
    /// <summary>
    /// Interaction logic for Connect4GameGUI.xaml
    /// </summary>
    public partial class Connect4GameGUI : Page
    {
        private IPlayService playService;
        private InGameService inGameService;
        private int column;
        private Grid connect4Grid;
        private BackgroundWorker worker = new BackgroundWorker();

        public Connect4GameGUI()
        {
            InitializeComponent();
            Loaded += Connect4GameGUI_Loaded;
            InitializeBoard();
            inGameService = Router.InGameService;
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
                        connect4Grid.IsEnabled = false;
                    });
                    playService.PlayOther();
                    SetCurrentTurn();
                    ((OnlineGameService)playService).SetFirstTurn();
                    this.Dispatcher.Invoke(() => { UpdateBoard(); });
                    this.Dispatcher.Invoke(() =>
                    {
                        connect4Grid.IsEnabled = true;
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

        private void InitializeBoard()
        {
            connect4Grid = new Grid();

            for (int i = 0; i < 8; i++)
            {
                ColumnDefinition col = new ColumnDefinition();
                connect4Grid.ColumnDefinitions.Add(col);
                RowDefinition row = new RowDefinition();
                connect4Grid.RowDefinitions.Add(row);
            }
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    Rectangle rect = new Rectangle();
                    rect.Fill = new SolidColorBrush(Colors.White);
                    rect.Stroke = new SolidColorBrush(Colors.Black);
                    if (i == 0)
                    {
                        rect.AddHandler(MouseDownEvent, new MouseButtonEventHandler(MouseDownHandler), true);
                    }
                    Grid.SetRow(rect, i);
                    Grid.SetColumn(rect, j);
                    connect4Grid.Children.Add(rect);
                }
            }

            Grid.SetColumn(connect4Grid, 1);
            Grid.SetRow(connect4Grid, 0);
            parentGrid.Children.Add(connect4Grid);
        }

        private void Connect4GameGUI_Loaded(object sender, RoutedEventArgs e)
        {
            playService = Router.PlayService;
            if (playService.GetTurn() != Router.UserPlayer.Id && Router.OnlineGame)
            {
                worker.RunWorkerAsync();
            }
            // populatePlayersData();
            SetCurrentTurn();
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
            ////PlayerStats playerStats = inGameService.GetPlayerStats(Router.UserPlayer);
            ////Player1Name.Text = playerStats.Player.Name;
            ////Player1Rank.Text = playerStats.Rank;
            ////Player1Trophies.Text = playerStats.Trophies.ToString();

            ////playerStats = inGameService.GetPlayerStats(Router.OpponentPlayer);
            ////Player2Name.Text = playerStats.Player.Name;
            ////Player2Rank.Text = playerStats.Rank;
            ////Player2Trophies.Text = playerStats.Trophies.ToString();
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

            // ChangeCellColor(0, column, Colors.Red);
            // 628-235=393/
        }

        public void MouseLeaveHandler(object sender, MouseEventArgs e)
        {
            // ChangeCellColor(0, column, Colors.White);
        }

        private void MouseDownHandler(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Point position = e.GetPosition(this);

            // Get the column number
            column = (int)(Math.Floor((position.X - 235) / 56));
            object[] arg =[column];
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
            catch (InvalidColumnException)
            {
                MessageBox.Show("Invalid column. Please try again.");
            }
            catch (NotYourTurnException)
            {
                MessageBox.Show("It is not your turn.");
            }
        }

        private void ChangeCellColor(int row, int column, Color color)
        {
            // Find the Rectangle element representing the cell
            foreach (var child in connect4Grid.Children)
            {
                if (child is Rectangle && Grid.GetRow(child as Rectangle) == row && Grid.GetColumn(child as Rectangle) == column)
                {
                    // Update the Fill property of the Rectangle
                    (child as Rectangle).Fill = new SolidColorBrush(color);
                    break; // Exit loop once the cell is found and updated
                }
            }
        }

        public void UpdateBoard()
        {
            foreach (IPiece piece in playService.GetBoard())
            {
                Color p;
                Guid? id = piece.Player.Id;
                if (id == Guid.Empty)
                {
                    p = Colors.Purple;
                }
                else
                {
                    if (id == Router.UserPlayer.Id)
                    {
                        p = Colors.Red;
                    }
                    else
                    {
                        p = Colors.Yellow;
                    }
                }
                ChangeCellColor(piece.YPosition + 1, piece.XPosition, p);
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
    }
}
