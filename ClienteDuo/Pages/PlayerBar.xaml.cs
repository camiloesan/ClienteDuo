using ClienteDuo.DataService;
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

namespace ClienteDuo.Pages
{
    /// <summary>
    /// Interaction logic for PlayerBar.xaml
    /// </summary>
    public partial class PlayerBar : UserControl
    {
        private string _username;
        private MatchManagerClient _client;

        public PlayerBar()
        {
            InitializeComponent();
        }

        public string Username {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
                LblUsername.Content = _username;
            }
        }

        public void SetClient(MatchManagerClient client)
        {
            _client = client;
        }

        private void BtnAddFriendEvent(object sender, RoutedEventArgs e)
        {
            //TODO
        }

        private void BtnKickEvent(object sender, RoutedEventArgs e)
        {
            _client.KickPlayerFromGame(SessionDetails.PartyCode, _username);
        }
    }
}
