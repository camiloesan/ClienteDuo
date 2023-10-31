using System.Windows;
using System.Windows.Controls;

namespace ClienteDuo.Pages
{
    /// <summary>
    /// Interaction logic for GameMenu.xaml
    /// </summary>
    public partial class GameMenu : UserControl
    {
        public GameMenu()
        {
            InitializeComponent();
        }

        private void _exitButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _hideMenuButton_Click(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
        }
    }
}
