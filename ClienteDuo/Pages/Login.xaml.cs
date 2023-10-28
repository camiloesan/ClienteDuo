using ClienteDuo.DataService;
using ClienteDuo.Utilities;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ClienteDuo.Pages
{
    public partial class Login : Page
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Login()
        {
            InitializeComponent();
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
            DataService.UsersManagerClient client = null;

            User loggedUser = new User();
            try
            {
                client = new DataService.UsersManagerClient();
                loggedUser = client.IsLoginValid(username, Sha256Encryptor.SHA256_hash(password));
            }
            catch (Exception ex)
            {
                log.Error(ex);
                MainWindow.ShowMessageBox(Properties.Resources.DlgConnectionError);
            }

            if (client != null)
            {
                if (loggedUser != null)
                {
                    SessionDetails.Username = loggedUser.UserName;
                    SessionDetails.Email = loggedUser.Email;
                    SessionDetails.IsGuest = false;
                    SessionDetails.UserID = loggedUser.ID;

                    MainMenu mainMenu = new MainMenu();
                    App.Current.MainWindow.Content = mainMenu;
                }
                else
                {
                    MainWindow.ShowMessageBox(Properties.Resources.DlgFailedLogin);
                }
            }
        }

        private void BtnCancel(object sender, RoutedEventArgs e)
        {
            Launcher launcher = new Launcher();
            App.Current.MainWindow.Content = launcher;
        }
    }
}
