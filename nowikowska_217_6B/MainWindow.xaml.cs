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
using Microsoft.Win32;

namespace nowikowska_217_6B
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenImageClick(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFileDialog
            {
                Filter = "Image files(*.jpg, *.jpeg, *.bmp, *.png)|*.jpg; *.jpeg; *.bmp; *.png"
            };
            var result = ofd.ShowDialog();
            if (result.HasValue && result.Value)
            {
                var iw = new ImageWindow(new ImageInfo(ofd.FileName));
                iw.Show();
            }
        }

        private void OpenFolderClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ExitClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AboutClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Simple Image Browser","About",MessageBoxButton.OK,MessageBoxImage.Information);
        }
    }
}
