﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Threading;
using TwoPlayerGames.Core;
using TwoPlayerGames.Service;

namespace TwoPlayerGames.Pages
{
    /// <summary>
    /// Interaction logic for LoadingPage.xaml
    /// </summary>
    public partial class LoadingPage : Page
    {
        private DispatcherTimer timer;
        private TimeSpan elapsedTime;
        private TimeSpan desiredTime = TimeSpan.FromSeconds(10); // Change to desired time

        public LoadingPage()
        {
            InitializeComponent();
            Loaded += LoadingPage_Loaded;
        }

        private void LoadingPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (Router.OnlineGame)
            {
                switch (Router.GameType)
                {
                    case "Obstruction":
                        object[] list2 = { Router.ObstructionMode, Router.ObstructionMode };
                        Router.PlayService = new OnlineGameService(Router.GameType, Router.UserPlayer, list2);
                        NavigationService.Navigate(Router.ObstructionPage);
                        break;
                    case "Darts":
                        Router.PlayService = new OnlineGameService(Router.GameType, Router.UserPlayer);
                        NavigationService.Navigate(Router.DartsPage);
                        break;
                    case "Connect4":
                        Router.PlayService = new OnlineGameService(Router.GameType, Router.UserPlayer);
                        NavigationService.Navigate(Router.ConnectPage);
                        break;
                    case "Chess":
                        object[] list = { Router.ChessMode };
                        Router.PlayService = new OnlineGameService(Router.GameType, Router.UserPlayer, list);
                        NavigationService.Navigate(Router.ChessPage);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                List<Object> list = new();
                list.Add(Router.AiDifficulty);
                switch (Router.GameType)
                {
                    case "Chess":
                        list.Add(Router.ChessMode);
                        Router.PlayService = new OfflineGameService(Router.GameType, Router.UserPlayer, list);
                        NavigationService.Navigate(Router.ChessPage);
                        break;
                    case "Obstruction":
                        list.Add(Router.ObstructionMode);
                        list.Add(Router.ObstructionMode);
                        Router.PlayService = new OfflineGameService(Router.GameType, Router.UserPlayer, list);
                        NavigationService.Navigate(Router.ObstructionPage);
                        break;
                    case "Darts":
                        Router.PlayService = new OfflineGameService(Router.GameType, Router.UserPlayer, list);
                        NavigationService.Navigate(Router.DartsPage);
                        break;
                    case "Connect4":
                        Router.PlayService = new OfflineGameService(Router.GameType, Router.UserPlayer, list);
                        NavigationService.Navigate(Router.ConnectPage);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
