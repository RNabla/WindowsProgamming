using System;
using System.Collections.Generic;
using System.IO;
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

namespace nowikowska_217_6B
{
    /// <summary>
    /// Interaction logic for ImageWindow.xaml
    /// </summary>
    public partial class ImageWindow : Window
    {
        private readonly ImageInfo _ii;
        private List<IPlugin> _plugins = new List<IPlugin>();

        public ImageWindow(ImageInfo ii)
        {
            _ii = ii;
            DataContext = ii;
            InitializeComponent();
            Picture.Source = ii.BmpImage;
            Picture.Width = Convert.ToDouble(Width);
            Picture.Height = Convert.ToDouble(Height);
            GetPlugins();
        }

        public void GetPlugins()
        {
            var dlls = new DirectoryInfo(_ii.Path).GetFiles().Where(fi => fi.Extension == ".dll");
            Plugins.DataContext = null;
            _plugins.Clear();

            Plugins.DataContext = _plugins;
        }

        public void UsePluginClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
