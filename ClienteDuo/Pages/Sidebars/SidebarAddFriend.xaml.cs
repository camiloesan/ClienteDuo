using ClienteDuo.DataService;
using ClienteDuo.Utilities;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ClienteDuo.Pages.Sidebars
{
    public partial class SidebarAddFriend : UserControl
    {

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
                string usernameSender = SessionDetails.Username;
                string usernameReceiver = TBoxUserReceiver.Text.Trim();
                try
                {
                    SendRequest(usernameSender, usernameReceiver);
                }
                catch (CommunicationException)
                {
                    MainWindow.ShowMessageBox(Properties.Resources.DlgServiceException, MessageBoxImage.Error);
                }
            }
        }

        private void BtnSendFriendRequestEvent(object sender, RoutedEventArgs e)
        {
            string usernameSender = SessionDetails.Username;
            string usernameReceiver = TBoxUserReceiver.Text.Trim();
            try
            {
                SendRequest(usernameSender, usernameReceiver);
            }
            catch (CommunicationException)
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgServiceException, MessageBoxImage.Error);
            }
        }

        private bool SendRequest(string usernameSender, string usernameReceiver)
        {
            bool result = false;
            if (usernameReceiver == SessionDetails.Username)
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgFriendYourself,
                    MessageBoxImage.Information);
            }
            else if (!UsersManager.IsUsernameTaken(usernameReceiver))
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgUsernameDoesNotExist,
                    MessageBoxImage.Information);
            }
            else if (UsersManager.IsAlreadyFriend(usernameSender, usernameReceiver))
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgAlreadyFriends,
                    MessageBoxImage.Information);
            }
            else if (UsersManager.IsFriendRequestAlreadySent(usernameSender, usernameReceiver))
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgFriendRequestAlreadySent,
                    MessageBoxImage.Information);
            }
            else if (UsersManager.IsUserBlocked(SessionDetails.Username, usernameReceiver)
                     || UsersManager.IsUserBlocked(usernameReceiver, SessionDetails.Username))
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgConnectionError,
                    MessageBoxImage.Information);
            }
            else
            {
                result = UsersManager.SendFriendRequest(usernameSender, usernameReceiver);
                if (result)
                {
                    MainWindow.ShowMessageBox(Properties.Resources.DlgFriendRequestSent,
                        MessageBoxImage.Information);
                    Visibility = Visibility.Collapsed;
                }
                TBoxUserReceiver.Clear();
            }
            return result;
        }
    }
}
