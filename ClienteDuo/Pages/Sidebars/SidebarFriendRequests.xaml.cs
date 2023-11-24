using System;
using System.Collections.Generic;
using ClienteDuo.DataService;
using ClienteDuo.Utilities;
using System.Windows;
using System.Windows.Controls;

namespace ClienteDuo.Pages.Sidebars
{
    /// <summary>
    /// Interaction logic for SidebarFriendRequests.xaml
    /// </summary>
    public partial class SidebarFriendRequests : UserControl
    {
        private readonly UsersManagerClient _usersManagerClient = new UsersManagerClient();

        public SidebarFriendRequests()
        {
            InitializeComponent();
            FillFriendRequestsPanel();
        }

        private void FillFriendRequestsPanel()
        {
            panelFriendRequests.Children.Clear();
            var friendRequestsList = GetFriendRequestsByUserId(SessionDetails.UserId);
            foreach (var friendRequest in friendRequestsList)
            {
                panelFriendRequests.Children.Clear();
                var stackPanel = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                panelFriendRequests.Children.Add(stackPanel);

                var lblSender = new Label
                {
                    Content = friendRequest.SenderUsername
                };
                stackPanel.Children.Add(lblSender);

                var btnAccept = new Button
                {
                    Content = Properties.Resources.BtnAccept,
                    DataContext = friendRequest,
                };
                btnAccept.Click += AcceptFriendRequestEvent;
                stackPanel.Children.Add(btnAccept);

                var btnReject = new Button
                {
                    Content = Properties.Resources.BtnReject,
                    DataContext = friendRequest,
                };
                btnReject.Click += DeclineFriendRequestEvent;
                stackPanel.Children.Add(btnReject);
            }
        }

        private void AcceptFriendRequestEvent(object sender, RoutedEventArgs e)
        {
            var friendRequest = ((FrameworkElement)sender).DataContext as FriendRequest;
            if (AcceptFriendRequest(friendRequest))
            {
                MainWindow.ShowMessageBox("ahora son amigos");
                FillFriendRequestsPanel();
            }
            else
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgConnectionError);
            }
        }

        private void DeclineFriendRequestEvent(object sender, RoutedEventArgs e)
        {
            var friendRequest = ((FrameworkElement)sender).DataContext as FriendRequest;
            if (DeclineFriendRequest(friendRequest))
            {
                MainWindow.ShowMessageBox("haz eliminado la solicitud");
                FillFriendRequestsPanel();
            }
            else
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgConnectionError);
            }
        }

        private void BtnCancelEvent(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
        }

        private bool AcceptFriendRequest(FriendRequest friendRequest)
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

        private bool DeclineFriendRequest(FriendRequest friendRequest)
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
        
        private IEnumerable<FriendRequest> GetFriendRequestsByUserId(int userID)
        {
            return _usersManagerClient.GetFriendRequestsList(userID);
        }
    }
}
