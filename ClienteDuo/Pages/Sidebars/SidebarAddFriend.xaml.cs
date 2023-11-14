using ClienteDuo.Utilities;
using System.Windows;
using System.Windows.Controls;

namespace ClienteDuo.Pages.Sidebars
{
    public partial class SidebarAddFriend : UserControl
    {
        FriendManager friendManager;

        public SidebarAddFriend()
        {
            InitializeComponent();
            friendManager = new FriendManager();
        }

        private void BtnClose(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
        }

        private void BtnSendFriendRequest(object sender, RoutedEventArgs e)
        {
            if (friendManager.SendFriendRequest(SessionDetails.Username, TBoxUserReceiver.Text))
            {
                MainWindow.ShowMessageBox("friend request sent ***");
            }
            else
            {
                MainWindow.ShowMessageBox("username does not exist or service is unavailable***");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
