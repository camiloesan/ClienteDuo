using ClienteDuo.DataService;

using ClienteDuo.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ClienteDuo.Pages.Sidebars
{
    /// <summary>
    /// Interaction logic for SidebarLeaderboard.xaml
    /// </summary>
    public partial class SidebarLeaderboard : UserControl
    {
        private readonly UsersManagerClient _usersManagerClient = new UsersManagerClient();

        public SidebarLeaderboard()
        {
            InitializeComponent();
            FillFriendsPanel();
        }

        private void FillFriendsPanel()
        {
            PanelLeaderboard.Children.Clear();
            var userList = GetLeaderboard();
            foreach (var user in userList)
            {
                CreateUserPanel(user);
            }
        }

        private void CreateUserPanel(UserDTO user)
        {
            var stackPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(0, 7, 0, 0),
                HorizontalAlignment = HorizontalAlignment.Center,
                Background = new SolidColorBrush(Colors.DimGray),
                Width = 200,
                Height = 40
            };
            PanelLeaderboard.Children.Add(stackPanel);

            var labelUsername = new Label
            {
                Foreground = new SolidColorBrush(Colors.Black),
                Content = user.UserName,
                Margin = new Thickness(5, 0, 10, 0),
                VerticalAlignment = VerticalAlignment.Center
            };
            stackPanel.Children.Add(labelUsername);

            var labelTotalWins = new Label
            {
                Foreground = new SolidColorBrush(Colors.Black),
                Content = user.TotalWins,
                Margin = new Thickness(5, 0, 10, 0),
                VerticalAlignment = VerticalAlignment.Center
            };
            stackPanel.Children.Add(labelTotalWins);
        }

        private IEnumerable<UserDTO> GetLeaderboard()
        {
            IEnumerable<UserDTO> resultList;
            try
            {
                resultList = _usersManagerClient.GetTopTenWinners();
            }
            catch // es necesario notificar al usuario de que algo salio mal en este caso ?
            {
                resultList = new List<UserDTO>();
            }

            return resultList;
        }

        private void BtnCancelEvent(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
        }
    }
}
