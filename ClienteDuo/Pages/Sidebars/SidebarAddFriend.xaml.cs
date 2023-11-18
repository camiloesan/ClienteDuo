using ClienteDuo.Utilities;
using System.Windows;
using System.Windows.Controls;

namespace ClienteDuo.Pages.Sidebars
{
    public partial class SidebarAddFriend : UserControl
    {
        readonly FriendManager friendManager;

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
            if (friendManager.IsFriendRequestAlreadyExistent(SessionDetails.Username, TBoxUserReceiver.Text.Trim()))
            {
                MainWindow.ShowMessageBox("a friend request has already been sent");
            } 
            else if (friendManager.IsAlreadyFriend(SessionDetails.Username, TBoxUserReceiver.Text.Trim()))
            {
                MainWindow.ShowMessageBox("this user is your friend already");
            }
            else
            {
                if (friendManager.SendFriendRequest(SessionDetails.Username, TBoxUserReceiver.Text))
                {
                    MainWindow.ShowMessageBox(Properties.Resources.DlgFriendRequestSent);
                    Visibility = Visibility.Collapsed;
                }
                else
                {
                    MainWindow.ShowMessageBox("username does not exist or service is unavailable***");
                }
            }
        }
    }
}
