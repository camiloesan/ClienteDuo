using System.Windows;
using System.Windows.Controls;

namespace ClienteDuo.Pages
{
    public partial class SidebarUserProfile : UserControl
    {
        public SidebarUserProfile()
        {
            InitializeComponent();
        }

        private void BtnCloseBar(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
        }
    }
}
