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
        public string Name;
        public string CreationDate;
        public string Dimensions;
        public string Extension;
        public BitmapImage BmpImage;
        public readonly string Path;
        public ImageInfo(string path)
        {
            Path = path;
        }

        public async void GetImageDetails()
        {
            await new Task(() =>
            {
                try
                {
                    Name = System.IO.Path.GetFileNameWithoutExtension(Path);
                    CreationDate = File.GetCreationTime(Path).ToLongDateString();
                    BmpImage = new BitmapImage(new Uri(Path));
                    Dimensions = $"{BmpImage.Width}x{BmpImage.Height}";
                    Extension = System.IO.Path.GetExtension(Path);
                }
                catch { }
            });
        }
    }
}
