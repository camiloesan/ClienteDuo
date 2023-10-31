using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using Label = System.Windows.Controls.Label;

namespace ClienteDuo.Pages
{
    public partial class CardTable : Window
    {
        DataService.Card[] _tableCards = new DataService.Card[3];
        GameMenu _gameMenu;
        List<Card> _selectedCards = new List<Card>();
        Label[] _cardLabels = new Label[3];
        Rectangle[] _cardColors = new Rectangle[3];
        int _matchingColors;

        public CardTable()
        {
            InitializeComponent();

            InitializeAttributes();
            LoadSettingsMenu();

            DataService.MatchManagerClient client = new DataService.MatchManagerClient();
            client.InitializeData();
            _tableCards = client.GetTableCards();
            client.Close();

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
        }

        void LoadSettingsMenu()
        {
            _gameMenu = new GameMenu();

            _gameMenu.Margin = new Thickness(550, 0, 0, 0);
            _gameMenu.Visibility = Visibility.Collapsed;
            _background.Children.Add(_gameMenu);
        }

        void UpdateTableCards()
        {
            DataService.MatchManagerClient client = new DataService.MatchManagerClient();
            client.DealTableCards(); //This is only called here for test purposes
            _tableCards = client.GetTableCards();

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
            client.Close();
        }

        void DealPlayerCard()
        {
            Card card = new Card();
            DataService.MatchManagerClient client = new DataService.MatchManagerClient();
            DataService.Card dealtCard = client.DrawCard();

            card.Number = dealtCard.Number;
            card.Color = dealtCard.Color;
            card.Visibility = Visibility.Visible;
            card.GameTable = this;

            _playerDeck.Children.Add(card);
            client.Close();
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

            for (int i = 0; i < _selectedCards.Count; i++)
            {
                if (_selectedCards[i].Color.Equals("#000000") || _selectedCards[i].Color.Equals(_tableCards[position].Color))
                {
                    _matchingColors++;
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

            return result;
        }

        void PlayCard(int position)
        {
            DataService.MatchManagerClient client = new DataService.MatchManagerClient();
            client.PlayCard(position);

            for (int i = 0; i < _selectedCards.Count; i++)
            {
                    _tableCards[position].Number = "";
                _cardLabels[position].Content = _selectedCards[i].Number;
                _cardColors[position].Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom(_selectedCards[i].Color));
                    
                    

                _playerDeck.Children.Remove(_selectedCards[i]);
            }

            _selectedCards.Clear();
            
            //UpdateTableCards(); //This must be run after ending the player's turn
            client.Close();
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
    }
}
