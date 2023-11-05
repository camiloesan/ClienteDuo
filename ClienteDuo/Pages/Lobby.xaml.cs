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
    public partial class Lobby : Page, DataService.IPartyManagerCallback
    {
        const int MESSAGE_MAX_LENGTH = 250;
        int partyCode = 0;
        Dictionary<string, object> players = new Dictionary<string, object>();

        public Lobby()
        {
            InitializeComponent();
            CreateNewParty();
        }

        private void CreateNewParty()
        {
            InstanceContext instanceContext = new InstanceContext(this);
            DataService.PartyManagerClient client = new DataService.PartyManagerClient(instanceContext);

            Random rand = new Random();
            partyCode = rand.Next(0, 10000); //re do until it is not the same
            SessionDetails.PartyCode = partyCode;

            client.NewParty(partyCode, SessionDetails.Username);
            LblPartyCode.Content = Properties.Resources.LblPartyCode + ": " + partyCode;


            MusicManager.PlayPlayerJoinedSound();
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
            if (e.Key == Key.Return && TBoxMessage.Text.Trim().Length > 0)
            {
                InstanceContext instanceContext = new InstanceContext(this);
                DataService.PartyManagerClient client = new DataService.PartyManagerClient(instanceContext);

                string message = SessionDetails.Username + ": " + TBoxMessage.Text;
                TBoxMessage.Text = "";
                client.SendMessage(partyCode, message);
            }
            else if (TBoxMessage.Text.Length > MESSAGE_MAX_LENGTH)
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgMessageMaxCharacters);
            }
        }

        private void BtnExitLobby(object sender, RoutedEventArgs e)
        {
            MusicManager.PlayPlayerLeftSound();
            MainMenu mainMenu = new MainMenu();
            App.Current.MainWindow.Content = mainMenu;

            InstanceContext instanceContext = new InstanceContext(this);
            DataService.PartyManagerClient client = new DataService.PartyManagerClient(instanceContext);
            client.LeaveParty(partyCode, SessionDetails.Username);
        }

        public void PlayerJoined(Dictionary<string, object> playersInLobby)
        {
            players = playersInLobby;
            MusicManager.PlayPlayerJoinedSound();
            UpdatePlayerList(playersInLobby);
        }

        public void PlayerLeft(Dictionary<string, object> playersInLobby)
        {
            players = playersInLobby;
            MusicManager.PlayPlayerLeftSound();
            UpdatePlayerList(playersInLobby);
        }

        private void UpdatePlayerList(Dictionary<string, object> playersInLobby)
        {
            playersPanel.Children.Clear();
            foreach (KeyValuePair<string, object> keyValuePair in playersInLobby)
            {
                CreatePlayerPanel(keyValuePair.Key);
            }
        }

        private void CreatePlayerPanel(string username)
        {
            var backgroundColor = new SolidColorBrush(); 
            if (username == SessionDetails.Username)
            {
                backgroundColor = new SolidColorBrush(Colors.Gold);
            }
            else
            {
                backgroundColor = new SolidColorBrush(Colors.DimGray);
            }

            StackPanel stackPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Center,
                Background = backgroundColor,
                Margin = new Thickness(15, 20, 15, 20),
                Width = 200,
                Height = 40,
            };
            playersPanel.Children.Add(stackPanel);

            Label usernameName = new Label
            {
                Foreground = new SolidColorBrush(Colors.Black),
                Content = username,
                Margin = new Thickness(10, 0, 5, 0),
                VerticalAlignment = VerticalAlignment.Center
            };
            stackPanel.Children.Add(usernameName);

            if (username != SessionDetails.Username)
            {
                Button BtnKick = new Button
                {
                    Content = "*kick*",
                    Margin = new Thickness(5, 0, 0, 0),
                    VerticalAlignment = VerticalAlignment.Center,
                };
                BtnKick.Click += (object sender, RoutedEventArgs e) =>
                {
                    DataService.PartyManagerClient client = new DataService.PartyManagerClient(new InstanceContext(this));
                    client.KickPlayer(SessionDetails.PartyCode, username);
                };
                stackPanel.Children.Add(BtnKick);

                Button BtnViewProfile = new Button
                {
                    Content = "*Profile*",
                    Margin = new Thickness(5, 0, 0, 0),
                    VerticalAlignment = VerticalAlignment.Center
                };
                stackPanel.Children.Add(BtnViewProfile);
            }
        }

        public void PartyCreated(Dictionary<string, object> playersInLobby)
        {
            UpdatePlayerList(playersInLobby);
        }

        private void BtnStartGame(object sender, RoutedEventArgs e)
        {
            InstanceContext instanceContext = new InstanceContext(this);
            DataService.PartyManagerClient client = new DataService.PartyManagerClient(instanceContext);
            client.StartGame(partyCode);
        }

        public void GameStarted()
        {
            CardTable cardTable = new CardTable();
            App.Current.MainWindow.Content = cardTable;
        }

        public void PlayerKicked()
        {
            
        }
    }
}
