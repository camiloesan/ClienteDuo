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
    public partial class CardTable : Window
    {
        GameMenu _gameMenu;
        Random _numberGenerator;
        static readonly List<string> _cardColors = new List<string>()
        {
            "#0000FF", //Blue
            "#FFFF00", //Yellow
            "#008000", //Green
            "#FF0000"  //Red
        };
        static readonly List<(string, int)> _cardNumbers = new List<(string, int)>()
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
            _numberGenerator = new Random();
            _gameMenu = new GameMenu();

            loadSettingsMenu();
            dealTableCards();
            dealPlayersCards();
        }

        void loadSettingsMenu()
        {
            _gameMenu.Margin = new Thickness(550, 0, 0, 0);
            _gameMenu.Visibility = Visibility.Collapsed;
            _background.Children.Add(_gameMenu);
        }

        void dealTableCards()
        {
            int leftRandomIndex = _numberGenerator.Next(1, 9);
            int rightRandomIndex = _numberGenerator.Next(1, 9);

            do
            {
                leftRandomIndex = _numberGenerator.Next(1, 9);
                _middleCardLabel.Content = leftRandomIndex.ToString();

                rightRandomIndex = _numberGenerator.Next(1, 9);
                _rightCardLabel.Content = rightRandomIndex.ToString();

            } while(leftRandomIndex == 2 || rightRandomIndex == 2 || leftRandomIndex == rightRandomIndex);

            string _middleColor = _cardColors[_numberGenerator.Next(0, 3)];
            string _rightColor = _cardColors[_numberGenerator.Next(0, 3)];

            _middleCardColor.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom(_middleColor));
            _rightCardColor.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom(_rightColor));
        }

        void dealPlayersCards()
        {
            for (int i = 0; i < 5; i++)
            {
                dealPlayerCard();
                dealCardLeft();
                dealCardRight();
                dealCardTop();
            }
        }

        void dealCardLeft()
        {
            FlippedCard card = new FlippedCard();

            card.RenderTransform = new RotateTransform(90);
            card.Margin = new Thickness(100, 0, 0, 0);


            card.Visibility = Visibility.Visible;
            _leftDeck.Children.Add(card);
        }

        void dealCardRight()
        {

        }

        void dealCardTop()
        {

        }

        void dealPlayerCard()
        {
            Card card = new Card();
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
            _playerDeck.Children.Add(card);
        }

        private void _gameMenuButton_Click(object sender, RoutedEventArgs e)
        {
            _gameMenu.Visibility = Visibility.Visible;
        }
    }
}
