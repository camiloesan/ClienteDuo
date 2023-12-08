using ClienteDuo.Utilities;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ClienteDuo.Pages.Sidebars
{
    public partial class SidebarUserProfile : UserControl
    {
        public SidebarUserProfile()
        {
            InitializeComponent();
            SetProfilePicture();
            FillLabels();
        }

        private void FillLabels()
        {
            LblPlayerId.Content = SessionDetails.UserId;
            LblUsername.Content = SessionDetails.Username;
            LblEmail.Content = SessionDetails.Email;
            LblTotalWins.Content = SessionDetails.TotalWins;
        }

        private void SetProfilePicture()
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
            ImageProfilePicture.Source = bitmapImage;
            ImageProfilePicture.Stretch = Stretch.UniformToFill;
        }

        private void BtnCancelEvent(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
        }
    }
}
