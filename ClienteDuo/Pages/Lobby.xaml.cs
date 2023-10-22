﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Resources;
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

        private Dictionary<string, object> players = new Dictionary<string, object>();
        int partyCode = 0;

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
            partyCode = rand.Next(0, 10000);

            client.NewParty(partyCode, Login.ACTIVE_EMAIL);
            LblPartyCode.Content = "Code: " + partyCode;
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
            if (e.Key == Key.Return && TBoxMessage.Text.Trim().Length > 0)
            {
                InstanceContext instanceContext = new InstanceContext(this);
                DataService.PartyManagerClient client = new DataService.PartyManagerClient(instanceContext);

                string message = Login.ACTIVE_EMAIL + ": " + TBoxMessage.Text;
                TBoxMessage.Text = "";
                client.SendMessage(partyCode, message); //partycode no good
            }
            else if (TBoxMessage.Text.Length > MESSAGE_MAX_LENGTH)
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgMessageMaxCharacters);
            }
        }

        private void BtnExitLobby(object sender, RoutedEventArgs e)
        {
            MainMenu mainMenu = new MainMenu();
            App.Current.MainWindow.Content = mainMenu;

            InstanceContext instanceContext = new InstanceContext(this);
            DataService.PartyManagerClient client = new DataService.PartyManagerClient(instanceContext);
            client.LeaveParty(partyCode, Login.ACTIVE_EMAIL);
        }

        public void PlayerJoined(Dictionary<string, object> playersInLobby)
        {
            players = playersInLobby;
            //PlayPlayerJoinedAudio();
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

        private void PlayPlayerJoinedAudio()
        {

            MediaPlayer player = new MediaPlayer();

            //player.Open(new System.Uri("fullpathx"));
            player.Play();
        }

        public void PartyCreated(Dictionary<string, object> playersInLobby)
        {
            UpdatePlayerList(playersInLobby);
        }
    }
}
