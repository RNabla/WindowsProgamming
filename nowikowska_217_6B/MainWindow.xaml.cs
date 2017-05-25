using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using MessageBox = System.Windows.MessageBox;
using TreeView = System.Windows.Controls.TreeView;

namespace nowikowska_217_6B
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Picture> Thumbnails = new List<Picture>();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = Thumbnails;
            foreach (var drive in Environment.GetLogicalDrives())
            {
                var node = new TreeViewItem
                {
                    Header = drive,
                    Tag = drive
                };
                node.Expanded += ExpandFolder;
                node.Items.Add(null);
                Folders.Items.Add(node);
            }
        }

        private void ExpandFolder(object sender, RoutedEventArgs e)
        {
            var node = sender as TreeViewItem;
            if (node.Items.Count == 1 && node.Items[0] == null)
            {
                node.Items.Clear();
                try
                {
                    foreach (var dir in Directory.GetDirectories(node.Tag.ToString()))
                    {
                        var subnode = new TreeViewItem
                        {
                            Header = Path.GetFileNameWithoutExtension(dir),
                            Tag = dir
                        };
                        subnode.Items.Add(null);
                        subnode.Expanded += ExpandFolder;
                        node.Items.Add(subnode);
                    }
                }
                catch
                {
                }
            }
        }

        private void OpenImageClick(object sender, RoutedEventArgs e)
        {
            using (
                var ofd = new OpenFileDialog
                {
                    Filter = "Image files(*.jpg, *.jpeg, *.bmp, *.png)|*.jpg; *.jpeg; *.bmp; *.png"
                })
            {
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    new ImageWindow(new ImageInfo(ofd.FileName)).Show();
                }
            }
        }

        private void OpenImagesFromFolder(string path)
        {
            DataContext = null;
            Thumbnails.Clear();
            var exts = new[] {".jpg", ".jpeg", ".bmp", ".png"};
            try
            {
                var fileInfos = new DirectoryInfo(path).GetFiles().Where(fi => exts.Contains(fi.Extension));
                foreach (var fileInfo in fileInfos)
                    Thumbnails.Add(new Picture
                    {
                        PathImage = fileInfo.FullName,
                        Name = Path.GetFileNameWithoutExtension(fileInfo.FullName)
                    });
            }
            catch { }
            DataContext = Thumbnails;
            GC.Collect();
        }

        private void TVI_DoubleClick(object sender, RoutedEventArgs e)
        {
            OpenImagesFromFolder(((TreeViewItem) ((TreeView) e.Source).SelectedItem).Tag.ToString());
        }

        private void Library_DoubleClick(object sender, RoutedEventArgs e)
        {
            new ImageWindow(new ImageInfo(((Picture)((System.Windows.Controls.ListView)e.Source).SelectedItem).PathImage)).Show();
        }
        private void OpenFolderClick(object sender, RoutedEventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    OpenImagesFromFolder(fbd.SelectedPath);
            }
        }

        private void ExitClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AboutClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Simple Image Browser", "About", MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        public struct Picture
        {
            public string Name { get; set; }
            public string PathImage { get; set; }
        }
    }

    internal class PathImageToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new BitmapImage(new Uri((string) value));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}