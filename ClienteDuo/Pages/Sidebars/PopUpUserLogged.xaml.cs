using System.Windows.Controls;

namespace ClienteDuo.Pages.Sidebars
{
    /// <summary>
    /// Interaction logic for PopUpUserLogged.xaml
    /// </summary>
    public partial class PopUpUserLogged : UserControl
    {
        public PopUpUserLogged()
        {
            InitializeComponent();
        }

        public void SetLabelText(string username)
        {
            LblMessage.Content = username + " " + Properties.Resources.DlgIsNowActive;
        }
    }
}
