using ClienteDuo.Pages;
using System;
using System.Windows;
using System.Windows.Controls;

namespace ClienteDuo
{
    public partial class NewAccount : Page
    {
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
                    MainWindow.ShowMessageBox("Ocurrió un error en la basa de datos, no se pudo registrar el usuario");
                }
            }
        }

        private bool AreFieldsValid()
        {
            if (!AreFieldsEmpty() && AreFieldsLengthValid() && IsPasswordMatch())
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
            string passwordField = TBoxPassword.Password.Trim();

            if (usernameField.Length > 30)
            {
                MainWindow.ShowMessageBox("el maximo de caracteres para el username es de 30 ***");
                return false;
            } 
            else if (emailField.Length > 30)
            {
                MainWindow.ShowMessageBox("el maximo de caracteres para el correo electronico es de 30***");
                return false;
            }
            else if (passwordField.Length < 8)
            {
                MainWindow.ShowMessageBox("La contraseña debe tener un minimo de 8 caracteres***");
                return false;
            }
            return true;
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
                MainWindow.ShowMessageBox("Todos los campos deben estar llenos *pendiente internacionalizar*");
                return true;
            }
            return false;
        }

        private bool AddUserToDatabase()
        {
            string username = TBoxUsername.Text;
            string email = TBoxEmail.Text;
            string password = TBoxPassword.Password;
            DataService.UsersManagerClient client = new DataService.UsersManagerClient();

            bool result = false;
            try
            {
                result = client.AddUserToDatabase(username, email, password);
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result;
        }
    }
}
