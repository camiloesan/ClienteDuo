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
            NewAccount newAccount = new NewAccount();
            App.Current.MainWindow.Content = newAccount;
        }

        private void ButtonLocalizationEnUS(object sender, RoutedEventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
            Launcher launcher = new Launcher();
            App.Current.MainWindow.Content = launcher;
        }

        private void ButtonLocalizationEsMX(object sender, RoutedEventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("es");
            Launcher launcher = new Launcher();
            App.Current.MainWindow.Content = launcher;
        }

        private void BtnLogin(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            App.Current.MainWindow.Content = login;
        }

        private void BtnJoinAsGuest(object sender, RoutedEventArgs e)
        {
            JoinAsGuest joinAsGuest = new JoinAsGuest();
            App.Current.MainWindow.Content = joinAsGuest;
        }
    }
}
