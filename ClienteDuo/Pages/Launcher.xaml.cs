using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
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

namespace ClienteDuo.Pages
{
    /// <summary>
    /// Interaction logic for Launcher.xaml
    /// </summary>
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
