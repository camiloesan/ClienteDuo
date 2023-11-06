using ClienteDuo.Utilities;
using System.Collections.Generic;
using System.Runtime.Remoting;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Label = System.Windows.Controls.Label;

namespace ClienteDuo.Pages
{
    public partial class CardTable : Page, DataService.IMatchManagerCallback
    {
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
            UpdateTableCards();

            for (int i = 0; i < 5; i++)
            {
                DealPlayerCard();
            }
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

        void InitializeAttributes()
        {
            _cardLabels[0] = _leftCardLabel;
            _cardLabels[1] = _middleCardLabel;
            _cardLabels[2] = _rightCardLabel;

            _cardColors[0] = _leftCardColor;
            _cardColors[1] = _middleCardColor;
            _cardColors[2] = _rightCardColor;

            _tableCards[0] = new DataService.Card();
            _tableCards[1] = new DataService.Card();
            _tableCards[2] = new DataService.Card();

            _playerIcons[0] = _topPlayerIcon;
            _playerIcons[1] = _leftPlayerIcon;
            _playerIcons[2] = _rightPlayerIcon;
        }

        void LoadSettingsMenu()
        {
            _gameMenu = new GameMenu();

            _gameMenu.Margin = new Thickness(550, 0, 0, 0);
            _gameMenu.Visibility = Visibility.Collapsed;
            _background.Children.Add(_gameMenu);
        }

        public void LoadPlayers()
        {
            InstanceContext instanceContext = new InstanceContext(this);
            DataService.MatchManagerClient client = new DataService.MatchManagerClient(instanceContext);
            Dictionary <string, int> playerScores = client.GetPlayerScores(SessionDetails.PartyCode);
            List<string> players = new List<string>();
            List<int> scores = new List<int>();

            foreach (KeyValuePair<string, int> entry in playerScores)
            {
                if (entry.Key != SessionDetails.Username)
                {
                    players.Add(entry.Key);
                    scores.Add(entry.Value);
                }
            }

            for (int i = 0; i < _playerIcons.Length; i++)
            {
                if (i < playerScores.Count)
                {
                    _playerIcons[i]._nameLabel.Content = players[i];
                    _playerIcons[i]._scoreLabel.Content = scores[i] + " points";
                }
                else
                {
                    _playerIcons[i].Visibility = Visibility.Collapsed;
                }
            }
        }

        public void UpdateTableCards()
        {
            DataService.CardManagerClient client = new DataService.CardManagerClient();
            client.DealCards(SessionDetails.PartyCode); //This is only called here for test purposes
            _tableCards = client.GetCards(SessionDetails.PartyCode);

            for (int i = 0; i < _tableCards.Length; i++)
            {
                if (_tableCards[i].Number != "")
                {
                    _cardLabels[i].Content = _tableCards[i].Number;
                    _cardColors[i].Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom(_tableCards[i].Color));
                }
                else
                {
                    _cardLabels[i].Content = "";
                    _cardColors[i].Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF969696"));
                }
            }

            _matchingColors = 0;
        }

        public void TurnFinished(string currentTurn)
        {
            _currentTurnLabel.Content = currentTurn;
        }

        public void RoundOver()
        {
            throw new System.NotImplementedException();
        }

        public void GameOver()
        {
            throw new System.NotImplementedException();
        }

        void DealPlayerCard()
        {
            Card card = new Card();
            DataService.CardManagerClient client = new DataService.CardManagerClient();
            DataService.Card dealtCard = client.DrawCard();

            card.Number = dealtCard.Number;
            card.Color = dealtCard.Color;
            card.Visibility = Visibility.Visible;
            card.GameTable = this;

            _playerDeck.Children.Add(card);
        }

        void _gameMenuButton_Click(object sender, RoutedEventArgs e)
        {
            _gameMenu.Visibility = Visibility.Visible;
        }

        void Deck_Click(object sender, RoutedEventArgs e)
        {
            DealPlayerCard();
        }

        bool isValidMove(int position)
        {
            bool result = false;
            int selectionSum = 0;

            if (_currentTurnLabel.Content.Equals(SessionDetails.Username))
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

                    if (_selectedCards[i].Number.Equals("#"))
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

        void PlayCard(int position)
        {
            DataService.CardManagerClient client = new DataService.CardManagerClient();
            client.PlayCard(SessionDetails.PartyCode, position);

            for (int i = 0; i < _selectedCards.Count; i++)
            {
                    _tableCards[position].Number = "";
                _cardLabels[position].Content = _selectedCards[i].Number;
                _cardColors[position].Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom(_selectedCards[i].Color));
                    
                _playerDeck.Children.Remove(_selectedCards[i]);
            }

            _selectedCards.Clear();
        }

        void PlayCardLeft(object sender, RoutedEventArgs e)
        {
            if (isValidMove(0)) 
            {
                PlayCard(0);
            }
        }

        void PlayCardMiddle(object sender, RoutedEventArgs e)
        {
            if (isValidMove(1))
            {
                PlayCard(1);
            }
        }

        void PlayCardRight(object sender, RoutedEventArgs e)
        {
            if (_tableCards[2].Number.Equals(""))
            {
                if (_matchingColors >= 1)
                {
                    PlayCard(2);
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

        void EndTurnButton_Click(object sender, RoutedEventArgs e)
        {
            InstanceContext instanceContext = new InstanceContext(this);
            DataService.MatchManagerClient client = new DataService.MatchManagerClient(instanceContext);

            client.EndTurn(SessionDetails.PartyCode);
        }
    }
}
