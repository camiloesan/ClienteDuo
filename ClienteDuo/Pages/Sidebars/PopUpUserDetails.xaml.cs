using ClienteDuo.DataService;
using ClienteDuo.Utilities;
using System.Windows;
using System.Windows.Controls;

namespace ClienteDuo.Pages.Sidebars
{
    /// <summary>
    /// Interaction logic for PopUpUserDetails.xaml
    /// </summary>
    public partial class PopUpUserDetails : UserControl
    {
        private readonly UsersManagerClient _usersManagerClient = new UsersManagerClient();
        private string _userSelectedName;

        public PopUpUserDetails()
        {
            InitializeComponent();
        }

        public void InitializeUserInfo(string username, bool isFriend)
        {
            _userSelectedName = username;
            LblUsername.Content = Properties.Resources.LblUsername + ": " + username;
            LblTrophies.Content = Properties.Resources.LblTrophies + ": ";

            if (isFriend)
            {
                BtnAddFriend.Visibility = Visibility.Collapsed;
            }
        }

        public void InitializeUserInfo(int friendshipId, string username)
        {
            DataContext = friendshipId;
            _userSelectedName = username;
            LblUsername.Content = Properties.Resources.LblUsername + ": " + username;
            LblTrophies.Content = Properties.Resources.LblTrophies + ": ";
            BtnAddFriend.Visibility = Visibility.Collapsed;
        }


        private void BtnAddFriendEvent(object sender, RoutedEventArgs e)
        {
            string usernameSender = SessionDetails.Username;
            string usernameReceiver = DataContext as string;
            if (IsFriendRequestAlreadySent(usernameSender, usernameReceiver))
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgFriendRequestAlreadySent, MessageBoxImage.Information);
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
                    MainWindow.ShowMessageBox(Properties.Resources.DlgConnectionError, MessageBoxImage.Error);
                }
            }
        }

        private void BtnCancelEvent(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
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

        private void BtnBlockEvent(object sender, RoutedEventArgs e)
        {
            bool confirmation = MainWindow.ShowConfirmationBox(Properties.Resources.DlgBlockConfirmation);
            if (!confirmation) return;
            
            if (_usersManagerClient.IsAlreadyFriend(SessionDetails.Username, _userSelectedName))
            {
                var friendshipId = (int)DataContext;
                _usersManagerClient.DeleteFriendshipById(friendshipId);
            }

            if (_usersManagerClient.BlockUserByUsername(SessionDetails.Username, _userSelectedName))
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgBlockedUser, MessageBoxImage.Exclamation);

                if (SessionDetails.PartyCode != 0) return;
                var mainMenu = new MainMenu();
                Application.Current.MainWindow.Content = mainMenu;
            } 
            else
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgConnectionError, MessageBoxImage.Error);
            }
        }
    }
}
