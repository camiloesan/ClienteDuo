using ClienteDuo.Utilities;
using System;
using System.Windows;
using System.Windows.Controls;

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
            client.SendFriendRequest(SessionDetails.UserID, userReceiverID);
        }
    }
}
