using ClienteDuo.Utilities;
using System.Windows;
using System.Windows.Controls;
using ClienteDuo.DataService;

namespace ClienteDuo.Pages.Sidebars
{
    public partial class SidebarAddFriend : UserControl
    {
        private readonly UsersManagerClient _usersManagerClient = new UsersManagerClient();

        public SidebarAddFriend()
        {
            InitializeComponent();
        }

        private void BtnCancelEvent(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
        }

        private void BtnSendFriendRequestEvent(object sender, RoutedEventArgs e)
        {
            string usernameSender = SessionDetails.Username;
            string usernameReceiver = TBoxUserReceiver.Text.Trim();
            if (IsFriendRequestAlreadySent(usernameSender, usernameReceiver))
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgFriendRequestAlreadySent, MessageBoxImage.Information);
            } 
            else if (IsAlreadyFriend(usernameSender, usernameReceiver))
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgAlreadyFriends, MessageBoxImage.Information);
            }
            else
            {
                if (SendFriendRequest(usernameSender, usernameReceiver))
                {
                    MainWindow.ShowMessageBox(Properties.Resources.DlgFriendRequestSent, MessageBoxImage.Information);
                    Visibility = Visibility.Collapsed;
                }
                else
                {
                    MainWindow.ShowMessageBox(Properties.Resources.DlgUsernameDoesNotExist, MessageBoxImage.Warning); //crear metodo exclusivo para validar si existe
                }
            }
        }

        private bool SendFriendRequest(string usernameSender, string usernameReceiver)
        {
            bool result;
            try
            {
                result = _usersManagerClient.SendFriendRequest(usernameSender, usernameReceiver);
            }
            catch
            {
                result = false;
            }
            return result;
        }

        private bool IsAlreadyFriend(string usernameSender, string usernameReceiver)
        {
            bool result;
            try
            {
                result = _usersManagerClient.IsAlreadyFriend(usernameSender, usernameReceiver);
            }
            catch
            {
                result = false;
            }
            return result;
        }

        private bool IsFriendRequestAlreadySent(string usernameSender, string usernameReceiver)
        {
            bool result;
            try
            {
                result = _usersManagerClient.IsFriendRequestAlreadyExistent(usernameSender, usernameReceiver);
            }
            catch
            {
                result = false;
            }
            return result;
        }
    }
}
