﻿using ClienteDuo.DataService;
using ClienteDuo.Utilities;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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
            UserDTO userInfo = UsersManager.GetUserInfoByUsername(username);
            _userSelectedName = username;

            SetProfilePicture(userInfo.PictureID);
            LblUsername.Content = Properties.Resources.LblUsername + ": " + username;
            LblTrophies.Content = Properties.Resources.LblTotalWins + ": " + userInfo.TotalWins;

            if (UsersManager.IsAlreadyFriend(SessionDetails.Username, username))
            {
                BtnAddFriend.Visibility = Visibility.Collapsed;
            }
        }

        public void InitializeUserInfo(int friendshipId, string username)
        {
            UserDTO userInfo = UsersManager.GetUserInfoByUsername(username);
            DataContext = friendshipId;
            _userSelectedName = username;

            SetProfilePicture(userInfo.PictureID);
            LblUsername.Content = Properties.Resources.LblUsername + ": " + username;
            LblTrophies.Content = Properties.Resources.LblTotalWins + ": " + userInfo.TotalWins;
            BtnAddFriend.Visibility = Visibility.Collapsed;
        }

        private void SetProfilePicture(int pictureId)
        {
            BitmapImage bitmapImage = new BitmapImage(new System.Uri("pack://application:,,,/ClienteDuo;component/Images/pfp0.png"));
            switch (pictureId)
            {
                case 0:
                    bitmapImage = new BitmapImage(new System.Uri("pack://application:,,,/ClienteDuo;component/Images/pfp0.png"));
                    break;
                case 1:
                    bitmapImage = new BitmapImage(new System.Uri("pack://application:,,,/ClienteDuo;component/Images/pfp1.jpg"));
                    break;
                case 2:
                    bitmapImage = new BitmapImage(new System.Uri("pack://application:,,,/ClienteDuo;component/Images/pfp2.jpg"));
                    break;
                case 3:
                    bitmapImage = new BitmapImage(new System.Uri("pack://application:,,,/ClienteDuo;component/Images/pfp3.jpg"));
                    break;
            }
            ImageProfilePicture.Source = bitmapImage;
            ImageProfilePicture.Stretch = Stretch.UniformToFill;
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
            if (UsersManager.IsFriendRequestAlreadySent(usernameSender, usernameReceiver))
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgFriendRequestAlreadySent, MessageBoxImage.Information);
            }
            else
            {
                if (UsersManager.SendFriendRequest(usernameSender, usernameReceiver))
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
                    isFriend = UsersManager.IsAlreadyFriend(SessionDetails.Username, _userSelectedName);
                }
                catch (CommunicationException)
                {
                    MainWindow.ShowMessageBox(Properties.Resources.DlgServiceException, MessageBoxImage.Error);
                }

                if (isFriend)
                {
                    int friendshipId = (int)DataContext;
                    UsersManager.DeleteFriendshipById(friendshipId);
                }

                bool result = false;
                try
                {
                    result = BlockUser(SessionDetails.Username, _userSelectedName);
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
            if (UsersManager.BlockUserByUsername(usernameSender, usernameReceiver))
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


        private void BtnCancelEvent(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
        }
        
        
    }
}
