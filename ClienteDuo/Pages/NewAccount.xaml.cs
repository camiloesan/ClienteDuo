using ClienteDuo.Pages;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace ClienteDuo
{
    public partial class NewAccount : Page
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public NewAccount()
        {
            InitializeComponent();
        }

        private void BtnCancel(object sender, RoutedEventArgs e)
        {
            Launcher launcher = new Launcher();
            App.Current.MainWindow.Content = launcher;
        }

        private void BtnContinue(object sender, RoutedEventArgs e)
        {
            if (AreFieldsValid())
            {
                if (AddUserToDatabase())
                {
                    MainWindow.ShowMessageBox(Properties.Resources.DlgNewAccountSuccess);
                }
                else
                {
                    MainWindow.ShowMessageBox(Properties.Resources.DlgServiceException);
                }
            }
        }

        private bool IsUsernameAvailable(String username)
        {
            DataService.UsersManagerClient client = new DataService.UsersManagerClient();

            bool isTaken = false;
            try
            {
                isTaken = client.IsUsernameTaken(username);

            }
            catch (Exception ex)
            {
                log.Error(ex);
                MainWindow.ShowMessageBox(Properties.Resources.DlgConnectionError);
            }

            if (isTaken)
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgUsernameTaken);
            }

            return !isTaken;
        }

        private bool IsEmailAvailable(string email)
        {
            DataService.UsersManagerClient client = new DataService.UsersManagerClient();

            bool isTaken = false;
            try
            {
                isTaken = client.IsEmailTaken(email);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                MainWindow.ShowMessageBox(Properties.Resources.DlgConnectionError);
            }

            if (isTaken)
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgEmailTaken);
            }

            return !isTaken;
        }

        private bool AreFieldsValid()
        {
            string username = TBoxUsername.Text.Trim();
            string email = TBoxEmail.Text.Trim();
            string password = TBoxPassword.Password.Trim();
            if (!AreFieldsEmpty()
                && AreFieldsLengthValid()
                && IsPasswordMatch()
                && IsPasswordSecure(password)
                && IsUsernameAvailable(username)
                && IsEmailAvailable(email))
            {
                return true;
            }

            return false;
        }

        private bool IsPasswordMatch()
        {
            return TBoxPassword.Password.Equals(TBoxConfirmPassword.Password);
        }

        private bool AreFieldsLengthValid()
        {
            string usernameField = TBoxUsername.Text.Trim();
            string emailField = TBoxEmail.Text.Trim();

            if (usernameField.Length > 30)
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgUsernameMaxCharacters);
                return false;
            }
            else if (emailField.Length > 30)
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgEmailMaxCharacters);
                return false;
            }
            return true;
        }

        private bool IsPasswordSecure(string password)
        {
            Regex regex = new Regex("^(?=.*[A-Z])(?=.*[!@#$&*])(?=.*[0-9].*[0-9])(?=.*[a-z]).{8,}$");
            if (regex.IsMatch(password))
            {
                return true;
            }
            else
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgInsecurePassword);
                return false;
            }
        }

        private bool AreFieldsEmpty()
        {
            string usernameField = TBoxUsername.Text;
            string emailField = TBoxEmail.Text;
            string passwordField = TBoxPassword.Password;
            string confirmPasswordField = TBoxConfirmPassword.Password;

            if (string.IsNullOrEmpty(usernameField)
                || string.IsNullOrEmpty(emailField)
                || string.IsNullOrEmpty(passwordField)
                || string.IsNullOrEmpty(confirmPasswordField))
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgEmptyFields);
                return true;
            }
            return false;
        }

        private bool AddUserToDatabase()
        {
            string username = TBoxUsername.Text;
            string email = TBoxEmail.Text;
            string password = TBoxPassword.Password;

            DataService.User databaseUser = new DataService.User
            {
                UserName = username,
                Email = email,
                Password = Sha256_hash(password)
            };

            DataService.UsersManagerClient client = new DataService.UsersManagerClient();

            bool result = false;
            try
            {
                result = client.AddUserToDatabase(databaseUser);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }

            return result;
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
