using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace IPlugin
{
    public class Rotate90 : IPlugin
    {
        public string Name => "Rotate90";

        public BitmapImage Do(BitmapImage image)
        {
            var newBmp = new BitmapImage();

            newBmp.BeginInit();
            newBmp.UriSource = image.UriSource;
            newBmp.Rotation = Rotation.Rotate90;
            newBmp.EndInit();
            return newBmp;
        }
    }

    public class Rotate180 : IPlugin
    {
        public string Name => "Rotate180";

        public BitmapImage Do(BitmapImage image)
        {
            var newBmp = new BitmapImage();

            newBmp.BeginInit();
            newBmp.UriSource = image.UriSource;
            newBmp.Rotation = Rotation.Rotate180;
            newBmp.EndInit();
            return newBmp;
        }
    }

    public class Rotate270 : IPlugin
    {
        public string Name => "Rotate270";

        public BitmapImage Do(BitmapImage image)
        {
            var newBmp = new BitmapImage();

            newBmp.BeginInit();
            newBmp.UriSource = image.UriSource;
            newBmp.Rotation = Rotation.Rotate270;
            newBmp.EndInit();
            return newBmp;
        }
    }

    public class Rotate360 : IPlugin
    {
        public string Name => "Rotate360";

        public BitmapImage Do(BitmapImage image)
        {
            var newBmp = new BitmapImage();

            newBmp.BeginInit();
            newBmp.UriSource = image.UriSource;
            newBmp.Rotation = Rotation.Rotate0;
            newBmp.EndInit();
            return newBmp;
        }
    }

    public class Negate : IPlugin
    {
        public string Name => "Negate";

        public BitmapImage Do(BitmapImage image)
        {
            var newBmp = new BitmapImage();
            var modifiedImage = new WriteableBitmap(image);

            var h = modifiedImage.PixelHeight;
            var w = modifiedImage.PixelWidth;
            var pixelData = new int[w * h];
            var widthInByte = 4 * w;

            modifiedImage.CopyPixels(pixelData, widthInByte, 0);

            for (var i = 0; i < pixelData.Length; i++)
                pixelData[i] ^= 0x00ffffff;

            modifiedImage.WritePixels(new Int32Rect(0, 0, w, h), pixelData, widthInByte, 0);

            using (var stream = new MemoryStream())
            {
                var encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(modifiedImage));
                encoder.Save(stream);
                newBmp.BeginInit();
                newBmp.CacheOption = BitmapCacheOption.OnLoad;
                newBmp.StreamSource = stream;
                newBmp.UriSource = image.UriSource;
                newBmp.EndInit();
                newBmp.Freeze();
            }
            return newBmp;
        }
    }
}