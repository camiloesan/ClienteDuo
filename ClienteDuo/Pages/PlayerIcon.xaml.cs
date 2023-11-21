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
        string username;

        public PlayerIcon()
        {
            InitializeComponent();
        }

        public string Username 
        {
            set {
                username = value;
                _nameLabel.Content = username;
            }

            get
            {
                return username;
            }
             
        }
    }
}
