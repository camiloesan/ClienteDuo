using ClienteDuo.DataService;
using ClienteDuo.Pages.Sidebars;
using ClienteDuo.Utilities;
using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace ClienteDuo.Pages
{
    public partial class MainMenu : Page
    {
        SidebarUserProfile _sidebarUserProfile;
        SidebarFriends _sidebarFriends;
        SidebarLeaderboard _sidebarLeaderboard;
        static PopUpUserDetails _popUpUserDetails;
        static PopUpUserLogged _popUpUserLogged;

        public MainMenu()
        {
            InitializeComponent();
            InitializeAddOns();
        }

        public static void ShowPopUpFriendLogged(string username)
        {
            _popUpUserLogged.SetLabelText(username);
            _popUpUserLogged.Visibility = Visibility.Visible;
        }

        public static void ShowPopUpUserDetails(string username)
        {
            _popUpUserDetails.SetDataContext(username, true);
            _popUpUserDetails.Visibility = Visibility.Visible;
        }

        void InitializeAddOns()
        {
            _sidebarUserProfile = new SidebarUserProfile
            {
                Margin = new Thickness(0, 0, 700, 0),
                Visibility = Visibility.Collapsed
            };
            MainGrid.Children.Add(_sidebarUserProfile);

            _sidebarFriends = new SidebarFriends
            {
                Margin = new Thickness(700, 0, 0, 0),
                Visibility = Visibility.Collapsed
            };
            MainGrid.Children.Add(_sidebarFriends);

            _sidebarLeaderboard = new SidebarLeaderboard
            {
                Margin = new Thickness(700, 0, 0, 0),
                Visibility = Visibility.Collapsed
            };
            MainGrid.Children.Add(_sidebarLeaderboard);

            _popUpUserLogged = new PopUpUserLogged
            {
                Width = 200,
                Height = 80,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Center,
                Visibility = Visibility.Collapsed
            };
            MainGrid.Children.Add(_popUpUserLogged);

            _popUpUserDetails = new PopUpUserDetails
            {
                Width = 350,
                Height = 200,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Visibility = Visibility.Collapsed
            };
            MainGrid.Children.Add(_popUpUserDetails);
        }

        private void BtnQuitGame(object sender, RoutedEventArgs e)
        {
            App.Current.MainWindow.Close();
        }

        private void BtnNewParty(object sender, RoutedEventArgs e)
        {
            Lobby lobby = new Lobby(SessionDetails.Username);
            App.Current.MainWindow.Content = lobby;
        }

        private void BtnJoinParty(object sender, RoutedEventArgs e)
        {
            JoinParty joinParty = new JoinParty();
            App.Current.MainWindow.Content = joinParty;
        }

        private void BtnMyProfileSidebar(object sender, RoutedEventArgs e)
        {
            _sidebarUserProfile.Visibility = Visibility.Visible;
        }

        private void BtnFriendsSidebar(object sender, RoutedEventArgs e)
        {
            _sidebarFriends.Visibility = Visibility.Visible;
        }

        private void BtnLeaderboard(object sender, RoutedEventArgs e)
        {
            _sidebarLeaderboard.Visibility = Visibility.Visible;
        }
    }
}
