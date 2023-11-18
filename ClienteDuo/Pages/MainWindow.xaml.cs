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
        static InstanceContext _instanceContext;
        static UserConnectionHandlerClient _userConnectionHandlerClient;

        public MainWindow()
        {
            InitializeComponent();
            _instanceContext = new InstanceContext(this);
            _userConnectionHandlerClient = new UserConnectionHandlerClient(_instanceContext);
            Launcher launcher = new Launcher();
            this.ResizeMode = ResizeMode.NoResize;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Content = launcher;
        }

        public static void NotifyLogin(User user)
        {
            _userConnectionHandlerClient.NotifyLogIn(user);
        }

        public static void ShowMessageBox(string message)
        {
            string messageBoxText = message;
            string caption = Properties.Resources.TitleAlert;

            MessageBoxButton okButton = MessageBoxButton.OK;
            MessageBoxImage warningIcon = MessageBoxImage.Warning;
            MessageBox.Show(messageBoxText, caption, okButton, warningIcon);
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
