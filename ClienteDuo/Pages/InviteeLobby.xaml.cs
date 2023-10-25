using ClienteDuo.Utilities;
using System;
using System.CodeDom;
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
    public partial class InviteeLobby : Page, DataService.IPartyManagerCallback
    {
        const int MESSAGE_MAX_LENGTH = 250;

        public InviteeLobby()
        {
            InitializeComponent();
            JoinGame();
        }

        private void JoinGame()
        {
            InstanceContext instanceContext = new InstanceContext(this);
            DataService.PartyManagerClient client = new DataService.PartyManagerClient(instanceContext);

            client.JoinParty(JoinParty.PARTY_CODE, SessionDetails.username);
        }

        public void MessageReceived(string messageSent)
        {
            Label labelMessageReceived = new Label
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                Foreground = new SolidColorBrush(Colors.White),
                FontSize = 14,
                Content = messageSent
            };

            chatPanel.Children.Add(labelMessageReceived);
            chatScrollViewer.ScrollToEnd();
        }

        private void SendMessage(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return && TBoxMessage.Text.Trim() != null)
            {
                InstanceContext instanceContext = new InstanceContext(this);
                DataService.PartyManagerClient client = new DataService.PartyManagerClient(instanceContext);

                string message = SessionDetails.username + ": " + TBoxMessage.Text;
                TBoxMessage.Text = "";
                client.SendMessage(JoinParty.PARTY_CODE, message); //partycode no good
            } 
            else if (TBoxMessage.Text.Length > MESSAGE_MAX_LENGTH)
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgMessageMaxCharacters);
            }
        }

        public void PartyCreated(Dictionary<string, object> playersInLobby)
        {
            throw new NotImplementedException();
        }

        public void PlayerJoined(Dictionary<string, object> playersInLobby)
        {
            PlayPlayerJoinedAudio();
            UpdatePlayerList(playersInLobby);
        }

        public void PlayerLeft(Dictionary<string, object> playersInLobby)
        {
            UpdatePlayerList(playersInLobby);
        }

        private void UpdatePlayerList(Dictionary<string, object> playersInLobby)
        {
            playersPanel.Children.Clear();
            foreach (KeyValuePair<string, object> keyValuePair in playersInLobby)
            {
                Label label = new Label
                {
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Foreground = new SolidColorBrush(Colors.White),
                    FontSize = 14,
                    VerticalAlignment = VerticalAlignment.Center,
                    Content = keyValuePair.Key
                };

                playersPanel.Children.Add(label);
            }
        }

        private void BtnExitLobby(object sender, RoutedEventArgs e)
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

            InstanceContext instanceContext = new InstanceContext(this);
            DataService.PartyManagerClient client = new DataService.PartyManagerClient(instanceContext);

            client.LeaveParty(JoinParty.PARTY_CODE, SessionDetails.username);
        }

        private void PlayPlayerJoinedAudio()
        {
            MusicManager musicManager = new MusicManager("SFX\\playerJoinedSound.wav");
            musicManager.PlayMusic();
        }
    }
}
