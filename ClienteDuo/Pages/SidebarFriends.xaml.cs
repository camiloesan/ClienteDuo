using System.Windows;
using System.Windows.Controls;

namespace ClienteDuo.Pages
{
    public partial class SidebarFriends : UserControl
    {
        SidebarAddFriend sidebarAddFriend;

        public SidebarFriends()
        {
            InitializeComponent();
            sidebarAddFriend = new SidebarAddFriend
            {
                Margin = new Thickness(700, 500, 110, 110),
                Visibility = Visibility.Collapsed
            };
            FriendsBar.Children.Add(sidebarAddFriend);
        }

        private void BtnCancel(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            sidebarAddFriend.Visibility = Visibility.Visible;
        }
    }
}
