using ClienteDuo.DataService;
using ClienteDuo.Utilities;
using System;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace ClienteDuo.Pages
{
    public partial class Login : Page
    {
        private readonly UsersManagerClient _usersManagerClient;

        public Login()
        {
            InitializeComponent();
            _usersManagerClient = new UsersManagerClient();
        }

        private void BtnLoginEvent(object sender, RoutedEventArgs e)
        {
            CreateSession();
        }

        private void EnterKeyEvent(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                CreateSession();
            }
        }

        private void CreateSession()
        {
            string username = TBoxUsername.Text;
            string password = TBoxPassword.Password;
            User loggedUser = AreCredentialsValid(username, password);

            if (loggedUser == null)
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgFailedLogin);
            }
            else if (_usersManagerClient.IsUserAlreadyLoggedIn(loggedUser.ID))
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgUserAlreadyLoggedIn);
            }
            else
            {
                SessionDetails.UserId = loggedUser.ID;
                SessionDetails.Username = loggedUser.UserName;
                SessionDetails.Email = loggedUser.Email;
                SessionDetails.IsGuest = false;

                var user = new User
                {
                    UserName = SessionDetails.Username,
                    ID = SessionDetails.UserId
                };

                var mainMenu = new MainMenu();
                Application.Current.MainWindow.Content = mainMenu;
                MainWindow.NotifyLogin(user);
            }
        }

        public User AreCredentialsValid(string username, string password)
        {
            User userCredentials = null;
            try
            {
                userCredentials = _usersManagerClient.IsLoginValid(username, Sha256Encryptor.SHA256_hash(password));
            }
            catch
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgConnectionError);
            }

            return userCredentials;
        }

        private void BtnCancelEvent(object sender, RoutedEventArgs e)
        {
            var launcher = new Launcher();
            Application.Current.MainWindow.Content = launcher;
        }

        private void ResetPasswordEvent(object sender, MouseEventArgs e)
        {

        }
    }
}
