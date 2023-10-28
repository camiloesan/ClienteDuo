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
            LblPlayerID.Content = SessionDetails.userID;
            LblUsername.Content = SessionDetails.username;
            LblEmail.Content = SessionDetails.email;
        }

        private void BtnCloseBar(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
        }
    }
}
