using System.Windows;

namespace ClienteDuo.Pages
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Launcher launcher = new Launcher();
            this.ResizeMode = ResizeMode.NoResize;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Content = launcher;
        }

        public static void ShowMessageBox(string message)
        {
            string messageBoxText = message;
            string caption = Properties.Resources.TitleAlert;

            MessageBoxButton okButton = MessageBoxButton.OK;
            MessageBoxImage warningIcon = MessageBoxImage.Warning;
            MessageBox.Show(messageBoxText, caption, okButton, warningIcon);
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
