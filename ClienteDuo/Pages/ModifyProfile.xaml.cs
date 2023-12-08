using ClienteDuo.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for ModifyProfile.xaml
    /// </summary>
    public partial class ModifyProfile : Page
    {
        public ModifyProfile()
        {
            InitializeComponent();
            InitializeCurrentProfilePicture();
            InitializeAvailableProfilePictures();
        }

        private void InitializeCurrentProfilePicture()
        {
            SetCurrentProfilePicture();
        }

        private void SetCurrentProfilePicture()
        {
            BitmapImage bitmapImage = new BitmapImage(new System.Uri("pack://application:,,,/ClienteDuo;component/Images/pfp0.png"));
            switch (SessionDetails.PictureID)
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

        private void BtnCancelEvent(object sender, RoutedEventArgs e)
        {
            MainMenu mainMenu = new MainMenu();
            Application.Current.MainWindow.Content = mainMenu;
        }
    }
}
