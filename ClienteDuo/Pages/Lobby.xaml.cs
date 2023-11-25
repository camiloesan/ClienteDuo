using ClienteDuo.DataService;
using ClienteDuo.Pages.Sidebars;
using ClienteDuo.Utilities;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
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
        private readonly PartyValidatorClient _partyValidatorClient = new PartyValidatorClient();
        private PopUpUserDetails _popUpUserDetails;

        public Lobby(string username)
        {
            InitializeComponent();
            SessionDetails.IsHost = true;
            var instanceContext = new InstanceContext(this);
            _partyManagerClient = new PartyManagerClient(instanceContext);
            
            CreateNewParty(username);
            LoadVisualComponents();
            MusicManager.PlayPlayerJoinedSound();
        }

        public Lobby(string username, int partyCode)
        {
            InitializeComponent();
            SessionDetails.IsHost = false;
            var instanceContext = new InstanceContext(this);
            _partyManagerClient = new PartyManagerClient(instanceContext);
            
            JoinGame(partyCode, username);
            LoadVisualComponents();
            MusicManager.PlayPlayerJoinedSound();
        }

        public Lobby() //test-only constructor
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
            SessionDetails.PartyCode = GenerateNewPartyCode();
            SessionDetails.Username = hostUsername;
            
            _partyManagerClient.NotifyCreateParty(SessionDetails.PartyCode, SessionDetails.Username);

            return SessionDetails.PartyCode;
        }

        private int GenerateNewPartyCode()
        {
            var random = new Random();
            var randomCode = random.Next(1000, 10000);
            if (_partyValidatorClient.IsPartyExistent(randomCode))
            {
                GenerateNewPartyCode();
            }

            return randomCode;
        }

        public void JoinGame(int partyCode, string username)
        {
            _partyManagerClient.NotifyJoinParty(partyCode, username);
        }
        
        private void LoadVisualComponents()
        {
            LblPartyCode.Content = Properties.Resources.LblPartyCode + ": " + SessionDetails.PartyCode;

            if (!SessionDetails.IsHost)
            {
                BtnStartGame.Visibility = Visibility.Collapsed;
            }
            
            _popUpUserDetails = new PopUpUserDetails
            {
                Width = 350,
                Height = 200,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Visibility = Visibility.Collapsed
            };
            MasterGrid.Children.Add(_popUpUserDetails);
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

        private void BtnExitLobbyEvent(object sender, RoutedEventArgs e)
        {
            _partyManagerClient.NotifyLeaveParty(SessionDetails.PartyCode, SessionDetails.Username);
            if (SessionDetails.IsHost)
            {
                CloseParty(SessionDetails.PartyCode);
            }
            
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
            MusicManager.PlayPlayerLeftSound();
        }

        public void CloseParty(int partyCode)
        {
            _partyManagerClient.NotifyCloseParty(partyCode);
        }

        private void UpdatePlayerList(Dictionary<string, object> playersInLobby)
        {
            PanelPlayers.Children.Clear();
            foreach (var player in playersInLobby)
            {
                CreatePlayerPanel(player.Key);
            }
        }

        private void CreatePlayerPanel(string playerUsername)
        {
            var backgroundColor = playerUsername == SessionDetails.Username
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
            PanelPlayers.Children.Add(stackPanel);

            var usernameName = new Label
            {
                Foreground = new SolidColorBrush(Colors.Black),
                Content = playerUsername,
                Margin = new Thickness(10, 0, 5, 0),
                VerticalAlignment = VerticalAlignment.Center
            };
            stackPanel.Children.Add(usernameName);

            if (playerUsername == SessionDetails.Username) return;
            
            var btnKick = new Button
            {
                Content = Properties.Resources.BtnKick,
                Margin = new Thickness(5, 0, 0, 0),
                VerticalAlignment = VerticalAlignment.Center,
                DataContext = playerUsername
            };
            btnKick.Click += KickPlayerEvent;

            var btnViewProfile = new Button
            {
                Content = Properties.Resources.BtnProfile,
                Margin = new Thickness(5, 0, 0, 0),
                VerticalAlignment = VerticalAlignment.Center,
                DataContext = playerUsername
            };
            btnViewProfile.Click += ViewProfileEvent;

            if (SessionDetails.IsHost)
            {
                stackPanel.Children.Add(btnKick);
            }
            
            if (!playerUsername.Contains("guest"))
            {
                stackPanel.Children.Add(btnViewProfile);
            }
        }

        private void ViewProfileEvent(object sender, RoutedEventArgs e)
        {
            string username = ((FrameworkElement)sender).DataContext as string;
            _popUpUserDetails.InitializeUserInfo(username, false);
            _popUpUserDetails.Visibility = Visibility.Visible;
        }

        private void KickPlayerEvent(object sender, RoutedEventArgs e)
        {
            string username = ((FrameworkElement)sender).DataContext as string;
            _partyManagerClient.NotifyKickPlayer(SessionDetails.PartyCode, username);
        }

        private void StartGameEvent(object sender, RoutedEventArgs e)
        {
            var instanceContext = new InstanceContext(this);
            var client = new PartyManagerClient(instanceContext);

            client.NotifyStartGame(SessionDetails.PartyCode);
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

            PanelChat.Children.Add(labelMessageReceived);
            ScrollViewerChat.ScrollToEnd();
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

        public void PartyCreated(Dictionary<string, object> playersInLobby)
        {
            if (_isWpfRunning)
            {
                UpdatePlayerList(playersInLobby);
            }
        }

        public async void GameStarted()
        {
            CardTable cardTable = new CardTable();
            InstanceContext tableContext = new InstanceContext(cardTable);
            MatchManagerClient tableClient = new MatchManagerClient(tableContext);
            tableClient.Subscribe(SessionDetails.PartyCode, SessionDetails.Username);

            await Task.Delay(5000);
            cardTable.LoadPlayers();
            cardTable.UpdateTableCards();
            Application.Current.MainWindow.Content = cardTable;
        }

        public void PlayerKicked()
        {
            if (!_isWpfRunning) return;
            
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