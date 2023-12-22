﻿using System.Threading.Tasks;
using ClienteDuo.Pages.Sidebars;
using ClienteDuo.Utilities;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace ClienteDuo.Pages
{
    public partial class MainMenu : Page
    {
        private static PopUpUserDetails _popUpUserDetails;
        private static PopUpUserLogged _popUpUserLogged;
        private SidebarUserProfile _sidebarUserProfile;
        private SidebarFriends _sidebarFriends;
        private SidebarLeaderboard _sidebarLeaderboard;

        public MainMenu()
        {
            InitializeComponent();
            InitializeAddOns();
        }

        public static async Task ShowPopUpFriendLogged(string username)
        {
            _popUpUserLogged.SetLabelText(username);
            _popUpUserLogged.Visibility = Visibility.Visible;
            await Task.Delay(5000);
            _popUpUserLogged.Visibility = Visibility.Collapsed;
        }

        public static void ShowPopUpUserDetails(int friendshipId, string username)
        {
            _popUpUserDetails.InitializeUserInfo(friendshipId, username);
            _popUpUserDetails.Visibility = Visibility.Visible;
        }

        private void InitializeAddOns()
        {
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
                Height = 240,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Visibility = Visibility.Collapsed
            };
            MainGrid.Children.Add(_popUpUserDetails);
        }

        private void BtnLogOutEvent(object sender, RoutedEventArgs e)
        {
            bool confirmation = MainWindow.ShowConfirmationBox(Properties.Resources.DlgLogOutConfirmation);
            if (confirmation)
            {
                MainWindow.NotifyLogOut(SessionDetails.UserId, false);
                SessionDetails.CleanSessionDetails();
                Application.Current.MainWindow.Content = new Launcher();
            }
        }

        private void BtnNewPartyEvent(object sender, RoutedEventArgs e)
        {
            Lobby lobby = new Lobby(SessionDetails.Username);
            Application.Current.MainWindow.Content = lobby;
        }

        private void BtnJoinPartyEvent(object sender, RoutedEventArgs e)
        {
            JoinParty joinParty = new JoinParty();
            Application.Current.MainWindow.Content = joinParty;
        }

        private void BtnMyProfileSidebarEvent(object sender, RoutedEventArgs e)
        {
            _sidebarUserProfile = new SidebarUserProfile
            {
                Width = 250,
                Height = 565,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left,
                Visibility = Visibility.Collapsed
            };
            MainGrid.Children.Add(_sidebarUserProfile);
            _sidebarUserProfile.Visibility = Visibility.Visible;
        }

        private void BtnFriendsSidebarEvent(object sender, RoutedEventArgs e)
        {
            _sidebarFriends = new SidebarFriends
            {
                Width = 250,
                Height = 565,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Right,
                Visibility = Visibility.Collapsed
            };
            MainGrid.Children.Add(_sidebarFriends);
            _sidebarFriends.Visibility = Visibility.Visible;
        }

        private void BtnLeaderboardEvent(object sender, RoutedEventArgs e)
        {
            _sidebarLeaderboard = new SidebarLeaderboard
            {
                Width = 250,
                Height = 565,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Right,
                Visibility = Visibility.Collapsed
            };
            MainGrid.Children.Add(_sidebarLeaderboard);
            _sidebarLeaderboard.Visibility = Visibility.Visible;
        }
    }
}
