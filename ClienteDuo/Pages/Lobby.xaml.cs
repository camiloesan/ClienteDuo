using System.Collections.Generic;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ClienteDuo.Pages
{
    public partial class Lobby : Page, DataService.IPartyManagerCallback
    {
        private Dictionary<string, object> players = new Dictionary<string, object>();

        public Lobby()
        {
            InitializeComponent();
            InstanceContext instanceContext = new InstanceContext(this);
            DataService.PartyManagerClient client = new DataService.PartyManagerClient(instanceContext);
            client.JoinParty(Login.ACTIVE_EMAIL);
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
                client.SendMessage(message);
            }
        }

        private void BtnExitLobby(object sender, RoutedEventArgs e)
        {
            MainMenu mainMenu = new MainMenu();
            App.Current.MainWindow.Content = mainMenu;

            InstanceContext instanceContext = new InstanceContext(this);
            DataService.PartyManagerClient client = new DataService.PartyManagerClient(instanceContext);
            client.LeaveParty(Login.ACTIVE_EMAIL);
        }

        public void PlayerJoined(Dictionary<string, object> playersInLobby)
        {
            players = playersInLobby;

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
    }
}
