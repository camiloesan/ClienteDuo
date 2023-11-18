using ClienteDuo.DataService;
using ClienteDuo.Utilities;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ClienteDuo.Pages.Sidebars
{
    public partial class SidebarFriends : UserControl
    {
        SidebarAddFriend _sidebarAddFriend;
        SidebarFriendRequests _sidebarFriendRequests;
        readonly FriendManager _friendManager;

        public SidebarFriends()
        {
            InitializeComponent();
            _friendManager = new FriendManager();
            InitializeBars();
            FillFriendsPanel();
        }

        private void InitializeBars()
        {
            _sidebarAddFriend = new SidebarAddFriend
            {
                Visibility = Visibility.Collapsed,
            };
            FriendsBar.Children.Add(_sidebarAddFriend);

            _sidebarFriendRequests = new SidebarFriendRequests
            {
                Visibility = Visibility.Collapsed,
            };
            FriendsBar.Children.Add(_sidebarFriendRequests);
        }

        private void FillFriendsPanel()
        {
            panelFriends.Children.Clear();
            var friendsList = _friendManager
                .GetFriendsListByUserID(SessionDetails.UserID);
            foreach (var friend in friendsList)
            {
                if (friend.Friend1ID != SessionDetails.UserID)
                {
                    CreateFriendPanel(friend.Friend1Username, friend.FriendshipID);
                }
                else if (friend.Friend2ID != SessionDetails.UserID)
                {
                    CreateFriendPanel(friend.Friend2Username, friend.FriendshipID);
                }
            }
        }

        private void CreateFriendPanel(string username, int friendshipID)
        {
            StackPanel stackPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Center,
                Background = new SolidColorBrush(Colors.DimGray),
                Width = 200,
                Height = 40,
            };
            panelFriends.Children.Add(stackPanel);

            Label activeStatus = new Label
            {
                Foreground = new SolidColorBrush(Colors.Black),
                Content = "●",
                Margin = new Thickness(10, 0, 5, 0),
                VerticalAlignment = VerticalAlignment.Center
            };
            stackPanel.Children.Add(activeStatus);

            Label usernameName = new Label
            {
                Foreground = new SolidColorBrush(Colors.Black),
                Content = username,
                Margin = new Thickness(5, 0, 10, 0),
                VerticalAlignment = VerticalAlignment.Center
            };
            stackPanel.Children.Add(usernameName);

            Button btnViewProfile = new Button
            {
                Content = Properties.Resources.BtnProfile,
                DataContext = username,
            };
            btnViewProfile.Click += ViewProfileEvent;
            stackPanel.Children.Add(btnViewProfile);

            Button btnUnfriend = new Button
            {
                Content = Properties.Resources.BtnUnfriend,
                DataContext = friendshipID,
            };
            btnUnfriend.Click += UnfriendEvent;
            stackPanel.Children.Add(btnUnfriend);
        }

        private void ViewProfileEvent(object sender, RoutedEventArgs e)
        {
            string username = ((FrameworkElement)sender).DataContext as string;
            MainMenu.ShowPopUpUserDetails(username);
        }

        private void UnfriendEvent(object sender, RoutedEventArgs e)
        {
            int friendshipID = (int)((FrameworkElement)sender).DataContext;
            
            if (_friendManager.DeleteFriendshipByID(friendshipID))
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgUnfriend);
                FillFriendsPanel();
            }
        }

        private void BtnCancel(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
        }

        private void BtnFriendRequests(object sender, RoutedEventArgs e)
        {
            _sidebarFriendRequests.Visibility = Visibility.Visible;
        }

        private void BtnAddFriend(object sender, RoutedEventArgs e)
        {
            _sidebarAddFriend.Visibility = Visibility.Visible;
        }
    }
}
