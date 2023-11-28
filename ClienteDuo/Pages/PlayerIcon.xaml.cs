﻿using System;
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
            set {
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
