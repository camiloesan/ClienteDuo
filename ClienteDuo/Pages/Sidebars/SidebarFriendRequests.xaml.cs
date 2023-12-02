using System;
using System.Collections.Generic;
using ClienteDuo.DataService;
using ClienteDuo.Utilities;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ClienteDuo.Pages.Sidebars
{
    /// <summary>
    /// Interaction logic for SidebarFriendRequests.xaml
    /// </summary>
    public partial class SidebarFriendRequests : UserControl
    {
        private readonly UsersManagerClient _usersManagerClient;

        public SidebarFriendRequests()
        {
            InitializeComponent();
            _usersManagerClient = new UsersManagerClient();
            FillFriendRequestsPanel();
        }

        private void FillFriendRequestsPanel()
        {
            PanelFriendRequests.Children.Clear();
            IEnumerable<FriendRequestDTO> friendRequestsList = GetFriendRequestsByUserId(SessionDetails.UserId);
            foreach (FriendRequestDTO friendRequest in friendRequestsList)
            {
                PanelFriendRequests.Children.Clear();
                StackPanel stackPanel = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                PanelFriendRequests.Children.Add(stackPanel);

                Label lblSender = new Label
                {
                    Content = friendRequest.SenderUsername
                };
                stackPanel.Children.Add(lblSender);

                Button btnAccept = new Button
                {
                    Content = Properties.Resources.BtnAccept,
                    DataContext = friendRequest
                };
                btnAccept.Click += AcceptFriendRequestEvent;
                stackPanel.Children.Add(btnAccept);

                Button btnReject = new Button
                {
                    Content = Properties.Resources.BtnReject,
                    DataContext = friendRequest
                };
                btnReject.Click += DeclineFriendRequestEvent;
                stackPanel.Children.Add(btnReject);
            }
        }

        private void AcceptFriendRequestEvent(object sender, RoutedEventArgs e)
        {
            var friendRequest = ((FrameworkElement)sender).DataContext as FriendRequestDTO;
            if (AcceptFriendRequest(friendRequest))
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgNowFriends, MessageBoxImage.Information);
                FillFriendRequestsPanel();
                MainMenu mainMenu = new MainMenu();
                Application.Current.MainWindow.Content = mainMenu;
            }
            else
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgConnectionError, MessageBoxImage.Error);
            }
        }

        private void DeclineFriendRequestEvent(object sender, RoutedEventArgs e)
        {
            FriendRequestDTO friendRequest = ((FrameworkElement)sender).DataContext as FriendRequestDTO;
            if (DeclineFriendRequest(friendRequest))
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgFriendRequestDeleted, MessageBoxImage.Information);
                FillFriendRequestsPanel();
            }
            else
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgConnectionError, MessageBoxImage.Error);
            }
        }

        private void BtnCancelEvent(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
        }

        private bool AcceptFriendRequest(FriendRequestDTO friendRequest)
        {
            bool result;
            try
            {
                result = _usersManagerClient.AcceptFriendRequest(friendRequest);
            }
            catch
            {
                result = false;
            }
            
            return result;
        }

        private bool DeclineFriendRequest(FriendRequestDTO friendRequest)
        {
            bool result;
            try
            {
                result = _usersManagerClient.RejectFriendRequest(friendRequest.FriendRequestID);
            }
            catch
            {
                result = false;
            }

            return result;
        }
        
        private IEnumerable<FriendRequestDTO> GetFriendRequestsByUserId(int userId)
        {
            return _usersManagerClient.GetFriendRequestsList(userId);
        }
    }
}
