using System.Windows;
using System.Windows.Controls;

namespace ClienteDuo.Pages
{
    public partial class MainMenu : Page
    {
        SidebarUserProfile sidebarUserProfile = new SidebarUserProfile();

        public MainMenu()
        {
            InitializeComponent();
            sidebarUserProfile.Margin = new Thickness(0, 0, 700, 0); // poner en función
            sidebarUserProfile.Visibility = Visibility.Collapsed;
            MainGrid.Children.Add(sidebarUserProfile);
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

        private void BtnMyProfileSidebar(object sender, RoutedEventArgs e)
        {
            sidebarUserProfile.Visibility = Visibility.Visible;
        }
    }
}
