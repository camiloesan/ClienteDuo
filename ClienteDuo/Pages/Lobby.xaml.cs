﻿using ClienteDuo.DataService;
using ClienteDuo.Pages.Sidebars;
using ClienteDuo.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
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
        const int MESSAGE_MAX_LENGTH = 64;
        private readonly bool _isWpfRunning = true;
        private readonly PartyManagerClient _partyManagerClient;
        private readonly PartyValidatorClient _partyValidatorClient = new PartyValidatorClient();
        private PopUpUserDetails _popUpUserDetails;

        public Lobby(string username)
        {
            InitializeComponent();
            SessionDetails.IsHost = true;
            InstanceContext instanceContext = new InstanceContext(this);
            _partyManagerClient = new PartyManagerClient(instanceContext);
            
            CreateNewParty(username);
            LoadVisualComponents();
            MusicManager.PlayPlayerJoinedSound();
        }

        public Lobby(string username, int partyCode)
        {
            InitializeComponent();
            SessionDetails.IsHost = false;
            InstanceContext instanceContext = new InstanceContext(this);
            _partyManagerClient = new PartyManagerClient(instanceContext);
            
            JoinGame(partyCode, username);
            LoadVisualComponents();
            MusicManager.PlayPlayerJoinedSound();
        }

        public Lobby() //test-only constructor
        {
            InstanceContext instanceContext = new InstanceContext(this);
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
            
            try
            {
                _partyManagerClient.NotifyCreateParty(SessionDetails.PartyCode, SessionDetails.Username);
            } catch (CommunicationException)
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgServiceException, MessageBoxImage.Error);
            }

            return SessionDetails.PartyCode;
        }

        private int GenerateNewPartyCode()
        {
            Random random = new Random();
            int randomCode = random.Next(1000, 10000);
            
            bool IsPartyExistent = false;
            try
            {
                IsPartyExistent = _partyValidatorClient.IsPartyExistent(randomCode);
            } 
            catch (CommunicationException)
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgServiceException, MessageBoxImage.Error);
            }

            if (IsPartyExistent)
            {
                GenerateNewPartyCode();
            }

            return randomCode;
        }

        public void JoinGame(int partyCode, string username)
        {
            try
            {
                _partyManagerClient.NotifyJoinParty(partyCode, username);
            }
            catch (CommunicationException)
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgServiceException, MessageBoxImage.Error);
            }
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
                Height = 240,
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

                try
                {
                    SendMessage(SessionDetails.PartyCode, message);
                }
                catch (CommunicationException)
                {
                    MainWindow.ShowMessageBox(Properties.Resources.DlgServiceException, MessageBoxImage.Error);
                }

                TBoxMessage.Text = "";
            }
            else if (TBoxMessage.Text.Length > MESSAGE_MAX_LENGTH)
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgMessageMaxCharacters, MessageBoxImage.Warning);
            }
        }

        public void SendMessage(int partyCode, string message)
        {
            try
            {
                _partyManagerClient.NotifySendMessage(partyCode, message);
            }
            catch (CommunicationException) 
            { 
                MainWindow.ShowMessageBox(Properties.Resources.DlgServiceException, MessageBoxImage.Error); 
            }
        }

        private void BtnExitLobbyEvent(object sender, RoutedEventArgs e)
        {
            try
            {
                _partyManagerClient.NotifyLeaveParty(SessionDetails.PartyCode, SessionDetails.Username);
            }
            catch
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgServiceException, MessageBoxImage.Error);
            }

            if (SessionDetails.IsHost)
            {
                CloseParty(SessionDetails.PartyCode);
                SessionDetails.IsHost = false;
                SessionDetails.PartyCode = 0;
            }
            
            if (SessionDetails.IsGuest)
            {
                SessionDetails.PartyCode = 0;
                Launcher launcher = new Launcher();
                Application.Current.MainWindow.Content = launcher;
            }
            else
            {
                MainMenu mainMenu = new MainMenu();
                Application.Current.MainWindow.Content = mainMenu;
            }

            MusicManager.PlayPlayerLeftSound();
        }

        public void CloseParty(int partyCode)
        {
            try
            {
                _partyManagerClient.NotifyCloseParty(partyCode, Properties.Resources.DlgHostHasClosedTheParty);
            }
            catch (CommunicationException)
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgServiceException, MessageBoxImage.Error);
            }
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
            SolidColorBrush backgroundColor = 
                playerUsername == SessionDetails.Username
                ? new SolidColorBrush(Colors.Gold) 
                : new SolidColorBrush(Colors.DimGray);

            StackPanel stackPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Center,
                Background = backgroundColor,
                Margin = new Thickness(15, 20, 15, 20),
                Width = 200,
                Height = 40,
            };
            PanelPlayers.Children.Add(stackPanel);

            Label usernameName = new Label
            {
                Foreground = new SolidColorBrush(Colors.Black),
                Content = playerUsername,
                Margin = new Thickness(10, 0, 5, 0),
                VerticalAlignment = VerticalAlignment.Center
            };
            stackPanel.Children.Add(usernameName);

            if (playerUsername != SessionDetails.Username)
            {
                Button btnKick = new Button
                {
                    Content = Properties.Resources.BtnKick,
                    Margin = new Thickness(5, 0, 0, 0),
                    VerticalAlignment = VerticalAlignment.Center,
                    DataContext = playerUsername
                };
                btnKick.Click += KickPlayerEvent;

                Button btnViewProfile = new Button
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
            PopUpKickPlayer popUpKickPlayer = new PopUpKickPlayer();
            popUpKickPlayer.KickedUsername = username;
            popUpKickPlayer.SetClient(_partyManagerClient);
            popUpKickPlayer.Show();
        }

        private void StartGameEvent(object sender, RoutedEventArgs e)
        {
            PartyValidatorClient partyValidatorClient = new PartyValidatorClient();

            if (partyValidatorClient.GetPlayersInParty(SessionDetails.PartyCode).Length > 1)
            {
                PartyManagerClient partyManagerClient = new PartyManagerClient(new InstanceContext(this));

                try
                {
                    partyManagerClient.NotifyStartGame(SessionDetails.PartyCode);
                }
                catch (CommunicationException)
                {
                    Launcher launcher = new Launcher();
                    Application.Current.MainWindow.Content = launcher;
                    MainWindow.ShowMessageBox(Properties.Resources.DlgServiceException, MessageBoxImage.Error);
                }
            }
            else
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgNotEnoughPlayers, MessageBoxImage.Exclamation);
            }
        }

        public void MessageReceived(string messageSent)
        {
            TextBlock textBlock = new TextBlock
            {
                Text = messageSent,
                Margin = new Thickness(0,4,0,0),
                HorizontalAlignment = HorizontalAlignment.Left,
                Foreground = new SolidColorBrush(Colors.White),
                Width = 248,
                FontSize = 14,
                TextWrapping = TextWrapping.Wrap
            };

            PanelChat.Children.Add(textBlock);
            ScrollViewerChat.ScrollToEnd();
        }

        public void PlayerJoined(Dictionary<string, object> playersInLobby)
        {
            if (_isWpfRunning)
            {
                MusicManager.PlayPlayerJoinedSound();
                UpdatePlayerList(playersInLobby);
            }
        }

        public void PlayerLeft(Dictionary<string, object> playersInLobby)
        {
            if (_isWpfRunning)
            {
                MusicManager.PlayPlayerLeftSound();
                UpdatePlayerList(playersInLobby);
            }
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
            LblLoading.Visibility = Visibility.Visible;

            await Task.Delay(5000);
            cardTable.LoadPlayers();
            cardTable.UpdateTableCards();
            Application.Current.MainWindow.Content = cardTable;
        }

        public void PlayerKicked(string reason)
        {
            if (_isWpfRunning)
            {
                if (SessionDetails.IsGuest)
                {
                    Launcher launcher = new Launcher();
                    Application.Current.MainWindow.Content = launcher;
                }
                else
                {
                    MainMenu mainMenu = new MainMenu();
                    Application.Current.MainWindow.Content = mainMenu;
                }
                string message = Properties.Resources.DlgKickedPlayer + Properties.Resources.DlgKickingReason + reason;
                MainWindow.ShowMessageBox(message, MessageBoxImage.Exclamation);
            }
        }
    }
}