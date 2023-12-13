using ClienteDuo.DataService;
using ClienteDuo.Pages.Sidebars;
using ClienteDuo.Utilities;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ClienteDuo.Pages
{
    /// <summary>
    /// Interaction logic for ModifyProfile.xaml
    /// </summary>
    public partial class ModifyProfile : Page
    {
        int _selectedPictureId = 0;

        public ModifyProfile()
        {
            InitializeComponent();
            InitializeCurrentProfilePicture();
            InitializeAvailableProfilePictures();
        }

        private void InitializeCurrentProfilePicture()
        {
            SetCurrentProfilePicturePreview(SessionDetails.PictureID);
        }

        private void SetCurrentProfilePicturePreview(int pictureId)
        {
            _selectedPictureId = pictureId;
            BitmapImage bitmapImage = new BitmapImage(new System.Uri("pack://application:,,,/ClienteDuo;component/Images/pfp0.png"));
            switch (pictureId)
            {
                case 0:
                    bitmapImage = new BitmapImage(new System.Uri("pack://application:,,,/ClienteDuo;component/Images/pfp0.png"));
                    break;
                case 1:
                    bitmapImage = new BitmapImage(new System.Uri("pack://application:,,,/ClienteDuo;component/Images/pfp1.jpg"));
                    break;
                case 2:
                    bitmapImage = new BitmapImage(new System.Uri("pack://application:,,,/ClienteDuo;component/Images/pfp2.jpg"));
                    break;
                case 3:
                    bitmapImage = new BitmapImage(new System.Uri("pack://application:,,,/ClienteDuo;component/Images/pfp3.jpg"));
                    break;
            }
            ImageCurrentProfilePicture.Source = bitmapImage;
            ImageCurrentProfilePicture.Stretch = Stretch.UniformToFill;
        }

        private void InitializeAvailableProfilePictures()
        {
            ImagePfp0.Source = new BitmapImage(new System.Uri("pack://application:,,,/ClienteDuo;component/Images/pfp0.png"));
            ImagePfp1.Source = new BitmapImage(new System.Uri("pack://application:,,,/ClienteDuo;component/Images/pfp1.jpg"));
            ImagePfp2.Source = new BitmapImage(new System.Uri("pack://application:,,,/ClienteDuo;component/Images/pfp2.jpg"));
            ImagePfp3.Source = new BitmapImage(new System.Uri("pack://application:,,,/ClienteDuo;component/Images/pfp3.jpg"));
        }

        private void BtnContinueEvent(object sender, RoutedEventArgs e)
        {
            bool result = false;
            try
            {
                result = UsersManager.UpdateProfilePicture(SessionDetails.UserId, _selectedPictureId);
            }
            catch (CommunicationException)
            {
                MainWindow.ShowMessageBox(Properties.Resources.DlgServiceException, MessageBoxImage.Error);
            }

            if (result)
            {
                SessionDetails.PictureID = _selectedPictureId;
                MainWindow.ShowMessageBox(Properties.Resources.DlgProfilePictureUpdated, MessageBoxImage.Information);
                MainMenu mainMenu = new MainMenu();
                Application.Current.MainWindow.Content = mainMenu;
            }
        }

        private void BtnCancelEvent(object sender, RoutedEventArgs e)
        {
            MainMenu mainMenu = new MainMenu();
            Application.Current.MainWindow.Content = mainMenu;
        }

        private void BtnChangePasswordEvent(object sender, RoutedEventArgs e)
        {
            EmailConfirmation emailConfirmation = new EmailConfirmation();
            Application.Current.MainWindow.Content = emailConfirmation;
        }

        private void BtnPfp0Event(object sender, RoutedEventArgs e)
        {
            SetCurrentProfilePicturePreview(0);
        }

        private void BtnPfp1Event(object sender, RoutedEventArgs e)
        {
            SetCurrentProfilePicturePreview(1);
        }

        private void BtnPfp2Event(object sender, RoutedEventArgs e)
        {
            SetCurrentProfilePicturePreview(2);
        }

        private void BtnPfp3Event(object sender, RoutedEventArgs e)
        {
            SetCurrentProfilePicturePreview(3);
        }
    }
}
