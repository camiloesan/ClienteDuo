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
using Label = System.Windows.Controls.Label;

namespace ClienteDuo.Pages
{
    /// <summary>
    /// Interaction logic for CardTable.xaml
    /// </summary>
    public partial class CardTable : Window
    {
        DataService.Card[] _tableCards = new DataService.Card[3];
        Label[] _cardLabels = new Label[3];
        Rectangle[] _cardColors = new Rectangle[3];
        GameMenu _gameMenu;

        public CardTable()
        {
            InitializeComponent();

            _cardLabels[0] = _leftCardLabel;
            _cardLabels[1] = _middleCardLabel;
            _cardLabels[2] = _rightCardLabel;

            _cardColors[0] = _leftCardColor;
            _cardColors[1] = _middleCardColor;
            _cardColors[2] = _rightCardColor;

            _tableCards[0] = null;
            _tableCards[1] = new DataService.Card();
            _tableCards[2] = new DataService.Card();

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

        void UpdateTableCards()
        {
            DataService.MatchManagerClient client = new DataService.MatchManagerClient();

            client.DealTableCards(); //This is only called here for test purposes
            DataService.Card[] cards = client.GetTableCards();

            for (int i = 0; i < cards.Length; i++)
            {
                if (cards[i] != null)
                {
                    _cardLabels[i].Content = cards[i].Number;
                    _cardColors[i].Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom(cards[i].Color));
                }
            }

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

        void PlayCardLeft(object sender, RoutedEventArgs e)
        {

        }

        void PlayCardMiddle(object sender, RoutedEventArgs e)
        {

        }

        void PlayCardRight(object sender, RoutedEventArgs e)
        {

        }
    }
}
