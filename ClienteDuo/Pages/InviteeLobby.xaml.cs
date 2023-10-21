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
    public partial class InviteeLobby : Page, DataService.IPartyManagerCallback
    {
        public InviteeLobby()
        {
            InitializeComponent();
            Join();
        }

        private void Join()
        {
            InstanceContext instanceContext = new InstanceContext(this);
            DataService.PartyManagerClient client = new DataService.PartyManagerClient(instanceContext);

            client.JoinParty(JoinParty.PARTY_CODE, Login.ACTIVE_EMAIL);
        }

        public void MessageReceived(string messageSent)
        {
            Label labelMessageReceived = new Label();
            labelMessageReceived.HorizontalAlignment = HorizontalAlignment.Left;
            labelMessageReceived.Foreground = new SolidColorBrush(Colors.White);
            labelMessageReceived.FontSize = 14;
            labelMessageReceived.Content = messageSent;

            chatPanel.Children.Add(labelMessageReceived);
            chatScrollViewer.ScrollToEnd();
        }
        private void SendMessage(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                InstanceContext instanceContext = new InstanceContext(this);
                DataService.PartyManagerClient client = new DataService.PartyManagerClient(instanceContext);

                string message = Login.ACTIVE_EMAIL + ": " + TBoxMessage.Text;
                TBoxMessage.Text = "";
                client.SendMessage(JoinParty.PARTY_CODE, message); //partycode no good
            }
        }

        public void PartyCreated(Dictionary<string, object> playersInLobby)
        {
            throw new NotImplementedException();
        }

        public void PlayerJoined(Dictionary<string, object> playersInLobby)
        {
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
                Label label = new Label();
                label.HorizontalAlignment = HorizontalAlignment.Center;
                label.Foreground = new SolidColorBrush(Colors.White);
                label.FontSize = 14;
                label.Content = keyValuePair.Key;

                playersPanel.Children.Add(label);
            }
        }

        private void BtnExitLobby(object sender, RoutedEventArgs e)
        {
            MainMenu mainMenu = new MainMenu();
            App.Current.MainWindow.Content = mainMenu;

            InstanceContext instanceContext = new InstanceContext(this);
            DataService.PartyManagerClient client = new DataService.PartyManagerClient(instanceContext);

            client.LeaveParty(JoinParty.PARTY_CODE, Login.ACTIVE_EMAIL);
        }
    }
}
