using ClienteDuo.DataService;
using ClienteDuo.Utilities;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ClienteDuo.Pages.Sidebars
{
    public partial class SidebarFriends : UserControl
    {
        private SidebarAddFriend _sidebarAddFriend;
        private SidebarFriendRequests _sidebarFriendRequests;
        private SidebarBlockedUsers _sidebarBlockedUsers;

        public SidebarFriends()
        {
            InitializeComponent();
            InitializeBars();
            FillFriendsPanel();
        }

        private void InitializeBars()
        {
            _sidebarAddFriend = new SidebarAddFriend
            {
                Visibility = Visibility.Collapsed
            };
            FriendsBar.Children.Add(_sidebarAddFriend);

            _sidebarFriendRequests = new SidebarFriendRequests
            {
                Visibility = Visibility.Collapsed
            };
            FriendsBar.Children.Add(_sidebarFriendRequests);

            _sidebarBlockedUsers = new SidebarBlockedUsers
            {
                Visibility = Visibility.Collapsed
            };
            FriendsBar.Children.Add(_sidebarBlockedUsers);
        }

        private void FillFriendsPanel()
        {
            PanelFriends.Children.Clear();
            IEnumerable<FriendshipDTO> friendsList = new List<FriendshipDTO>();
            try
            {
                GetFriendsListByUserId(SessionDetails.UserId);
            }
            catch (CommunicationException)
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgServiceException, MessageBoxImage.Error);
            }
            
            foreach (FriendshipDTO friend in friendsList)
            {
                if (friend.Friend1ID != SessionDetails.UserId)
                {
                    CreateFriendPanel(friend.Friend1Username, friend.FriendshipID);
                }
                else if (friend.Friend2ID != SessionDetails.UserId)
                {
                    CreateFriendPanel(friend.Friend2Username, friend.FriendshipID);
                }
            }
        }

        private void CreateFriendPanel(string username, int friendshipId)
        {
            StackPanel stackPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(0,7,0,0),
                HorizontalAlignment = HorizontalAlignment.Center,
                Background = new SolidColorBrush(Colors.DimGray),
                Width = 200,
                Height = 40
            };
            PanelFriends.Children.Add(stackPanel);

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

            Tuple<int, string> friendshipUsernameTuple = Tuple.Create(friendshipId, username);
            var btnViewProfile = new Button
            {
                Content = Properties.Resources.BtnProfile,
                DataContext = friendshipUsernameTuple
            };
            btnViewProfile.Click += ViewProfileEvent;
            stackPanel.Children.Add(btnViewProfile);

            Button btnUnfriend = new Button
            {
                Content = Properties.Resources.BtnUnfriend,
                DataContext = friendshipId
            };
            btnUnfriend.Click += UnfriendEvent;
            stackPanel.Children.Add(btnUnfriend);
        }

        private void ViewProfileEvent(object sender, RoutedEventArgs e)
        {
            Tuple<int, string> friendshipUsernameTuple
                = ((FrameworkElement)sender).DataContext as Tuple<int, string>;
            MainMenu.ShowPopUpUserDetails(friendshipUsernameTuple.Item1, friendshipUsernameTuple.Item2);
        }

        private void UnfriendEvent(object sender, RoutedEventArgs e)
        {
            int friendshipId = (int)((FrameworkElement)sender).DataContext;
            bool confirmation = MainWindow.ShowConfirmationBox(Properties.Resources.DlgUnfriendConfirmation);

            if (confirmation)
            {
                bool result = false;
                try
                {
                    result = DeleteFriendshipById(friendshipId);
                }
                catch (CommunicationException)
                {
                    MainWindow.ShowMessageBox(Properties.Resources.DlgServiceException, MessageBoxImage.Error);
                }
               
                if (result)
                {
                    MainWindow.ShowMessageBox(Properties.Resources.DlgUnfriend, MessageBoxImage.Information);
                }
                FillFriendsPanel();
            }
        }

        private void BtnCancelEvent(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
        }

        private void BtnFriendRequestsEvent(object sender, RoutedEventArgs e)
        {
            _sidebarFriendRequests.Visibility = Visibility.Visible;
        }

        private void BtnAddFriendEvent(object sender, RoutedEventArgs e)
        {
            _sidebarAddFriend.Visibility = Visibility.Visible;
        }
        
        private IEnumerable<FriendshipDTO> GetFriendsListByUserId(int userId)
        {
            UsersManagerClient usersManagerClient = new UsersManagerClient();
            return usersManagerClient.GetFriendsList(userId);
        }

        private bool DeleteFriendshipById(int friendshipId)
        {
            UsersManagerClient usersManagerClient = new UsersManagerClient();
            return usersManagerClient.DeleteFriendshipById(friendshipId);
        }

        private void BtnBlockedUsersEvent(object sender, RoutedEventArgs e)
        {
            _sidebarBlockedUsers.Visibility = Visibility.Visible;
        }
    }
}
