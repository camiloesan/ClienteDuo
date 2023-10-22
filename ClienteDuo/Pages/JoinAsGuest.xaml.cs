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
    /// Interaction logic for JoinAsGuest.xaml
    /// </summary>
    public partial class JoinAsGuest : Page
    {
        public static int PARTY_CODE = 0;

        public JoinAsGuest()
        {
            InitializeComponent();
        }

        private void BtnCancel(object sender, RoutedEventArgs e)
        {
            Launcher launcher = new Launcher();
            App.Current.MainWindow.Content = launcher;
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

            if (!isInteger)
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgInvalidPartyCodeFormat);
            }
            else if (!IsPartyCodeCorrect(PARTY_CODE))
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgPartyNotFound);
            }
            else if (!IsSpaceAvailable(PARTY_CODE))
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgFullParty);
            }
            else
            {
                InviteeLobby inviteeLobby = new InviteeLobby();
                App.Current.MainWindow.Content = inviteeLobby;
            }
        }

        private bool IsPartyCodeCorrect(int partyCode)
        {
            DataService.PartyValidatorClient client = new DataService.PartyValidatorClient();
            return client.IsPartyExistent(partyCode);
        }

        private bool IsSpaceAvailable(int partyCode) //thread insecure?
        {
            DataService.PartyValidatorClient client = new DataService.PartyValidatorClient();
            return client.IsSpaceAvailable(partyCode);
        }
    }
}
