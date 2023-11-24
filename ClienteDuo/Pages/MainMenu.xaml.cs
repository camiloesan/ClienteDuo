using ClienteDuo.DataService;
using ClienteDuo.Pages.Sidebars;
using ClienteDuo.Utilities;
using System.Windows;
using System.Windows.Controls;

namespace ClienteDuo.Pages
{
    public partial class MainMenu : Page
    {
        private SidebarUserProfile _sidebarUserProfile;
        private SidebarFriends _sidebarFriends;
        private SidebarLeaderboard _sidebarLeaderboard;
        private static PopUpUserDetails _popUpUserDetails;
        private static PopUpUserLogged _popUpUserLogged;

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

        private void BtnQuitGame(object sender, RoutedEventArgs e)
        {
            var userConnectionHandlerClient = new UserConnectionHandlerClient(MainWindow._instanceContext);
            var user = new User
            {
                ID = SessionDetails.UserId,
            };
            userConnectionHandlerClient.NotifyLogOut(user);
            Application.Current.MainWindow.Close();
        }

        private void BtnNewParty(object sender, RoutedEventArgs e)
        {
            var lobby = new Lobby(SessionDetails.Username);
            Application.Current.MainWindow.Content = lobby;
        }

        private void BtnJoinParty(object sender, RoutedEventArgs e)
        {
            var joinParty = new JoinParty();
            Application.Current.MainWindow.Content = joinParty;
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
