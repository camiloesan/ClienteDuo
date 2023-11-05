using ClienteDuo.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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
        private bool _isWPFRunning = true;

        public InviteeLobby(string username)
        {
            InitializeComponent();
            JoinGame(SessionDetails.PartyCode, username);
        }

        public InviteeLobby()
        {
            try
            {
                _ = App.Current.Windows;
            }
            catch
            {
                _isWPFRunning = false;
            }
        }

        public void JoinGame(int partyCode, string username)
        {
            SessionDetails.PartyCode = partyCode;
            SessionDetails.Username = username;
            InstanceContext instanceContext = new InstanceContext(this);
            DataService.PartyManagerClient client = new DataService.PartyManagerClient(instanceContext);

            client.JoinParty(partyCode, username);
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
                Button BtnViewProfile = new Button
                {
                    Content = "*Profile*",
                    Margin = new Thickness(5, 0, 0, 0),
                    VerticalAlignment = VerticalAlignment.Center
                };
                stackPanel.Children.Add(BtnViewProfile);
            }
        }

        public void NotifyMessageReceived(string messageSent)
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

        public void NotifyPartyCreated(Dictionary<string, object> playersInLobby)
        {
            throw new NotImplementedException();
        }

        public void NotifyPlayerJoined(Dictionary<string, object> playersInLobby)
        {
            if (_isWPFRunning)
            {
                MusicManager.PlayPlayerJoinedSound();
                UpdatePlayerList(playersInLobby);
            }
            
        }

        public void NotifyPlayerLeft(Dictionary<string, object> playersInLobby)
        {
            if (_isWPFRunning)
            {
                MusicManager.PlayPlayerLeftSound();
                UpdatePlayerList(playersInLobby);
            }
        }

        private void UpdatePlayerList(Dictionary<string, object> playersInLobby)
        {
            playersPanel.Children.Clear();
            foreach (KeyValuePair<string, object> keyValuePair in playersInLobby)
            {
                CreatePlayerPanel(keyValuePair.Key);
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

            ExitParty(SessionDetails.PartyCode, SessionDetails.Username);
        }

        public void ExitParty(int partyCode, string username)
        {
            InstanceContext instanceContext = new InstanceContext(this);
            DataService.PartyManagerClient client = new DataService.PartyManagerClient(instanceContext);

            client.LeaveParty(partyCode, username);
        }

        public void NotifyGameStarted()
        {
            CardTable cardTable = new CardTable();
            App.Current.MainWindow.Content = cardTable;
        }

        public void NotifyPlayerKicked()
        {
            if (_isWPFRunning)
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
}
