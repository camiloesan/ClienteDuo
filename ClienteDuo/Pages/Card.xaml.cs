using System.Windows.Controls;
using System.Windows.Media;

namespace ClienteDuo.Pages
{
    public partial class Card : UserControl
    {
        string _number;
        string _color;

        public Card()
        {
            InitializeComponent();
            _number = "2";
            _color = "000000";
        }

        public string Color
        {
            set
            {
                _color = value;
                _colorRectangle.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom(_color));
            }
            get => _color;
        }
        public string Number
        {
            set
            {
                _number = value;
                _numberLabel.Content = _number.ToString();
            }
            get => _number;
        }


    }
}
