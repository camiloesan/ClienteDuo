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
using System.Windows.Media;

namespace ClienteDuo
{
    public partial class NewAccount : Page
    {
        private readonly UsersManagerClient _usersManagerClient;
        private const int MAX_LENGTH_EMAIL = 30;

        public NewAccount()
        {
            InitializeComponent();
            _usersManagerClient = new UsersManagerClient();
        }

        private void BtnCancelEvent(object sender, RoutedEventArgs e)
        {
            var launcher = new Launcher();
            Application.Current.MainWindow.Content = launcher;
        }

        private void BtnContinueEvent(object sender, RoutedEventArgs e)
        {
            string username = TBoxUsername.Text.Trim();
            string email = TBoxEmail.Text.Trim();
            string password = TBoxPassword.Password.Trim();

            if (!AreFieldsValid()) return;
            
            if (AddUserToDatabase(username, email, password))
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgNewAccountSuccess, MessageBoxImage.Information);
                var launcher = new Launcher();
                Application.Current.MainWindow.Content = launcher;
            }
            else
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgServiceException, MessageBoxImage.Error);
            }
        }

        private bool AreFieldsValid()
        {
            string username = TBoxUsername.Text.Trim();
            string email = TBoxEmail.Text.Trim();
            string password = TBoxPassword.Password.Trim();
            string confirmedPassword = TBoxConfirmPassword.Password.Trim();

            if (AreFieldsEmpty())
            {
                TBoxUsername.BorderBrush = new SolidColorBrush(Colors.Red);
                TBoxEmail.BorderBrush = new SolidColorBrush(Colors.Red);
                MainWindow.ShowMessageBox(Properties.Resources.DlgEmptyFields, MessageBoxImage.Warning);
                return false;
            }
            else if (!IsUsernameValid(username))
            {
                TBoxUsername.BorderBrush = new SolidColorBrush(Colors.Red);
                MainWindow.ShowMessageBox(Properties.Resources.DlgUsernameInvalid, MessageBoxImage.Warning);
                return false;
            }
            else if (!IsPasswordSecure(password))
            {
                TBoxPassword.BorderBrush = new SolidColorBrush(Colors.Red);
                MainWindow.ShowMessageBox(Properties.Resources.DlgInsecurePassword, MessageBoxImage.Warning);
                return false;
            }
            else if (!IsPasswordMatch(password, confirmedPassword))
            {
                TBoxPassword.BorderBrush = new SolidColorBrush(Colors.Red);
                TBoxConfirmPassword.BorderBrush = new SolidColorBrush(Colors.Red);
                MainWindow.ShowMessageBox(Properties.Resources.DlgPasswordDoesNotMatch, MessageBoxImage.Warning);
                return false;
            }
            else if (IsUsernameTaken(username))
            {
                TBoxUsername.BorderBrush = new SolidColorBrush(Colors.Red);
                MainWindow.ShowMessageBox(Properties.Resources.DlgUsernameTaken, MessageBoxImage.Warning);
                return false;
            }
            else if (!IsEmailValid(email))
            {
                TBoxEmail.BorderBrush = new SolidColorBrush(Colors.Red);
                MainWindow.ShowMessageBox(Properties.Resources.DlgEmailInvalid, MessageBoxImage.Warning);
                return false;
            }
            else if (!IsEmailAvailable(email))
            {
                TBoxEmail.BorderBrush = new SolidColorBrush(Colors.Red);
                MainWindow.ShowMessageBox(Properties.Resources.DlgEmailTaken, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        public bool IsUsernameValid(string username)
        {
            var regex = new Regex("^[a-zA-Z0-9]{3,14}$");
            return regex.IsMatch(username) && !username.Contains("guest");
        }

        public bool IsUsernameTaken(string username)
        {
            try
            {
                return _usersManagerClient.IsUsernameTaken(username);
            }
            catch
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgConnectionError, MessageBoxImage.Error);
                return false;
            }
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
                MainWindow.ShowMessageBox(Properties.Resources.DlgConnectionError, MessageBoxImage.Error);
            }

            if (isTaken)
            {
                
            }

            return !isTaken;
        }

        public bool IsEmailValid(string email)
        {
            var regex = new Regex("^[a-zA-Z0-9]{3,16}@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$");
            return regex.IsMatch(email) && email.Length <= MAX_LENGTH_EMAIL;
        }

        public bool IsPasswordMatch(string password, string confirmedPassword)
        {
            return password.Equals(confirmedPassword);
        }

        public bool IsPasswordSecure(string password)
        {
            var regex = new Regex("^(?=.*[A-Z])(?=.*[!@#$&*])(?=.*[0-9].*[0-9])(?=.*[a-z]).{8,16}$");
            return regex.IsMatch(password);
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
                && !string.IsNullOrEmpty(confirmPasswordField))
            {
                return false;
            }
                
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

        public bool DeleteUserFromDatabaseByUsername(string username) //test-only method
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
