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
        static List<string> _cardColors = new List<string>()
        {
            "#0000FF", //Blue
            "#FFFF00", //Yellow
            "#008000", //Green
            "#FF0000"  //Red
        };

        static List<(string, int)> _cardNumbers = new List<(string, int)>()
        {
            ("1", 12),
            ("2", 12),
            ("3", 12),
            ("4", 12),
            ("5", 12),
            ("6", 8),
            ("7", 8),
            ("8", 8),
            ("9", 8),
            ("10", 8),
            ("#", 8)
        };

        public CardTable()
        {
            InitializeComponent();

            loadSettingsMenu();
            dealTableCards();
            dealPlayerCards();
            dealOtherPlayersCards();
        }

        void loadSettingsMenu()
        {
            GameMenu gameMenu = new GameMenu();

            _gameMenu.Children.Add(gameMenu);
        }

        void dealTableCards()
        {
            // Assign card values
            Random numberGenerator = new Random();

            int leftRandomIndex = numberGenerator.Next(1, 9);
            int rightRandomIndex = numberGenerator.Next(1, 9);

            do
            {
                leftRandomIndex = numberGenerator.Next(1, 9);
                _middleCardLabel.Content = leftRandomIndex.ToString();

                rightRandomIndex = numberGenerator.Next(1, 9);
                _rightCardLabel.Content = rightRandomIndex.ToString();

            } while(leftRandomIndex == 2 || rightRandomIndex == 2 || leftRandomIndex == rightRandomIndex);

            //Assign colors
            Random colorGenerator = new Random();

            string _middleColor = _cardColors[colorGenerator.Next(1, 4)];
            string _rightColor = _cardColors[colorGenerator.Next(1, 4)];

            _middleCardColor.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom(_middleColor));
            _rightCardColor.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom(_rightColor));
        }

        public void dealCardLeft()
        {

        }

        public void dealCardRight()
        {

        }

        public void dealCardTop()
        {

        }

        void dealOtherPlayersCards()
        {
            for (int i = 0; i < 5; i++)
            {
                dealCardLeft();
                dealCardRight();
                dealCardTop();
            }
        }

        void dealPlayerCards()
        {
            Card[] deck = new Card[5];
            Random numberGenerator = new Random();
            
            for (int i = 0; i < deck.Length; i++)
            {
                deck[i] = new Card();
                int _accumulatedWeight = 0;
                int _cardNumber = numberGenerator.Next(0, 108); //108 is the total of cards in a standard DUO deck

                foreach (var (number, weight) in _cardNumbers)
                {
                    _accumulatedWeight += weight;

                    if (_accumulatedWeight <= _cardNumber)
                    {
                        deck[i].Number = number;
                    }
                }
            }

            Random colorGenerator = new Random();

            for (int i = 0; i < deck.Length; i++)
            {
                if (deck[i].Number.CompareTo("2") != 0)
                {
                    deck[i].Color = _cardColors[colorGenerator.Next(0, 4)];
                }

                deck[i].Visibility = Visibility.Visible;
                _playerDeck.Children.Add(deck[i]);
            }
        }

        private void _gameMenuButton_Click(object sender, RoutedEventArgs e)
        {
            _gameMenu.Visibility = Visibility.Visible;

            _showGameMenuButton.Visibility = Visibility.Collapsed;
        }
    }
}
