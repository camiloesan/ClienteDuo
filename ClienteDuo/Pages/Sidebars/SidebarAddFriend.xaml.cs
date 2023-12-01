using ClienteDuo.Utilities;
using System.Windows;
using System.Windows.Controls;
using ClienteDuo.DataService;
using System.Diagnostics.Eventing.Reader;
using System.Windows.Input;

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
        
        private void EnterKeyEvent(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                SendRequest();
            }
        }

        private void BtnSendFriendRequestEvent(object sender, RoutedEventArgs e)
        {
            SendRequest();
        }

        private void SendRequest()
        {
            string usernameSender = SessionDetails.Username;
            string usernameReceiver = TBoxUserReceiver.Text.Trim();

            if (usernameReceiver == SessionDetails.Username)
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgFriendYourself,
                    MessageBoxImage.Information);
            } 
            else if (IsFriendRequestAlreadySent(usernameSender, usernameReceiver))
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgFriendRequestAlreadySent, 
                    MessageBoxImage.Information);
            }
            else if (!_usersManagerClient.IsUsernameTaken(usernameReceiver))
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgUsernameDoesNotExist, 
                    MessageBoxImage.Information);
            }
            else if (IsAlreadyFriend(usernameSender, usernameReceiver))
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgAlreadyFriends, 
                    MessageBoxImage.Information);
            }
            else if (IsUserBlocked(SessionDetails.Username, usernameReceiver) 
                     || IsUserBlocked(usernameReceiver, SessionDetails.Username))
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgConnectionError,
                    MessageBoxImage.Information);
            }
            else
            {
                bool result = SendFriendRequest(usernameSender, usernameReceiver);
                if (result)
                {
                    MainWindow.ShowMessageBox(Properties.Resources.DlgFriendRequestSent, 
                        MessageBoxImage.Information);
                    Visibility = Visibility.Collapsed;
                }
                else
                {
                    MainWindow.ShowMessageBox(Properties.Resources.DlgConnectionError, 
                        MessageBoxImage.Error);
                }
            }
        }

        private bool IsUserBlocked(string usernameBlocker, string usernameBlocked)
        {
            return _usersManagerClient.IsUserBlockedByUsername(usernameBlocker, usernameBlocked);
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
