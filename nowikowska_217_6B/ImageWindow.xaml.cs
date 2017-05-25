using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace nowikowska_217_6B
{
    /// <summary>
    ///     Interaction logic for ImageWindow.xaml
    /// </summary>
    public partial class ImageWindow : Window
    {
        private readonly ImageInfo _ii;
        private readonly List<IPlugin.IPlugin> _plugins = new List<IPlugin.IPlugin>();

        public ImageWindow(ImageInfo ii)
        {
            _ii = ii;
            DataContext = ii;
            InitializeComponent();
            Picture.Source = ii.BmpImage;
            Picture.Width = Convert.ToDouble(Width);
            Picture.Height = Convert.ToDouble(Height);
            MaxHeight = SystemParameters.PrimaryScreenHeight;
            MaxWidth = SystemParameters.PrimaryScreenWidth;
            GetPlugins();
        }

        public void GetPlugins()
        {
            Plugins.DataContext = null;
            _plugins.Clear();
            var dlls = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.dll");
            ICollection<Assembly> assemblies = new List<Assembly>(dlls.Length);
            foreach (var dllFile in dlls)
            {
                var an = AssemblyName.GetAssemblyName(dllFile);
                var assembly = Assembly.Load(an);
                assemblies.Add(assembly);
            }
            var pluginType = typeof(IPlugin.IPlugin);
            ICollection<Type> pluginTypes = new List<Type>();
            foreach (var assembly in assemblies)
                if (assembly != null)
                {
                    var types = assembly.GetTypes();
                    foreach (var type in types)
                        if (type.IsInterface || type.IsAbstract)
                        {
                        }
                        else
                        {
                            if (type.GetInterface(pluginType.FullName) != null)
                                pluginTypes.Add(type);
                        }
                }
            foreach (var type in pluginTypes)
                _plugins.Add((IPlugin.IPlugin)Activator.CreateInstance(type));
            Plugins.DataContext = _plugins;
        }

        public void UsePluginClick(object sender, RoutedEventArgs e)
        {
            var plug = Plugins.SelectedItem as IPlugin.IPlugin;
            Picture.Source = plug.Do(Picture.Source as BitmapImage);
        }

        private void TooltipExit(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ResetImage(object sender, RoutedEventArgs e)
        {
            Picture.Source = new BitmapImage(new Uri(_ii.Path));
        }

        private void SaveImage(object sender, RoutedEventArgs e)
        {
            using (var sfd = new SaveFileDialog
            {
                Filter = "Image files(*.jpg, *.jpeg, *.bmp, *.png)|*.jpg; *.jpeg; *.bmp; *.png",
                FileName = $"{_ii.Name}_0"
            })
            {
                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    try
                    {
                        var encoder = new JpegBitmapEncoder();
                        encoder.Frames.Add(BitmapFrame.Create(Picture.Source as BitmapImage));
                        using (var filestream = new FileStream(sfd.FileName, FileMode.Create))
                        {
                            encoder.Save(filestream);
                        }
                    }
                    catch
                    {
                    }
            }
        }
    }
}