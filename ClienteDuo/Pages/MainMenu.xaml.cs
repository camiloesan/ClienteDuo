using ClienteDuo.Pages.Sidebars;
using System;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace ClienteDuo.Pages
{
    public partial class MainMenu : Page
    {
        SidebarUserProfile sidebarUserProfile;
        SidebarFriends sidebarFriends;
        SidebarLeaderboard sidebarLeaderboard;

        public MainMenu()
        {
            InitializeComponent();
            InitializeSidebars();
        }

        void InitializeSidebars()
        {
            sidebarUserProfile = new SidebarUserProfile
            {
                Margin = new Thickness(0, 0, 700, 0),
                Visibility = Visibility.Collapsed
            };
            MainGrid.Children.Add(sidebarUserProfile);

            sidebarFriends = new SidebarFriends
            {
                Margin = new Thickness(700, 0, 0, 0),
                Visibility = Visibility.Collapsed
            };
            MainGrid.Children.Add(sidebarFriends);

            sidebarLeaderboard = new SidebarLeaderboard
            {
                Margin = new Thickness(700, 0, 0, 0),
                Visibility = Visibility.Collapsed
            };
            MainGrid.Children.Add(sidebarLeaderboard);
        }

        private void BtnQuitGame(object sender, RoutedEventArgs e)
        {
            App.Current.MainWindow.Close();
        }

        private void BtnNewParty(object sender, RoutedEventArgs e)
        {
            Lobby lobby = new Lobby();
            App.Current.MainWindow.Content = lobby;
        }

        private void BtnJoinParty(object sender, RoutedEventArgs e)
        {
            JoinParty joinParty = new JoinParty();
            App.Current.MainWindow.Content = joinParty;
        }

        private void BtnMyProfileSidebar(object sender, RoutedEventArgs e)
        {
            sidebarUserProfile.Visibility = Visibility.Visible;
        }

        private void BtnFriendsSidebar(object sender, RoutedEventArgs e)
        {   
            sidebarFriends.Visibility = Visibility.Visible;
        }

        private void BtnLeaderboard(object sender, RoutedEventArgs e)
        {
            sidebarLeaderboard.Visibility = Visibility.Visible;
        }
    }
}
