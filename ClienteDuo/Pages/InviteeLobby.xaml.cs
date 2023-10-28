using ClienteDuo.Utilities;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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

            client.JoinParty(SessionDetails.PartyCode, SessionDetails.Username);
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

                string message = SessionDetails.Username + ": " + TBoxMessage.Text;
                TBoxMessage.Text = "";
                client.SendMessage(SessionDetails.PartyCode, message);
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
            MusicManager.PlayPlayerJoinedSound();
            UpdatePlayerList(playersInLobby);
        }

        public void PlayerLeft(Dictionary<string, object> playersInLobby)
        {
            MusicManager.PlayPlayerLeftSound();
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

            InstanceContext instanceContext = new InstanceContext(this);
            DataService.PartyManagerClient client = new DataService.PartyManagerClient(instanceContext);

            client.LeaveParty(SessionDetails.PartyCode, SessionDetails.Username);
        }
    }
}
