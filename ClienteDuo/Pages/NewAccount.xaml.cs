using ClienteDuo.Pages;
using ClienteDuo.Utilities;
using System;
using System.Net.Mail;
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
            string username = TBoxUsername.Text.Trim();
            string email = TBoxEmail.Text.Trim();
            string password = TBoxPassword.Password.Trim();

            if (AreFieldsValid())
            {
                if (AddUserToDatabase(username, email, password))
                {
                    MainWindow.ShowMessageBox(Properties.Resources.DlgNewAccountSuccess);
                    Launcher launcher = new Launcher();
                    App.Current.MainWindow.Content = launcher;
                }
                else
                {
                    MainWindow.ShowMessageBox(Properties.Resources.DlgServiceException);
                }
            }
        }

        private bool AreFieldsValid()
        {
            string username = TBoxUsername.Text.Trim();
            string email = TBoxEmail.Text.Trim();
            string password = TBoxPassword.Password.Trim();
            string confirmedPassword = TBoxConfirmPassword.Password.Trim();

            if (!AreFieldsEmpty()
                && AreFieldsLengthValid(username, email)
                && IsPasswordMatch(password, confirmedPassword)
                && IsPasswordSecure(password)
                && IsUsernameAvailable(username)
                && IsEmailAvailable(email)
                && !UsernameContainsGuestKeyword(username)
                && IsEmailValid(email))
            {
                return true;
            }

            return false;
        }

        public bool IsUsernameAvailable(String username)
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

        public bool IsEmailAvailable(string email)
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

        public bool IsEmailValid(string email)
        {
            try
            {
                MailAddress mailAddress = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public bool IsPasswordMatch(string password, string confirmedPassword)
        {
            return password.Equals(confirmedPassword);
        }

        public bool AreFieldsLengthValid(string username, string email)
        {
            if (username.Length > 30)
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgUsernameMaxCharacters);
                return false;
            }
            else if (email.Length > 30)
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgEmailMaxCharacters);
                return false;
            }
            return true;
        }

        public bool IsPasswordSecure(string password)
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

        public bool UsernameContainsGuestKeyword(string username)
        {
            return username.Contains("guest");
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

        public bool AddUserToDatabase(string username, string email, string password)
        {
            DataService.User databaseUser = new DataService.User
            {
                UserName = username,
                Email = email,
                Password = Sha256Encryptor.SHA256_hash(password),
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

        public bool DeleteUserFromDatabaseByUsername(string username)
        {
            DataService.UsersManagerClient client = new DataService.UsersManagerClient();
            bool result = false;
            try
            {
                result = client.DeleteUserFromDatabaseByUsername(username);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }

            return result;
        }
    }
}
