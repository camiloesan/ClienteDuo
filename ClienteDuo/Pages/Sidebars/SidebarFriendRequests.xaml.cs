using ClienteDuo.DataService;
using ClienteDuo.Utilities;
using System.Windows;
using System.Windows.Controls;

namespace ClienteDuo.Pages.Sidebars
{
    public partial class SidebarFriendRequests : UserControl
    {
        readonly DataService.UsersManagerClient _usersManagerClient;

        public SidebarFriendRequests()
        {
            InitializeComponent();
            _usersManagerClient = new DataService.UsersManagerClient();
            FillFriendRequestsPanel();
        }

        private void FillFriendRequestsPanel()
        {
            var list = _usersManagerClient.GetFriendRequestsList(SessionDetails.UserID);
            foreach (var item in list)
            {
                StackPanel stackPanel = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                panelFriendRequests.Children.Add(stackPanel);

                Label lblSender = new Label
                {
                    Content = item.SenderUsername
                };
                stackPanel.Children.Add(lblSender);

                Button btnAccept = new Button
                {
                    Content = Properties.Resources.BtnAccept,
                    DataContext = item,
                };
                btnAccept.Click += AcceptFriendRequestEvent;
                stackPanel.Children.Add(btnAccept);

                Button btnReject = new Button
                {
                    Content = Properties.Resources.BtnReject,
                    DataContext = item,
                };
                btnReject.Click += DeclineFriendRequestEvent;
                stackPanel.Children.Add(btnReject);
            }
        }

        public void AcceptFriendRequestEvent(object sender, RoutedEventArgs e)
        {
            DataService.FriendRequest friendRequest = ((FrameworkElement)sender).DataContext as DataService.FriendRequest;

            if (_usersManagerClient.AcceptFriendRequest(friendRequest))
            {
                MainWindow.ShowMessageBox("ahora son amigos");
            }
            else
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgConnectionError);
            }
        }

        public void DeclineFriendRequestEvent(object sender, RoutedEventArgs e)
        {
            DataService.FriendRequest friendRequest = ((FrameworkElement)sender).DataContext as DataService.FriendRequest;
            if (_usersManagerClient.RejectFriendRequest(friendRequest.FriendRequestID))
            {
                MainWindow.ShowMessageBox("haz eliminado la solicitud");
            }
            else
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgConnectionError);
            }
        }

        private void BtnCancel(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
        }
    }
}
