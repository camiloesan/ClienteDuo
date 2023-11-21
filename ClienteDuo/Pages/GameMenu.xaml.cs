using ClienteDuo.DataService;
using ClienteDuo.Utilities;
using System.Collections.Generic;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ClienteDuo.Pages
{
    /// <summary>
    /// Interaction logic for GameMenu.xaml
    /// </summary>
    public partial class GameMenu : UserControl
    {
        public GameMenu()
        {
            InitializeComponent();

            PlayerBar yourBar = new PlayerBar();
            yourBar.Username = SessionDetails.Username;
            yourBar.addFriendButton.Visibility = Visibility.Collapsed;
            yourBar.kickButton.Visibility = Visibility.Collapsed;
            yourBar.Background = new SolidColorBrush(Colors.Gold);
            yourBar.Visibility = Visibility.Visible;

            playerStackPanel.Children.Add(yourBar);
        }

        public void LoadPlayers(List<string> playerList)
        {
            foreach (string playerUsername in playerList)
            {
                if (!playerUsername.Equals(SessionDetails.Username))
                {
                    PlayerBar playerBar = new PlayerBar();
                    playerBar.Username = playerUsername;

                    if (SessionDetails.IsHost)
                    {
                        playerBar.kickButton.Visibility = Visibility.Visible;
                    }

                    if (SessionDetails.IsGuest)
                    {
                        playerBar.addFriendButton.Visibility = Visibility.Collapsed;
                    }

                    playerBar.Visibility = Visibility.Visible;

                    playerStackPanel.Children.Add(playerBar);
                }
            }
        }

        private void BtnExit(object sender, RoutedEventArgs e)
        {
            if (SessionDetails.IsGuest)
            {
                Launcher launcher = new Launcher();

                App.Current.MainWindow.Content = launcher;
            }
            else
            {
                MainMenu mainMenu = new MainMenu();

                App.Current.MainWindow.Content = mainMenu;
            }
        }

        private void BtnHideMenu(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
        }
    }
}
