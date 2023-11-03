using ClienteDuo.Utilities;
using System;
using System.Runtime.InteropServices.WindowsRuntime;
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
        }

        private void BtnJoin(object sender, RoutedEventArgs e)
        {
            if (SessionDetails.IsGuest)
            {
                GenerateGuestNameWithSeed(SessionDetails.PartyCode);
            }

            string partyCode = TBoxPartyCode.Text.Trim();
            if (ArePartyConditionsValid(partyCode))
            {
                SessionDetails.PartyCode = Int32.Parse(partyCode);
                InviteeLobby inviteeLobby = new InviteeLobby();
                App.Current.MainWindow.Content = inviteeLobby;
            }
        }

        private bool ArePartyConditionsValid(string partyCode)
        {
            if (!IsInputInteger(partyCode))
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgInvalidPartyCodeFormat);
            }
            else if (!IsPartyCodeCorrect(Int32.Parse(partyCode)))
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgPartyNotFound);
            }
            else if (!IsSpaceAvailable(Int32.Parse(partyCode)))
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgFullParty);
            }
            else
            {
                return true;
            }

            return false;
        }

        private void GenerateGuestNameWithSeed(int seed)
        {
            Random rand = new Random(seed);
            int id = rand.Next(0,1000); // validate in server if a player with same name exists
            SessionDetails.Username = "guest" + id;
        }

        public bool IsInputInteger(string code)
        {
            bool isInteger = true;
            try
            {
                Int32.Parse(code);
            }
            catch
            {
                return false;
            }

            return isInteger;
        }

        public bool IsPartyCodeCorrect(int partyCode)
        {
            DataService.PartyValidatorClient client = new DataService.PartyValidatorClient();
            return client.IsPartyExistent(partyCode);
        }

        public bool IsSpaceAvailable(int partyCode)
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
