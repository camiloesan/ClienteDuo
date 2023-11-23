using ClienteDuo.Pages;
using ClienteDuo.Utilities;
using System;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using ClienteDuo.DataService;

namespace ClienteDuo
{
    public partial class NewAccount : Page
    {
        private readonly UsersManagerClient _usersManagerClient;

        public NewAccount()
        {
            InitializeComponent();
            _usersManagerClient = new UsersManagerClient();
        }

        private void BtnCancel(object sender, RoutedEventArgs e)
        {
            var launcher = new Launcher();
            Application.Current.MainWindow.Content = launcher;
        }

        private void BtnContinue(object sender, RoutedEventArgs e)
        {
            string username = TBoxUsername.Text.Trim();
            string email = TBoxEmail.Text.Trim();
            string password = TBoxPassword.Password.Trim();

            if (!AreFieldsValid()) return;
            
            if (AddUserToDatabase(username, email, password))
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgNewAccountSuccess);
                var launcher = new Launcher();
                Application.Current.MainWindow.Content = launcher;
            }
            else
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgServiceException);
            }
        }

        private bool AreFieldsValid()
        {
            string username = TBoxUsername.Text.Trim();
            string email = TBoxEmail.Text.Trim();
            string password = TBoxPassword.Password.Trim();
            string confirmedPassword = TBoxConfirmPassword.Password.Trim();

            return !AreFieldsEmpty()
                   && AreFieldsLengthValid(username, email)
                   && IsPasswordMatch(password, confirmedPassword)
                   && IsPasswordSecure(password)
                   && IsUsernameAvailable(username)
                   && IsEmailAvailable(email)
                   && !UsernameContainsGuestKeyword(username)
                   && IsEmailValid(email);
        }

        public bool IsUsernameAvailable(String username)
        {
            bool isTaken = false;
            try
            {
                isTaken = _usersManagerClient.IsUsernameTaken(username);

            }
            catch
            {
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
            bool isTaken = false;
            try
            {
                isTaken = _usersManagerClient.IsEmailTaken(email);
            }
            catch
            {
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
                _ = new MailAddress(email);
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

            if (email.Length > 30)
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgEmailMaxCharacters);
                return false;
            }
            return true;
        }

        public bool IsPasswordSecure(string password)
        {
            var regex = new Regex("^(?=.*[A-Z])(?=.*[!@#$&*])(?=.*[0-9].*[0-9])(?=.*[a-z]).{8,}$");
            if (regex.IsMatch(password))
            {
                return true;
            }

            MainWindow.ShowMessageBox(Properties.Resources.DlgInsecurePassword);
            return false;
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

            if (!string.IsNullOrEmpty(usernameField)
                && !string.IsNullOrEmpty(emailField)
                && !string.IsNullOrEmpty(passwordField)
                && !string.IsNullOrEmpty(confirmPasswordField)) return false;
            MainWindow.ShowMessageBox(Properties.Resources.DlgEmptyFields);
            return true;
        }

        public bool AddUserToDatabase(string username, string email, string password)
        {
            User databaseUser = new User
            {
                UserName = username,
                Email = email,
                Password = Sha256Encryptor.SHA256_hash(password),
            };

            bool result;
            try
            {
                result = _usersManagerClient.AddUserToDatabase(databaseUser);
            }
            catch
            {
                result = false;
            }

            return result;
        }

        public bool DeleteUserFromDatabaseByUsername(string username)
        {
            bool result;
            try
            {
                result = _usersManagerClient.DeleteUserFromDatabaseByUsername(username);
            }
            catch
            {
                result = false;
            }

            return result;
        }
    }
}
