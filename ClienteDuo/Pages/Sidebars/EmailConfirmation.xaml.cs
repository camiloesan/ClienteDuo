using ClienteDuo.DataService;
using ClienteDuo.Utilities;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;

namespace ClienteDuo.Pages.Sidebars
{
    /// <summary>
    /// Interaction logic for EmailConfirmation.xaml
    /// </summary>
    public partial class EmailConfirmation : Page
    {
        public EmailConfirmation()
        {
            InitializeComponent();
        }

        private void BtnContinueEvent(object sender, RoutedEventArgs e)
        {
            string email = TBoxEmail.Text.Trim();
            string language = System.Threading.Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;
            try
            {
                RequestCode(email, language);
            }
            catch (CommunicationException)
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgServiceException, MessageBoxImage.Error);
            }
        }

        private void RequestCode(string email, string lang)
        {
            if (!UsersManager.IsEmailTaken(email))
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgEmailNonExistent, MessageBoxImage.Information);
            }
            else
            {
                int confirmationCode = UsersManager.SendConfirmationCode(email, lang);

                if (confirmationCode != -1)
                {
                    EmailCodeVerification emailCodeVerification = new EmailCodeVerification(email, confirmationCode);
                    Application.Current.MainWindow.Content = emailCodeVerification;
                }
            }
        }

        private void BtnCancelEvent(object sender, RoutedEventArgs e)
        {
            if (SessionDetails.IsGuest)
            {
                Login login = new Login();
                Application.Current.MainWindow.Content = login;
            }
            else
            {
                MainMenu mainMenu = new MainMenu();
                Application.Current.MainWindow.Content = mainMenu;
            }
        }
    }
}
