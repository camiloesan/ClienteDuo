using ClienteDuo.DataService;
using ClienteDuo.Utilities;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.ServiceModel;
using System.Windows;

namespace ClienteDuo.Pages
{
    public partial class MainWindow : Window, IUserConnectionHandlerCallback
    {
        private static UserConnectionHandlerClient _userConnectionHandlerClient;
        public static InstanceContext _instanceContext;

        public MainWindow()
        {
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Closing += OnWindowClosing;

            _instanceContext = new InstanceContext(this);
            var launcher = new Launcher();
            Content = launcher;
        }

        public static void NotifyLogin(User user)
        {
            _userConnectionHandlerClient = new UserConnectionHandlerClient(_instanceContext);
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

        public void OnWindowClosing(object sender, CancelEventArgs e)
        {
            var userConnectionHandlerClient = new UserConnectionHandlerClient(_instanceContext);
            var user = new User
            {
                ID = SessionDetails.UserId,
            };
            userConnectionHandlerClient.NotifyLogOut(user);
        }

        public static bool ShowConfirmationBox(string message)
        {
            string messageBoxText = message;
            string caption = Properties.Resources.TitleAlert;

            MessageBoxButton buttons = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Question;

            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, buttons, icon);
            return result == MessageBoxResult.Yes;
        }

    }
}
