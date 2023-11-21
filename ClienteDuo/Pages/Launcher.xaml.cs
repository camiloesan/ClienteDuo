using System.Windows;
using System.Windows.Controls;

namespace ClienteDuo.Pages
{
    public partial class Launcher : Page
    {
        public Launcher()
        {
            InitializeComponent();
        }

        private void BtnNewAccount(object sender, RoutedEventArgs e)
        {
            var newAccount = new NewAccount();
            Application.Current.MainWindow.Content = newAccount;
        }

        private void BtnLocalizationEnUS(object sender, RoutedEventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
            var launcher = new Launcher();
            Application.Current.MainWindow.Content = launcher;
        }

        private void BtnLocalizationEsMX(object sender, RoutedEventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("es");
            var launcher = new Launcher();
            Application.Current.MainWindow.Content = launcher;
        }

        private void BtnLogin(object sender, RoutedEventArgs e)
        {
            var login = new Login();
            Application.Current.MainWindow.Content = login;
        }

        private void BtnJoinAsGuest(object sender, RoutedEventArgs e)
        {
            var joinParty = new JoinParty();
            Application.Current.MainWindow.Content = joinParty;
        }

        private void BtnLocalizationJaJP(object sender, RoutedEventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("jp");
            var launcher = new Launcher();
            Application.Current.MainWindow.Content = launcher;
        }

        private void BtnLocalizationFrFR(object sender, RoutedEventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("fr");
            var launcher = new Launcher();
            Application.Current.MainWindow.Content = launcher;
        }
    }
}
