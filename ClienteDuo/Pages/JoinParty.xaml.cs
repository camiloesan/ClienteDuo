using ClienteDuo.Utilities;
using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows;
using System.Windows.Controls;
using ClienteDuo.DataService;

namespace ClienteDuo.Pages
{
    public partial class JoinParty : Page
    {
        private readonly PartyValidatorClient _partyValidatorClient;

        public JoinParty()
        {
            InitializeComponent();
            _partyValidatorClient = new PartyValidatorClient();
        }

        private void BtnJoinEvent(object sender, RoutedEventArgs e)
        {
            var partyCodeString = TBoxPartyCode.Text.Trim();
            if (!ArePartyConditionsValid(partyCodeString)) return;
            
            if (SessionDetails.IsGuest)
            {
                GenerateGuestName(partyCodeString);
            }

            JoinLobby(int.Parse(partyCodeString));
        }

        public void JoinLobby(int partyCode)
        {
            SessionDetails.PartyCode = partyCode;
            var lobby = new Lobby(SessionDetails.Username, SessionDetails.PartyCode);
            Application.Current.MainWindow.Content = lobby;
        }

        private bool ArePartyConditionsValid(string partyCode)
        {
            if (!IsInputInteger(partyCode))
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgInvalidPartyCodeFormat);
            }
            else if (!IsPartyCodeExistent(int.Parse(partyCode)))
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgPartyNotFound);
            }
            else if (!IsSpaceAvailable(int.Parse(partyCode)))
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgFullParty);
            }
            else
            {
                return true;
            }

            return false;
        }

        private void GenerateGuestName(string partyCodeString)
        {
            var randomId = new Random();
            var id = randomId.Next(0,1000);
            string randomUsername = "guest" + id;
            if (_partyValidatorClient.IsUsernameInParty(int.Parse(partyCodeString), randomUsername))
            {
                GenerateGuestName(randomUsername);
            }
            SessionDetails.Username = randomUsername;
        }

        public bool IsInputInteger(string code)
        {
            return int.TryParse(code, out _);
        }

        public bool IsPartyCodeExistent(int partyCode)
        {
            return _partyValidatorClient.IsPartyExistent(partyCode);
        }

        public bool IsSpaceAvailable(int partyCode)
        {
            return _partyValidatorClient.IsSpaceAvailable(partyCode);
        }

        private void BtnCancelEvent(object sender, RoutedEventArgs e)
        {
            if (SessionDetails.IsGuest)
            {
                var launcher = new Launcher();
                Application.Current.MainWindow.Content = launcher;
            }
            else
            {
                var mainMenu = new MainMenu();
                Application.Current.MainWindow.Content = mainMenu;
            }
        }
    }
}
