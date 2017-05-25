using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace nowikowska_217_6B
{
    public class ImageInfo
    {
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
            Original = new BitmapImage(new Uri(Path));
            Width = $"{Convert.ToInt32(BmpImage.Width)} px";
            Height = $"{Convert.ToInt32(BmpImage.Height)} px";
        }

        public string Name { get; set; }
        public string CreationDate { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public BitmapImage BmpImage { get; set; }
        public BitmapImage Original { get; set; }
    }
}