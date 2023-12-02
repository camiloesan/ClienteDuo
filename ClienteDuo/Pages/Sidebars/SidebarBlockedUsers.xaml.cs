using ClienteDuo.DataService;
using ClienteDuo.Utilities;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
namespace ClienteDuo.Pages.Sidebars
{
    /// <summary>
    /// Interaction logic for SidebarBlockedUsers.xaml
    /// </summary>
    public partial class SidebarBlockedUsers : UserControl
    {
        private readonly UsersManagerClient _usersManagerClient;

        public SidebarBlockedUsers()
        {
            InitializeComponent();
            _usersManagerClient = new UsersManagerClient();
            FillBlockedUsersPanel();
        }

        private void FillBlockedUsersPanel()
        {
            PanelBlockedUsers.Children.Clear();
            IEnumerable<UserBlockedDTO> blockedUsersList = GetBlockedUsersListByUserId(SessionDetails.UserId);
            foreach (UserBlockedDTO blockedUser in blockedUsersList)
            {
                PanelBlockedUsers.Children.Clear();
                StackPanel stackPanel = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                PanelBlockedUsers.Children.Add(stackPanel);

                Label lblSender = new Label
                {
                    Content = blockedUser.BlockedUsername
                };
                stackPanel.Children.Add(lblSender);

                Button btnUnblock = new Button
                {
                    Content = Properties.Resources.BtnUnblockUser,
                    DataContext = blockedUser
                };
                btnUnblock.Click += UnblockUserEvent;
                stackPanel.Children.Add(btnUnblock);
            }
        }

        private void UnblockUserEvent(object sender, RoutedEventArgs e)
        {
            UserBlockedDTO blockedUser = ((FrameworkElement)sender).DataContext as UserBlockedDTO;
            if (UnblockUserByBlockId(blockedUser.BlockID))
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgUnblockSuccess, MessageBoxImage.Information);
                MainMenu mainMenu = new MainMenu();
                Application.Current.MainWindow.Content = mainMenu;
            }
            else
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgConnectionError, MessageBoxImage.Error);
            }
        }

        private bool UnblockUserByBlockId(int blockId)
        {
            return _usersManagerClient.UnblockUserByBlockId(blockId);
        }

        private IEnumerable<UserBlockedDTO> GetBlockedUsersListByUserId(int userId)
        {
            return _usersManagerClient.GetBlockedUsersListByUserId(userId);
        }

        private void BtnCancelEvent(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
        }
    }
}
