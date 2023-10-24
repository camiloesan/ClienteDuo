using System.Security.Cryptography;
using System;
using System.ServiceModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ClienteDuo.Utilities;

namespace ClienteDuo.Pages
{
    public partial class Login : Page
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static string ACTIVE_EMAIL;
        public static string ACTIVE_USERNAME;

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

            bool isLoginValid = false;
            try
            {
                client = new DataService.UsersManagerClient();
                isLoginValid = client.IsLoginValid(username, Sha256_hash(password));
            } 
            catch (Exception ex)
            {
                log.Error(ex);
                MainWindow.ShowMessageBox(Properties.Resources.DlgConnectionError);
            }
            
            if (client != null)
            {
                if (isLoginValid)
                {
                    SessionDetails.username = username;
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

        public static String Sha256_hash(String value)
        {
            StringBuilder Sb = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (Byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }
    }
}
