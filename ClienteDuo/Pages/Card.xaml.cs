using System;
using System.CodeDom;
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
using System.Windows.Shapes;

namespace ClienteDuo.Pages
{
    public partial class Card: UserControl
    {
        string _number; 
        string _color;
        bool _isSelected;

        public Card()
        {
            InitializeComponent();
            _number = "2";
            _color = "000000";
            _isSelected = true;
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

        public void SelectCard(object sender, RoutedEventArgs e)
        {

        }
    }
}
