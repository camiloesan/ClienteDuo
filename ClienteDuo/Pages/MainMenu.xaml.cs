using ClienteDuo.DataService;
using ClienteDuo.Pages.Sidebars;
using ClienteDuo.Utilities;
using System.Windows;
using System.Windows.Controls;

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

        public static void ShowPopUpFriendLogged(string username)
        {
            _popUpUserLogged.SetLabelText(username);
            _popUpUserLogged.Visibility = Visibility.Visible;
        }

        public static void ShowPopUpUserDetails(string username)
        {
            _popUpUserDetails.InitializeUserInfo(username, true);
            _popUpUserDetails.Visibility = Visibility.Visible;
        }

        private void InitializeAddOns()
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

            _sidebarFriends = new SidebarFriends
            {
                Width = 250,
                Height = 565,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Right,
                Visibility = Visibility.Collapsed
            };
            MainGrid.Children.Add(_sidebarFriends);

            _sidebarLeaderboard = new SidebarLeaderboard
            {
                Width = 250,
                Height = 565,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Right,
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

        private void BtnQuitGameEvent(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Close();
        }

        private void BtnNewPartyEvent(object sender, RoutedEventArgs e)
        {
            var lobby = new Lobby(SessionDetails.Username);
            Application.Current.MainWindow.Content = lobby;
        }

        private void BtnJoinPartyEvent(object sender, RoutedEventArgs e)
        {
            var joinParty = new JoinParty();
            Application.Current.MainWindow.Content = joinParty;
        }

        private void BtnMyProfileSidebarEvent(object sender, RoutedEventArgs e)
        {
            _sidebarUserProfile.Visibility = Visibility.Visible;
        }

        private void BtnFriendsSidebarEvent(object sender, RoutedEventArgs e)
        {
            _sidebarFriends.Visibility = Visibility.Visible;
        }

        private void BtnLeaderboardEvent(object sender, RoutedEventArgs e)
        {
            _sidebarLeaderboard.Visibility = Visibility.Visible;
        }
    }
}
