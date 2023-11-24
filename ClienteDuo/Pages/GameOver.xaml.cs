using ClienteDuo.DataService;
using ClienteDuo.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClienteDuo.Pages
{
    public partial class GameOver : UserControl
    {
        public GameOver()
        {
            InitializeComponent();
        }

        public void LoadPlayers(Dictionary<string, int> playerScores)
        {
            foreach (KeyValuePair<string, int> playerScore in playerScores)
            {
                PlayerBar playerBar = new PlayerBar();

                if (playerScore.Value == 0) 
                {
                    playerBar.Username = playerScore.Key + "( " + Properties.Resources.LblWinner + ")"; //TODO Replace "Winner" with translation
                }
                else
                {
                    playerBar.Username = playerScore.Key + "(" + playerScore.Value.ToString() + " " + "Properties.Resources.LblCardsLeft" + ")"; //TODO Replace "cards left" with its translation
                }

                if (playerScore.Key.Equals(SessionDetails.Username))
                {
                    playerBar.Background = new SolidColorBrush(Colors.Gold);
                }

                playerBar.Visibility = Visibility.Visible;

                resultStackPanel.Children.Add(playerBar);
            }
        }

        private void BtnGoToLobby(object sender, RoutedEventArgs e)
        {   

        }
    }
}
