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
            LblPlayerID.Content = SessionDetails.UserID;
            LblUsername.Content = SessionDetails.Username;
            LblEmail.Content = SessionDetails.Email;
        }

        private void BtnCloseBar(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
        }
    }
}
