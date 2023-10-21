using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
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
            string caption = "Alert *internacionalizar*";

            MessageBoxButton okButton = MessageBoxButton.OK;
            MessageBoxImage warningIcon = MessageBoxImage.Warning;
            MessageBox.Show(messageBoxText, caption, okButton, warningIcon);
        }
    }
}
