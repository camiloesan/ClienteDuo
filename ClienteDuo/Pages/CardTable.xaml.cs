using ClienteDuo.DataService;
using ClienteDuo.Utilities;
using System.Collections.Generic;
using System.Runtime.Remoting;
using System.ServiceModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Label = System.Windows.Controls.Label;

namespace ClienteDuo.Pages
{
    public partial class CardTable : Page, DataService.IMatchManagerCallback
    {
        bool _hasDrawnCard;
        DataService.Card[] _tableCards = new DataService.Card[3];
        GameMenu _gameMenu;
        PlayerIcon[] _playerIcons = new PlayerIcon[3];
        List<Card> _selectedCards = new List<Card>();
        Label[] _cardLabels = new Label[3];
        Rectangle[] _cardColors = new Rectangle[3];
        int _matchingColors;

        public CardTable()
        {
            InitializeComponent();

            InitializeAttributes();
            LoadSettingsMenu();

            for (int i = 0; i < 5; i++)
            {
                DealPlayerCard();
            }

            _hasDrawnCard = false;
        }

        public bool SelectCard(Card selectedCard)
        {
            bool result = false;

            if (_selectedCards.Count < 2)
            {
                _selectedCards.Add(selectedCard);
                result = true;
            }

            return result;
        }

        public void UnselectCard(Card unselectedCard)
        {
            _selectedCards.Remove(unselectedCard);
        }

        public void PlayerLeft(string username)
        {
            if (username.Equals(SessionDetails.Username))
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

                MainWindow.ShowMessageBox("You have been kicked from the game");
            }
            else
            {
                foreach (PlayerIcon playerIcon in _playerIcons)
                {
                    //The '?' operator is not available in the C# version I used for this and I'm too lazy to update
                    if (playerIcon.Username != null)
                    {
                        if (playerIcon.Username.Equals(username))
                        {
                            playerIcon.Visibility = Visibility.Collapsed;
                        }
                    }
                }
            }
        }

        private void InitializeAttributes()
        {
            _cardLabels[0] = leftCardLabel;
            _cardLabels[1] = middleCardLabel;
            _cardLabels[2] = rightCardLabel;

            _cardColors[0] = leftCardColor;
            _cardColors[1] = middleCardColor;
            _cardColors[2] = rightCardColor;

            _tableCards[0] = new DataService.Card();
            _tableCards[1] = new DataService.Card();
            _tableCards[2] = new DataService.Card();

            _playerIcons[0] = topPlayerIcon;
            _playerIcons[1] = leftPlayerIcon;
            _playerIcons[2] = rightPlayerIcon;
        }

        private void LoadSettingsMenu()
        {
            _gameMenu = new GameMenu();

            _gameMenu.Margin = new Thickness(550, 0, 0, 0);
            _gameMenu.Visibility = Visibility.Collapsed;
            _background.Children.Add(_gameMenu);
        }

        public void LoadPlayers()
        {
            InstanceContext instanceContext = new InstanceContext(this);
            MatchManagerClient client = new MatchManagerClient(instanceContext);
            List<string> matchPlayers = new List<string>(client.GetPlayerList(SessionDetails.PartyCode));
            currentTurnLabel.Content = client.GetCurrentTurn(SessionDetails.PartyCode);

            List<string> otherPlayers = new List<string>();
            foreach (string player in matchPlayers)
            {
                if (!player.Equals(SessionDetails.Username))
                {
                    otherPlayers.Add(player);
                }
            }

            for (int i = 0; i < otherPlayers.Count; i++)
            {
                _playerIcons[i].Username = otherPlayers[i];
                _playerIcons[i].Visibility = Visibility.Visible;
            }

            _gameMenu.LoadPlayers(otherPlayers);
        }

        public void UpdateTableCards()
        {
            CardManagerClient client = new CardManagerClient();
            _tableCards = client.GetCards(SessionDetails.PartyCode);

            for (int i = 0; i < _tableCards.Length; i++)
            {
                if (_tableCards[i].Number != "")
                {
                    _cardLabels[i].Content = _tableCards[i].Number;
                    _cardColors[i].Fill = (SolidColorBrush) new BrushConverter().ConvertFrom(_tableCards[i].Color);
                }
                else
                {
                    _cardLabels[i].Content = "";
                    _cardColors[i].Fill = (SolidColorBrush) new BrushConverter().ConvertFrom("#FF969696");
                }
            }
        }

        public void TurnFinished(string currentTurn)
        {
            currentTurnLabel.Content = currentTurn;

            UpdateTableCards();
        }

