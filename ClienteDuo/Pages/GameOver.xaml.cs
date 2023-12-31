﻿using ClienteDuo.DataService;
using ClienteDuo.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClienteDuo.Pages
{
    public partial class GameOver : UserControl
    {
        public GameOver()
        {
            InitializeComponent();
        }

        public void LoadPlayers(Dictionary<string, int> playerScores)
        {
            string winner = playerScores.OrderBy(x => x.Value).First().Key;

            if (SessionDetails.Username.Equals(winner))
            {
                SessionDetails.TotalWins++;
            }

            foreach (KeyValuePair<string, int> playerScore in playerScores)
            {
                PlayerBar playerBar = new PlayerBar();

                if (playerScore.Key.Equals(winner))
                {
                    playerBar.Username = playerScore.Key + "( " + Properties.Resources.LblWinner + ")";
                }
                else
                {
                    playerBar.Username = playerScore.Key + "(" + playerScore.Value.ToString() + " " + Properties.Resources.LblCardsLeft + ")";
                }

                if (playerScore.Key.Equals(SessionDetails.Username))
                {
                    playerBar.Background = new SolidColorBrush(Colors.Gold);
                }

                playerBar.BtnAddFriend.Visibility = Visibility.Collapsed;
                playerBar.Visibility = Visibility.Visible;

                resultStackPanel.Children.Add(playerBar);
            }
        }

        private void BtnExitLobbyEvent(object sender, RoutedEventArgs e)
        {
            if (SessionDetails.IsGuest)
            {
                Launcher launcher = new Launcher();
                Application.Current.MainWindow.Content = launcher;
            }
            else
            {
                MainMenu mainMenu = new MainMenu();
                Application.Current.MainWindow.Content = mainMenu;
            }
        }
    }
}
