using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ClienteDuo.Pages
{
    /// <summary>
    /// Interaction logic for CardTable.xaml
    /// </summary>
    public partial class CardTable : Page
    {
        Card[] _tableCards = new Card[3];
        GameMenu _gameMenu;

        public CardTable()
        {
            InitializeComponent();

            //The middle and right cards are instantiated first because of the rules
            _tableCards[0] = new Card();
            _tableCards[1] = new Card();
            _tableCards[2] = new Card();

            LoadSettingsMenu();
            UpdateTableCards();

            for (int i = 0; i < 5; i++)
            {
                DealPlayerCard();
            }
        }

        void LoadSettingsMenu()
        {
            _gameMenu = new GameMenu();

            _gameMenu.Margin = new Thickness(550, 0, 0, 0);
            _gameMenu.Visibility = Visibility.Collapsed;
            _background.Children.Add(_gameMenu);
        }

        void DealTableCards()
        {
            //DataService.MatchManagerClient client = new DataService.MatchManagerClient();

            //client.DealTableCards();
            //DataService.Card[] cards = client.GetTableCards();

            
        }



        void UpdateTableCards()
        {
            //DataService.MatchManagerClient client = new DataService.MatchManagerClient();

            //client.DealTableCards();
            //DataService.Card[] cards = client.GetTableCards();

            //for (int i = 0; i < cards.Length; i++)
            //{
                //If the table card is not black, it means there
                //if (cards[i] != null)
                //{
                    //_tableCards[]
                    
                    //_middleCardLabel.Content = cards[i].Number;
                    //_middleCardColor.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom(cards[i].Color));
                //}
            //}

            //client.Close();
        }

        void DealPlayerCard()
        {
            /*Card card = new Card();
            int _accumulatedWeight = 0;
            int _cardNumber = _numberGenerator.Next(0, 108); //108 is the total of cards in a standard DUO deck

            foreach (var (number, weight) in _cardNumbers)
            {
                _accumulatedWeight += weight;

                if (_accumulatedWeight <= _cardNumber)
                {
                    card.Number = number;
                }
            }

            if (card.Number.CompareTo("2") != 0)
            {
                card.Color = _cardColors[_numberGenerator.Next(0, 3)];
            }

            card.Visibility = Visibility.Visible;
            _playerDeck.Children.Add(card);*/
        }

        private void _gameMenuButton_Click(object sender, RoutedEventArgs e)
        {
            _gameMenu.Visibility = Visibility.Visible;
        }
    }
}
