using ClienteDuo.Utilities;
using System.Windows;
using System.Windows.Controls;

namespace ClienteDuo.Pages.Sidebars
{
    public partial class SidebarAddFriend : UserControl
    {
        public SidebarAddFriend()
        {
            InitializeComponent();
        }

        private void BtnClose(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
        }

        private void BtnSendFriendRequest(object sender, RoutedEventArgs e)
        {
            if (SendFriendRequest(SessionDetails.Username, TBoxUserReceiver.Text))
            {
                MainWindow.ShowMessageBox("friend request sent ***");
            }
            else
            {
                MainWindow.ShowMessageBox("username does not exist or service is unavailable***");
            }
        }

        public bool SendFriendRequest(string usernameSender ,string usernameReceiver)
        {
            usernameSender = usernameSender.Trim();
            usernameReceiver = usernameReceiver.Trim();

            bool result = false;
            if (usernameSender.Length > 0 && usernameReceiver.Length > 0)
            {
                DataService.UsersManagerClient client = new DataService.UsersManagerClient();
                result = client.SendFriendRequest(usernameSender, usernameReceiver);
            }

            return result;
        }
    }
}
