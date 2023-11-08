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
        readonly FriendManager friendManager;

        public SidebarFriendRequests()
        {
            InitializeComponent();
            friendManager = new FriendManager();
            FillFriendRequestsPanel();
        }

        private void FillFriendRequestsPanel()
        {
            var friendRequestsList = friendManager.GetFriendRequestsByUserID(SessionDetails.UserID);
            foreach (var friendRequest in friendRequestsList)
            {
                StackPanel stackPanel = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                panelFriendRequests.Children.Add(stackPanel);

                Label lblSender = new Label
                {
                    Content = friendRequest.SenderUsername
                };
                stackPanel.Children.Add(lblSender);

                Button btnAccept = new Button
                {
                    Content = Properties.Resources.BtnAccept,
                    DataContext = friendRequest,
                };
                btnAccept.Click += AcceptFriendRequestEvent;
                stackPanel.Children.Add(btnAccept);

                Button btnReject = new Button
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
            DataService.FriendRequest friendRequest = ((FrameworkElement)sender).DataContext as DataService.FriendRequest;
            if (friendManager.AcceptFriendRequest(friendRequest))
            {
                MainWindow.ShowMessageBox("ahora son amigos");
            }
            else
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgConnectionError);
            }
        }

        private void DeclineFriendRequestEvent(object sender, RoutedEventArgs e)
        {
            DataService.FriendRequest friendRequest = ((FrameworkElement)sender).DataContext as DataService.FriendRequest;
            if (friendManager.DeclineFriendRequest(friendRequest))
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
