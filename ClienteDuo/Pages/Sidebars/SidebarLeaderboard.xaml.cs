using System.Windows;
using System.Windows.Controls;

namespace ClienteDuo.Pages.Sidebars
{
    /// <summary>
    /// Interaction logic for SidebarLeaderboard.xaml
    /// </summary>
    public partial class SidebarLeaderboard : UserControl
    {
        public SidebarLeaderboard()
        {
            InitializeComponent();
        }

        private void BtnCancel(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
        }
    }
}
