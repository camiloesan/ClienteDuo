using ClienteDuo.Utilities;
using System;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ClienteDuo.Pages.Sidebars
{
    public partial class SidebarFriends : UserControl
    {
        SidebarAddFriend sidebarAddFriend;
        SidebarFriendRequests sidebarFriendRequests;

        public SidebarFriends()
        {
            InitializeComponent();
            sidebarAddFriend = new SidebarAddFriend
            {
                Visibility = Visibility.Collapsed,
            };
            FriendsBar.Children.Add(sidebarAddFriend);

            sidebarFriendRequests = new SidebarFriendRequests
            {
                Visibility = Visibility.Collapsed,
            };
            FriendsBar.Children.Add(sidebarFriendRequests);

            FillFriendsPanel();
        }

        private void FillFriendsPanel()
        {
            DataService.UsersManagerClient client = new DataService.UsersManagerClient();
            var friendsList = client.GetFriendsList(SessionDetails.userID);

            foreach (var friend in friendsList)
            {
                StackPanel stackPanel = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                panelFriends.Children.Add(stackPanel);

                Label lblFriend = new Label();
                lblFriend.Foreground = new SolidColorBrush(Colors.White);
                if (friend.Friend1ID != SessionDetails.userID)
                {
                    lblFriend.Content = friend.Friend1Username;
                    stackPanel.Children.Add(lblFriend);
                }
                else if (friend.Friend2ID != SessionDetails.userID)
                {
                    lblFriend.Content = friend.Friend2Username;
                    stackPanel.Children.Add(lblFriend);
                }
            }
        }

        private void BtnCancel(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
        }

        private void BtnFriendRequests(object sender, RoutedEventArgs e)
        {
            sidebarFriendRequests.Visibility = Visibility.Visible;
        }

        private void BtnAddFriend(object sender, RoutedEventArgs e)
        {
            sidebarAddFriend.Visibility = Visibility.Visible;
        }
    }
}
