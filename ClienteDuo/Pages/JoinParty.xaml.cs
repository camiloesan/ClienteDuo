using ClienteDuo.Utilities;
using System;
using System.Windows;
using System.Windows.Controls;

namespace ClienteDuo.Pages
{
    public partial class JoinParty : Page
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public JoinParty()
        {
            InitializeComponent();
            EnableOrDisableGuestFields();
        }

        private void EnableOrDisableGuestFields()
        {
            if (SessionDetails.IsGuest)
            {
                LblUsername.Visibility = Visibility.Visible;
                TBoxUsername.Visibility = Visibility.Visible;
            }
        }

        private void BtnJoin(object sender, RoutedEventArgs e)
        {
            if (SessionDetails.IsGuest)
            {
                SessionDetails.Username = TBoxUsername.Text.Trim(); //validar longitud de nombre
            }

            bool isInteger = false;
            try // all this block in another function
            {
                SessionDetails.PartyCode = Int32.Parse(TBoxPartyCode.Text.Trim());
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
            else if (!IsPartyCodeCorrect(SessionDetails.PartyCode))
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgPartyNotFound);
            }
            else if (!IsSpaceAvailable(SessionDetails.PartyCode))
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

        private bool IsSpaceAvailable(int partyCode)
        {
            DataService.PartyValidatorClient client = new DataService.PartyValidatorClient();
            return client.IsSpaceAvailable(partyCode);
        }

        private void BtnCancel(object sender, RoutedEventArgs e)
        {

            if (SessionDetails.IsGuest)
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
