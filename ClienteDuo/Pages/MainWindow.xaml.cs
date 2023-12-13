using ClienteDuo.DataService;
using ClienteDuo.Utilities;
using System.ComponentModel;
using System.ServiceModel;
using System.Windows;

namespace ClienteDuo.Pages
{
    public partial class MainWindow : Window, IUserConnectionHandlerCallback
    {
        private static UserConnectionHandlerClient _userConnectionHandlerClient;
        private static InstanceContext _instanceContext;

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

        public static void NotifyLogin(UserDTO user)
        {
            _userConnectionHandlerClient = new UserConnectionHandlerClient(_instanceContext);
            _userConnectionHandlerClient.NotifyLogIn(user);
        }

        public static void ShowMessageBox(string message, MessageBoxImage messageBoxImage)
        {
            string caption = Properties.Resources.TitleAlert;

            const MessageBoxButton okButton = MessageBoxButton.OK;
            MessageBox.Show(message, caption, okButton, messageBoxImage);
        }

        public void UserLogged(string username)
        {
            MainMenu.ShowPopUpFriendLogged(username);
        }

        private void OnWindowClosing(object sender, CancelEventArgs e)
        {
            NotifyLogOut(SessionDetails.UserId, SessionDetails.IsGuest);
        }

        public static void NotifyLogOut(int userId, bool isGuest)
        {
            if (!isGuest)
            {
                var userConnectionHandlerClient = new UserConnectionHandlerClient(_instanceContext);
                var user = new UserDTO
                {
                    ID = userId
                };
                userConnectionHandlerClient.NotifyLogOut(user);
            }
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
