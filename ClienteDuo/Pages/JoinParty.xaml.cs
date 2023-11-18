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
        readonly DataService.PartyValidatorClient _partyValidatorClient;

        public JoinParty()
        {
            InitializeComponent();
            _partyValidatorClient = new DataService.PartyValidatorClient();
        }

        private void BtnJoin(object sender, RoutedEventArgs e)
        {
            if (SessionDetails.IsGuest)
            {
                GenerateGuestNameWithSeed();
            }

            string partyCodeString = TBoxPartyCode.Text.Trim();
            if (ArePartyConditionsValid(partyCodeString))
            {
                JoinLobby(Int32.Parse(partyCodeString));
            }
        }

        public void JoinLobby(int partyCode)
        {
            SessionDetails.PartyCode = partyCode;
            InviteeLobby inviteeLobby = new InviteeLobby(SessionDetails.Username);
            App.Current.MainWindow.Content = inviteeLobby;
        }

        private bool ArePartyConditionsValid(string partyCode)
        {
            if (!IsInputInteger(partyCode))
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgInvalidPartyCodeFormat);
            }
            else if (!IsPartyCodeExistent(Int32.Parse(partyCode)))
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

        private void GenerateGuestNameWithSeed()
        {
            Random rand = new Random();
            int id = rand.Next(0,1000); // validate in server if a player with same name exists
            SessionDetails.Username = "guest" + id;
        }

        public bool IsInputInteger(string code)
        {
            return Int32.TryParse(code, out _);
        }

        public bool IsPartyCodeExistent(int partyCode)
        {
            return _partyValidatorClient.IsPartyExistent(partyCode);
        }

        public bool IsSpaceAvailable(int partyCode)
        {
            return _partyValidatorClient.IsSpaceAvailable(partyCode);
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
