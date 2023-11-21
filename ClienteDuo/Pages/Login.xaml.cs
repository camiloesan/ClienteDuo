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

        private void BtnLogin(object sender, RoutedEventArgs e)
        {
            CreateSession();
        }

        private void EnterKey(object sender, KeyEventArgs e)
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

            if (loggedUser != null && !_usersManagerClient.IsUserAlreadyLoggedIn(loggedUser.ID))
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
            catch
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgConnectionError);
            }

            return userCredentials;
        }

        private void BtnCancel(object sender, RoutedEventArgs e)
        {
            var launcher = new Launcher();
            Application.Current.MainWindow.Content = launcher;
        }
    }
}
