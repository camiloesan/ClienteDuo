using ClienteDuo.DataService;
using ClienteDuo.Utilities;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace ClienteDuo.Pages.Sidebars
{
    /// <summary>
    /// Interaction logic for PasswordReset.xaml
    /// </summary>
    public partial class PasswordReset : Page
    {
        private readonly UsersManagerClient _usersManagerClient = new UsersManagerClient();

        public PasswordReset()
        {
            InitializeComponent();
            ToggleBtnShowHide.Checked += ShowPassword;
            ToggleBtnShowHide.Unchecked += HidePassword;
        }

        private void ShowPassword(object sender, RoutedEventArgs e)
        {
            //todo
        }

        private void HidePassword(object sender, RoutedEventArgs e)
        {
            //todo
        }

        private bool IsInputValid()
        {
            if (!TBoxNewPassword.Password.Equals(TBoxConfirmNewPassword.Password))
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgPasswordDoesNotMatch, MessageBoxImage.Information);
                return false;
            }
            else if (!IsPasswordSecure(TBoxNewPassword.Password))
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgInsecurePassword, MessageBoxImage.Information);
                return false;
            }

            return true;
        }

        private bool IsPasswordSecure(string newPassword)
        {
            var regex = new Regex("^(?=.*[A-Z])(?=.*[!@#$&*])(?=.*[0-9].*[0-9])(?=.*[a-z]).{8,16}$");
            return regex.IsMatch(newPassword);
        }

        private void ModifyPassword(string email, string newPassword)
        {
            bool result = _usersManagerClient.ModifyPasswordByEmail(email, newPassword);

            if (result)
            {
                MainWindow.ShowMessageBox("password modified succesfully", MessageBoxImage.Information);
                ReturnToPage();
            }
            else
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgConnectionError, MessageBoxImage.Information);
            }
        }

        private void BtnCancelEvent(object sender, RoutedEventArgs e)
        {
            ReturnToPage();
        }

        private void ReturnToPage()
        {
            if (SessionDetails.IsGuest)
            {
                SessionDetails.CleanSessionDetails();
                var login = new Launcher();
                Application.Current.MainWindow.Content = login;
            }
            else
            {
                var mainMenu = new MainMenu();
                Application.Current.MainWindow.Content = mainMenu;
            }
        }

        private void BtnContinueEvent(object sender, RoutedEventArgs e)
        {
            if (IsInputValid())
            {
                ModifyPassword(SessionDetails.Email, Sha256Encryptor.SHA256_hash(TBoxNewPassword.Password));
            }
        }
    }
}
