using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Drawing;
using System.Windows.Media.Imaging;

namespace nowikowska_217_6B
{
    public class ImageInfo
    {
        public string Name { get; set; }
        public string CreationDate { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public BitmapImage BmpImage;
        public readonly string Path;

        public ImageInfo()
        {
            
        }
        public ImageInfo(string path)
        {
            Path = path;
            Name = System.IO.Path.GetFileNameWithoutExtension(Path);
            CreationDate = File.GetCreationTime(Path).ToLongDateString();
            BmpImage = new BitmapImage(new Uri(Path));
            Width = $"{Convert.ToInt32(BmpImage.Width)} px";
            Height = $"{Convert.ToInt32(BmpImage.Height)} px";
        }
    }
}
