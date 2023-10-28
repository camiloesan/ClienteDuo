using ClienteDuo.Utilities;
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
    /// Interaction logic for SidebarAddFriend.xaml
    /// </summary>
    public partial class SidebarAddFriend : UserControl
    {
        public SidebarAddFriend()
        {
            InitializeComponent();
        }

        private void BtnClose(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataService.UsersManagerClient client = new DataService.UsersManagerClient();
            int userReceiverID = Int32.Parse(TBoxUserReceiver.Text);
            client.SendFriendRequest(SessionDetails.userID, userReceiverID);
        }
    }
}