        public void GameOver()
        {
            GameOver gameOverScreen = new GameOver();
            InstanceContext context = new InstanceContext(this);
            MatchManagerClient client = new MatchManagerClient(context);
            client.SetGameScore(SessionDetails.PartyCode, SessionDetails.Username, playerDeck.Children.Count);

            Thread.Sleep(3000);
            gameOverScreen.LoadPlayers(client.GetMatchResults(SessionDetails.PartyCode));

            gameOverScreen.Visibility = Visibility.Visible;
        }

        private void DealPlayerCard()
        {
            Card card = new Card();
            CardManagerClient client = new CardManagerClient();
            DataService.Card dealtCard = client.DrawCard();
            _hasDrawnCard = true;

            card.Number = dealtCard.Number;
            card.Color = dealtCard.Color;
            card.Visibility = Visibility.Visible;
            card.GameTable = this;

            playerDeck.Children.Add(card);
        }

        private void _gameMenuButton_Click(object sender, RoutedEventArgs e)
        {
            _gameMenu.Visibility = Visibility.Visible;
        }

        private void Deck_Click(object sender, RoutedEventArgs e)
        {
            if (currentTurnLabel.Content.Equals(SessionDetails.Username))
            {
                DealPlayerCard();
            }
        }

        private bool isValidMove(int position)
        {
            bool result = false;
            int selectionSum = 0;

            if (currentTurnLabel.Content.Equals(SessionDetails.Username))
            {
                for (int i = 0; i < _selectedCards.Count; i++)
                {
                    if (_selectedCards[i].Color.Equals("#000000") || _selectedCards[i].Color.Equals(_tableCards[position].Color))
                    {
                        _matchingColors++;
                    }
                    else
                    {
                        _matchingColors = -1;
                    }

                    if (_selectedCards[i].Number.Equals("#") && selectionSum < int.Parse(_tableCards[position].Number))
                    {
                        result = true;
                    }
                    else
                    {
                        selectionSum += int.Parse(_selectedCards[i].Number);
                    }
                }

                if (_tableCards[position].Number == selectionSum.ToString())
                {
                    result = true;
                }
            }

            return result;
        }

        private void PlayCard(int position)
        {
            CardManagerClient client = new CardManagerClient();
            DataService.Card playedCard = new DataService.Card();
            playedCard.Number = _selectedCards[0].Number;
            playedCard.Color = _selectedCards[0].Color;

            client.PlayCard(SessionDetails.PartyCode, position, playedCard);

            for (int i = 0; i < _selectedCards.Count; i++)
            {
                _cardLabels[position].Content = _selectedCards[i].Number;
                _cardColors[position].Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom(_selectedCards[i].Color));
                    
                playerDeck.Children.Remove(_selectedCards[i]);
            }

            _selectedCards.Clear();

            if (playerDeck.Children.Count == 0)
            {
                InstanceContext context = new InstanceContext(this);
                MatchManagerClient matchClient = new MatchManagerClient(context);

                matchClient.EndGame(SessionDetails.PartyCode);
            }
            else
            {
                endTurnButton.Visibility = Visibility.Visible;
            }
        }

        private void PlayCardLeft(object sender, RoutedEventArgs e)
        {
            if (isValidMove(0)) 
            {
                PlayCard(0);
            }
        }

        private void PlayCardMiddle(object sender, RoutedEventArgs e)
        {
            if (isValidMove(1))
            {
                PlayCard(1);
            }
        }

        private void PlayCardRight(object sender, RoutedEventArgs e)
        {
            if (_tableCards[2].Number.Equals("") && _selectedCards.Count == 1)
            {
                if (_matchingColors >= 1 || _hasDrawnCard)
                {
                    PlayCard(2);

                    //*Whistling in a non-suspicious manner*
                    _matchingColors = -9000;
                    _hasDrawnCard = false;
                }
            }
            else
            {
                if (isValidMove(2))
                {
                    PlayCard(2);
                }
            }
        }

        private void EndTurnButton_Click(object sender, RoutedEventArgs e)
        {
            InstanceContext instanceContext = new InstanceContext(this);
            MatchManagerClient client = new MatchManagerClient(instanceContext);
            endTurnButton.Visibility = Visibility.Collapsed;
            _matchingColors = 0;

            CardManagerClient cardClient = new CardManagerClient();
            cardClient.DealCards(SessionDetails.PartyCode);

            client.EndTurn(SessionDetails.PartyCode);
        }
    }
}
