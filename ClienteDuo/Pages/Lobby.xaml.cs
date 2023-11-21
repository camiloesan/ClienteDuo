using ClienteDuo.DataService;
using ClienteDuo.Pages.Sidebars;
using ClienteDuo.Utilities;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ClienteDuo.Pages
{
    public partial class Lobby : Page, IPartyManagerCallback
    {
        const int MESSAGE_MAX_LENGTH = 250;
        private readonly bool _isWpfRunning = true;
        private readonly PartyManagerClient _partyManagerClient;
        private PopUpUserDetails _popUpUserDetails;
        CardTable _cardTable;
        readonly bool _isWPFRunning = true;
        readonly InstanceContext _instanceContext;
        readonly DataService.PartyManagerClient _partyManagerClient;
        PartyManager partyManager;

        public Lobby(string username)
        {
            InitializeComponent();
            var instanceContext = new InstanceContext(this);
            _partyManagerClient = new PartyManagerClient(instanceContext);
            CreateNewParty(username);
            LoadNewPartyCreatedComponents();
        }

        public Lobby()
        {
            var instanceContext = new InstanceContext(this);
            _partyManagerClient = new PartyManagerClient(instanceContext);
            try
            {
                _ = Application.Current.Windows;
            }
            catch
            {
                _isWpfRunning = false;
            }
        }

        public int CreateNewParty(string hostUsername)
        {
            var randomCode = new Random();
            SessionDetails.PartyCode = randomCode.Next(0, 10000); // validate collision
            SessionDetails.Username = hostUsername;
            _partyManagerClient.NotifyCreateParty(SessionDetails.PartyCode, SessionDetails.Username);

            return SessionDetails.PartyCode;
        }

        private void LoadNewPartyCreatedComponents()
        {
            LblPartyCode.Content = Properties.Resources.LblPartyCode + ": " + SessionDetails.PartyCode;
            MusicManager.PlayPlayerJoinedSound();
            _popUpUserDetails = new PopUpUserDetails
            {
                Width = 350,
                Height = 200,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Visibility = Visibility.Collapsed
            };
            masterGrid.Children.Add(_popUpUserDetails);
        }

        private void OnEnterSendMessage(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return && TBoxMessage.Text.Trim().Length > 0)
            {
                string message = SessionDetails.Username + ": " + TBoxMessage.Text;
                SendMessage(SessionDetails.PartyCode, message);
                TBoxMessage.Text = "";
            }
            else if (TBoxMessage.Text.Length > MESSAGE_MAX_LENGTH)
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgMessageMaxCharacters);
            }
        }

        public void SendMessage(int partyCode, string message)
        {
            _partyManagerClient.NotifySendMessage(partyCode, message);
        }

        private void BtnExitLobby(object sender, RoutedEventArgs e)
        {
            MusicManager.PlayPlayerLeftSound();
            CloseParty(SessionDetails.PartyCode);
            var mainMenu = new MainMenu();
            Application.Current.MainWindow.Content = mainMenu;
            MainMenu mainMenu = new MainMenu();
            SessionDetails.IsHost = false;

            App.Current.MainWindow.Content = mainMenu;
        }

        public void CloseParty(int partyCode)
        {
            _partyManagerClient.NotifyCloseParty(partyCode);
        }

        private void UpdatePlayerList(Dictionary<string, object> playersInLobby)
        {
            playersPanel.Children.Clear();
            foreach (var player in playersInLobby)
            {
                CreatePlayerPanel(player.Key);
            }
        }

        private void CreatePlayerPanel(string username)
        {
            var backgroundColor = username == SessionDetails.Username
                ? new SolidColorBrush(Colors.Gold) 
                : new SolidColorBrush(Colors.DimGray);

            var stackPanel = new StackPanel
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

            if (username == SessionDetails.Username) return;
            
            var btnKick = new Button
            {
                Content = Properties.Resources.BtnKick,
                Margin = new Thickness(5, 0, 0, 0),
                VerticalAlignment = VerticalAlignment.Center,
                DataContext = username
            };
            btnKick.Click += KickPlayerEvent;
            stackPanel.Children.Add(btnKick);

            var btnViewProfile = new Button
            {
                Content = Properties.Resources.BtnProfile,
                Margin = new Thickness(5, 0, 0, 0),
                VerticalAlignment = VerticalAlignment.Center,
                DataContext = username
            };
            btnViewProfile.Click += ViewProfileEvent;

            if (!username.Contains("guest"))
            {
                stackPanel.Children.Add(btnViewProfile);
            }
        }

        private void ViewProfileEvent(object sender, RoutedEventArgs e)
        {
            string username = ((FrameworkElement)sender).DataContext as string;
            _popUpUserDetails.SetDataContext(username, false);
            _popUpUserDetails.Visibility = Visibility.Visible;
        }

        private void KickPlayerEvent(object sender, RoutedEventArgs e)
        {
            string username = ((FrameworkElement)sender).DataContext as string;
            _partyManagerClient.NotifyKickPlayer(SessionDetails.PartyCode, username);
        }

        private void BtnStartGame(object sender, RoutedEventArgs e)
        {
            _partyManagerClient.NotifyStartGame(SessionDetails.PartyCode);
        }

        public void MessageReceived(string messageSent)
        {
            var labelMessageReceived = new Label
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                Foreground = new SolidColorBrush(Colors.White),
                FontSize = 14,
                Content = messageSent
            };

            chatPanel.Children.Add(labelMessageReceived);
            chatScrollViewer.ScrollToEnd();
        }

        public void PlayerJoined(Dictionary<string, object> playersInLobby)
        {
            if (!_isWpfRunning) return;
            
            MusicManager.PlayPlayerJoinedSound();
            UpdatePlayerList(playersInLobby);
        }

        public void PlayerLeft(Dictionary<string, object> playersInLobby)
        {
            if (!_isWpfRunning) return;
            
            MusicManager.PlayPlayerLeftSound();
            UpdatePlayerList(playersInLobby);
        }

        private void BtnStartGame(object sender, RoutedEventArgs e)
        {
            InstanceContext instanceContext = new InstanceContext(this);
            PartyManagerClient client = new DataService.PartyManagerClient(instanceContext);
            
            _cardTable = new CardTable();
            InstanceContext tableContext = new InstanceContext(_cardTable);
            MatchManagerClient tableClient = new MatchManagerClient(tableContext);
            tableClient.Subscribe(SessionDetails.PartyCode, SessionDetails.Username);

            client.StartGame(SessionDetails.PartyCode);
        public void PartyCreated(Dictionary<string, object> playersInLobby)
        {
            if (_isWpfRunning)
            {
                UpdatePlayerList(playersInLobby);
            }
        }

        public void GameStarted()
        {
            _cardTable.LoadPlayers();
            _cardTable.UpdateTableCards();

            App.Current.MainWindow.Content = _cardTable;
            CardTable cardTable = new CardTable();
            Application.Current.MainWindow.Content = cardTable;
        }

        public void PlayerKicked()
        {
            if (!_isWpfRunning) return;
            
            var mainMenu = new MainMenu();
            Application.Current.MainWindow.Content = mainMenu;
        }
    }
}