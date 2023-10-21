using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
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
    public partial class JoinParty : Page
    {
        public static int PARTY_CODE = 0;

        public JoinParty()
        {
            InitializeComponent();
        }

        private void BtnJoin(object sender, RoutedEventArgs e)
        {
            bool isInteger = false;
            try
            {
                PARTY_CODE = Int32.Parse(TBoxPartyCode.Text.Trim());
                isInteger = true;
            } 
            catch (Exception ex)
            {
                isInteger = false;
            }

            if (isInteger)
            {
                InviteeLobby inviteeLobby = new InviteeLobby();
                App.Current.MainWindow.Content = inviteeLobby;
            }

        }

        private void BtnCancel(object sender, RoutedEventArgs e)
        {
            MainMenu mainMenu = new MainMenu();
            App.Current.MainWindow.Content = mainMenu;
        }
    }
}
