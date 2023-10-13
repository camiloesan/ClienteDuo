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
    /// <summary>
    /// Interaction logic for Lobby.xaml
    /// </summary>
    public partial class Lobby : Page, DataService.IMessageServiceCallback
    {
        public Lobby()
        {
            InitializeComponent();
            Console.WriteLine(this.GetHashCode());
            //try connection and register user in hashmap
        }

        public void MessageReceived(string messageSent)
        {
            TBlockChat.Text = messageSent;
            // GUARDAR EN UN HASHMAP LOS CONTEXTOS (o lista) Y DESPUES RECORRERLOS PARA MANDAR EL MESSAGE RECEIVED
        }
        
        private void SendMessage(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                InstanceContext instanceContext = new InstanceContext(this);
                DataService.MessageServiceClient client = new DataService.MessageServiceClient(instanceContext); //instance, address???
                string message = TBoxMessage.Text;
                client.SendMessage(message);
            }
        }

        private void BtnExitLobby(object sender, RoutedEventArgs e)
        {
            MainMenu mainMenu = new MainMenu();
            App.Current.MainWindow.Content = mainMenu;
            //disconnect
        }
    }
}
