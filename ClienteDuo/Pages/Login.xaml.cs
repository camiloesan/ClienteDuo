﻿using ClienteDuo.DataService;
using ClienteDuo.Utilities;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace ClienteDuo.Pages
{
    public partial class Login : Page
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        readonly DataService.UsersManagerClient _usersManagerClient;

        public Login()
        {
            InitializeComponent();
            _usersManagerClient = new DataService.UsersManagerClient();
        }

        private void BtnLogin(object sender, RoutedEventArgs e)
        {
            AttemptLogin();
        }

        private void EnterKey(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                AttemptLogin();
            }
        }

        private void AttemptLogin()
        {
            string username = TBoxUsername.Text;
            string password = TBoxPassword.Password;

            User loggedUser = AreCredentialsValid(username, password);

            if (loggedUser.ID != 0)
            {
                SessionDetails.UserID = loggedUser.ID;
                SessionDetails.Username = loggedUser.UserName;
                SessionDetails.Email = loggedUser.Email;
                SessionDetails.IsGuest = false;

                MainMenu mainMenu = new MainMenu();
                App.Current.MainWindow.Content = mainMenu;
            }
            else
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgFailedLogin);
            }
        }

        public User AreCredentialsValid(string username, string password)
        {
            User userCredentials = null;
            try
            {
                userCredentials = _usersManagerClient.IsLoginValid(username, Sha256Encryptor.SHA256_hash(password));
            }
            catch (Exception ex)
            {
                log.Error(ex);
                MainWindow.ShowMessageBox(Properties.Resources.DlgConnectionError);
            }

            return userCredentials;
        }

        private void BtnCancel(object sender, RoutedEventArgs e)
        {
            Launcher launcher = new Launcher();
            App.Current.MainWindow.Content = launcher;
        }
    }
}
