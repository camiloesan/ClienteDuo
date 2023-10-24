using System.Windows;
using System.Windows.Controls;

namespace ClienteDuo.Pages
{
    public partial class MainMenu : Page
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void BtnQuitGame(object sender, RoutedEventArgs e)
        {
            App.Current.MainWindow.Close();
        }

        private void BtnNewParty(object sender, RoutedEventArgs e)
        {
            Lobby lobby = new Lobby();
            App.Current.MainWindow.Content = lobby;
        }

        private void BtnJoinParty(object sender, RoutedEventArgs e)
        {
            JoinParty joinParty = new JoinParty();
            App.Current.MainWindow.Content = joinParty;
        }
    }
}
