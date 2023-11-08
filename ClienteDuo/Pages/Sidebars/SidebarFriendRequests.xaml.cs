using ClienteDuo.DataService;
using ClienteDuo.Utilities;
using System.Windows;
using System.Windows.Controls;

namespace ClienteDuo.Pages.Sidebars
{
    public partial class SidebarFriendRequests : UserControl
    {
        public SidebarFriendRequests()
        {
            InitializeComponent();
            FillFriendRequestsPanel();
        }

        private void FillFriendRequestsPanel()
        {
            DataService.UsersManagerClient client = new DataService.UsersManagerClient();
            var list = client.GetFriendRequestsList(SessionDetails.UserID);


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
                };
                btnAccept.DataContext = item;
                btnAccept.Click += (object sender, RoutedEventArgs e) =>
                {
                    DataService.FriendRequest friendRequest = new DataService.FriendRequest
                    {
                        FriendRequestID = item.FriendRequestID,
                        SenderID = item.SenderID,
                        ReceiverID = item.ReceiverID
                    };

                    if (client.AcceptFriendRequest(friendRequest))
                    {
                        MainWindow.ShowMessageBox("ahora son amigos");
                    }
                    else
                    {
                        MainWindow.ShowMessageBox(Properties.Resources.DlgConnectionError);
                    }
                };
                stackPanel.Children.Add(btnAccept);

                Button btnReject = new Button
                {
                    Content = Properties.Resources.BtnReject
                };
                stackPanel.Children.Add(btnReject);
                btnReject.Click += (object sender, RoutedEventArgs e) =>
                {
                    if (client.RejectFriendRequest(item.FriendRequestID))
                    {
                        MainWindow.ShowMessageBox("haz eliminado la solicitud");
                    }
                    else
                    {
                        MainWindow.ShowMessageBox(Properties.Resources.DlgConnectionError);
                    }
                };
            }
        }

        public void AcceptFriendRequest(object sender, RoutedEventArgs e)
        {
            DataService.UsersManagerClient client = new DataService.UsersManagerClient();

            DataService.FriendRequest friendRequest = ((FrameworkElement)sender).DataContext as DataService.FriendRequest;

            if (client.AcceptFriendRequest(friendRequest))
            {
                MainWindow.ShowMessageBox("ahora son amigos");
            }
            else
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgConnectionError);
            }
        }

        public void DeclineFriendRequest()
        {

        }

        private void BtnCancel(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
        }
    }
}
