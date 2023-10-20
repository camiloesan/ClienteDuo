using System.Windows;
using System.Windows.Controls;

namespace ClienteDuo.Pages
{
    public partial class Login : Page
    {
        public static string ACTIVE_EMAIL;

        public Login()
        {
            InitializeComponent();
        }

        private void BtnLogin(object sender, RoutedEventArgs e)
        {
            DataService.UsersManagerClient client = new DataService.UsersManagerClient();
            string email = TBoxEmail.Text;
            string password = TBoxPassword.Password;

            bool isLoginValid = client.IsLoginValid(email, password);
            if (isLoginValid)
            {
                ACTIVE_EMAIL = email;
                MainMenu mainMenu = new MainMenu();
                App.Current.MainWindow.Content = mainMenu;
            }
            else
            {
                MainWindow.ShowMessageBox("El correo o contraseña no son validos *pendiente internacionalizar*");
            }
        }

        private void BtnCancel(object sender, RoutedEventArgs e)
        {
            Launcher launcher = new Launcher();
            App.Current.MainWindow.Content = launcher;
        }
    }
}
