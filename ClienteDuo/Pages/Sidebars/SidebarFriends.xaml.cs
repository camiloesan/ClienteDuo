﻿using ClienteDuo.DataService;
using ClienteDuo.Utilities;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ClienteDuo.Pages.Sidebars
{
    public partial class SidebarFriends : UserControl
    {
        SidebarAddFriend sidebarAddFriend;
        SidebarFriendRequests sidebarFriendRequests;
        readonly FriendManager friendManager;

        public SidebarFriends()
        {
            InitializeComponent();
            friendManager = new FriendManager();
            InitializeBars();
            FillFriendsPanel();
        }

        private void InitializeBars()
        {
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
        }

        private void FillFriendsPanel()
        {
            var friendsList = friendManager.GetFriendsListByUserID(SessionDetails.UserID);

            foreach (var friend in friendsList)
            {
                if (friend.Friend1ID != SessionDetails.UserID)
                {
                    CreateFriendPanel(friend.Friend1Username);
                }
                else if (friend.Friend2ID != SessionDetails.UserID)
                {
                    CreateFriendPanel(friend.Friend2Username);
                }
            }
        }

        private void CreateFriendPanel(string username)
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
