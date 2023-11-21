using ClienteDuo.DataService;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.ServiceModel;
using System.Windows;

namespace ClienteDuo.Pages
{
    public partial class MainWindow : Window, IUserConnectionHandlerCallback
    {
        private static UserConnectionHandlerClient _userConnectionHandlerClient;

        public MainWindow()
        {
            InitializeComponent();
            _userConnectionHandlerClient = new DataService.UserConnectionHandlerClient(new InstanceContext(this));
            var launcher = new Launcher();
            ResizeMode = ResizeMode.NoResize;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Content = launcher;
        }

        public static void NotifyLogin(User user)
        {
            _userConnectionHandlerClient.NotifyLogIn(user);
        }

        public static void ShowMessageBox(string message)
        {
            string caption = Properties.Resources.TitleAlert;

            const MessageBoxButton okButton = MessageBoxButton.OK;
            const MessageBoxImage warningIcon = MessageBoxImage.Warning;
            MessageBox.Show(message, caption, okButton, warningIcon);
        }

        public void UserLogged(string username)
        {
            MainMenu.ShowPopUpFriendLogged(username);
        }

        public void UserLoggedOut(string username)
        {
            throw new NotImplementedException();
        }
    }
}
