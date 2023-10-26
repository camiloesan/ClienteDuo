using ClienteDuo.Utilities;
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
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static int PARTY_CODE = 0;

        public JoinParty()
        {
            InitializeComponent();
            EnableOrDisableGuestFields();
        }

        private void EnableOrDisableGuestFields()
        {
            if (SessionDetails.isGuest)
            {
                LblUsername.Visibility = Visibility.Visible;
                TBoxUsername.Visibility = Visibility.Visible;
            }
        }

        private void BtnJoin(object sender, RoutedEventArgs e)
        {
            if (SessionDetails.isGuest)
            {
                SessionDetails.username = TBoxUsername.Text.Trim(); //validar longitud de nombre
            }

            bool isInteger = false;
            try // all this block in another function
            {
                PARTY_CODE = Int32.Parse(TBoxPartyCode.Text.Trim());
                isInteger = true;
            } 
            catch (Exception ex)
            {
                log.Error(ex);
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

        private void BtnCancel(object sender, RoutedEventArgs e)
        {

            if (SessionDetails.isGuest)
            {
                Launcher launcher = new Launcher();
                App.Current.MainWindow.Content = launcher;
            }
            else
            {
                MainMenu mainMenu = new MainMenu();
                App.Current.MainWindow.Content = mainMenu;
            }
        }
    }
}
