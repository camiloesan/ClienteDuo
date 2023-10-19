using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            string password = TBoxPassword.Text;

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
