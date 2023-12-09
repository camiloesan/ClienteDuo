using System.Windows.Controls;

namespace ClienteDuo.Pages
{
    /// <summary>
    /// Interaction logic for PlayerIcon.xaml
    /// </summary>
    public partial class PlayerIcon : UserControl
    {
        string _username;

        public PlayerIcon()
        {
            InitializeComponent();

            _username = "";
        }

        public string Username
        {
            set
            {
                _username = value;
                _nameLabel.Content = _username;
            }

            get
            {
                return _username;
            }
        }
    }
}
