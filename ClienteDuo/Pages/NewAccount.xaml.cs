using ClienteDuo.DataService;
using ClienteDuo.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClienteDuo
{
    /// <summary>
    /// Interaction logic for NewAccount.xaml
    /// </summary>
    public partial class NewAccount : Page
    {
        public NewAccount()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Launcher launcher = new Launcher();
            App.Current.MainWindow.Content = launcher;
        }

        private void BtnContinue_Click(object sender, RoutedEventArgs e)
        {
            if (AreFieldsValid())
            {
                AddUserExternally();
                ShowUsersOnServer();
                ShowMessageBox("El usuario fue registrado exitosamente");
            }
        }

        private void ShowUsersOnServer()
        {
            DataService.DatabaseServiceClient client = new DataService.DatabaseServiceClient();
            client.ShowUsers();
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
                ShowMessageBox("el maximo de caracteres para el username es de 30 ***");
                return false;
            } 
            else if (emailField.Length > 30)
            {
                ShowMessageBox("el maximo de caracteres para el correo electronico es de 30***");
                return false;
            }
            else if (passwordField.Length < 8)
            {
                ShowMessageBox("La contraseña debe tener un minimo de 8 caracteres***");
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
                ShowMessageBox("Todos los campos deben estar llenos *pendiente internacionalizar*");
                return true;
            }
            return false;
        }

        private void ShowMessageBox(string message)
        {
            //ResourceManager resourceManager = new ResourceManager("resx", Assembly.GetExecutingAssembly());
            string messageBoxText = message;
            string caption = "Alert *internacionalizar*";

            MessageBoxButton okButton = MessageBoxButton.OK;
            MessageBoxImage warningIcon = MessageBoxImage.Warning;
            MessageBox.Show(messageBoxText, caption, okButton, warningIcon);
        }

        private void AddUserExternally()
        {
            string username = TBoxUsername.Text;
            string email = TBoxEmail.Text;
            string password = TBoxPassword.Password;
            DataService.DatabaseServiceClient client = new DataService.DatabaseServiceClient();
            client.AddUserToDatabase(username, email, password);
        }
    }
}
