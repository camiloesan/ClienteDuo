using ClienteDuo.Utilities;
using System.Windows;
using System.Windows.Controls;

namespace ClienteDuo.Pages.Sidebars
{
    public partial class SidebarUserProfile : UserControl
    {
        public SidebarUserProfile()
        {
            InitializeComponent();
            FillLabels();
        }

        private void FillLabels()
        {
            LblPlayerId.Content = SessionDetails.UserId;
            LblUsername.Content = SessionDetails.Username;
            LblEmail.Content = SessionDetails.Email;
            LblTotalWins.Content = SessionDetails.TotalWins;
        }

        private void BtnCancelEvent(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
        }
    }
}
