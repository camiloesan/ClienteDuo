using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClienteDuo.Pages.Sidebars
{
    /// <summary>
    /// Interaction logic for PopUpUserDetails.xaml
    /// </summary>
    public partial class PopUpUserDetails : UserControl
    {
        public PopUpUserDetails()
        {
            InitializeComponent();
        }

        public void SetDataContext(string username, bool isFriend)
        {
            DataContext = username;
            LblUsername.Content = username;

            if (isFriend)
            {
                BtnAddFriend.Visibility = Visibility.Collapsed;
            }
        }

        private void BtnAddFriendEvent(object sender, RoutedEventArgs e)
        {

        }

        private void BtnCancelEvent(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
        }
    }
}
