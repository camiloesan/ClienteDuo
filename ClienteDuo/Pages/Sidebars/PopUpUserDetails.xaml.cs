using ClienteDuo.DataService;
using ClienteDuo.Utilities;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;

namespace ClienteDuo.Pages.Sidebars
{
    /// <summary>
    /// Interaction logic for PopUpUserDetails.xaml
    /// </summary>
    public partial class PopUpUserDetails : UserControl
    {
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
            
            try
            {
                AddFriend(usernameSender, usernameReceiver);
            }
            catch (CommunicationException)
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgServiceException, MessageBoxImage.Error);
            }
        }

        private void AddFriend(string usernameSender, string usernameReceiver)
        {
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
            }
        }

        private void BtnBlockEvent(object sender, RoutedEventArgs e)
        {
            bool confirmation = MainWindow.ShowConfirmationBox(Properties.Resources.DlgBlockConfirmation);
            if (confirmation)
            {
                bool isFriend = false;
                try
                {
                    isFriend = IsFriend(SessionDetails.Username, _userSelectedName);
                }
                catch (CommunicationException)
                {
                    MainWindow.ShowMessageBox(Properties.Resources.DlgServiceException, MessageBoxImage.Error);
                }

                if (isFriend)
                {
                    int friendshipId = (int)DataContext;
                    DeleteFriendship(friendshipId);
                }

                bool result = false;
                try
                {
                    BlockUser(SessionDetails.Username, _userSelectedName);
                }
                catch (CommunicationException)
                {
                    MainWindow.ShowMessageBox(Properties.Resources.DlgServiceException, MessageBoxImage.Error);
                }

                if (result)
                {
                    MainWindow.ShowMessageBox(Properties.Resources.DlgBlockedUser, MessageBoxImage.Exclamation);
                }
            }
        }

        private bool BlockUser(string usernameSender, string usernameReceiver)
        {
            if (BlockUserByUsername(usernameSender, usernameReceiver))
            {
                if (SessionDetails.PartyCode == 0)
                {
                    var mainMenu = new MainMenu();
                    Application.Current.MainWindow.Content = mainMenu;
                }
                return true;
            }
            return false;
        }

        private bool BlockUserByUsername(string blockerUsername, string blockedUsername)
        {
            UsersManagerClient usersManagerClient = new UsersManagerClient();
            return usersManagerClient.BlockUserByUsername(blockerUsername, blockedUsername);
        }

        private void BtnCancelEvent(object sender, RoutedEventArgs e)
        {   
            Visibility = Visibility.Collapsed;
        }

        private bool DeleteFriendship(int friendshipId)
        {
            UsersManagerClient usersManagerClient = new UsersManagerClient();
            return usersManagerClient.DeleteFriendshipById(friendshipId);
        }

        private bool IsFriend(string usernameSender, string usernameReceiver)
        {
            UsersManagerClient usersManagerClient = new UsersManagerClient();
            return usersManagerClient.IsAlreadyFriend(usernameSender, usernameReceiver);
        }
        
        private bool SendFriendRequest(string usernameSender, string usernameReceiver)
        {
            UsersManagerClient usersManagerClient = new UsersManagerClient();
            return usersManagerClient.SendFriendRequest(usernameSender, usernameReceiver);
        }

        private bool IsFriendRequestAlreadySent(string usernameSender, string usernameReceiver)
        {
            UsersManagerClient usersManagerClient = new UsersManagerClient();
            return usersManagerClient.IsFriendRequestAlreadyExistent(usernameSender, usernameReceiver);
        }
    }
}
