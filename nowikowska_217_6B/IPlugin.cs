using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

namespace nowikowska_217_6B
{
    public interface IPlugin
    {
        string Name { get; }
        BitmapImage Do(BitmapImage image);
    }

    public class ExamplePlugin : IPlugin
    {
        public string Name
        {
            get
            {
                return "Randomize Colors";
            }
        }

        public BitmapImage Do(BitmapImage bmpOriginal)
        {
            var bmp = DoSomethingWithImage(bmpOriginal);

            return bmp;
        }

        public BitmapImage DoSomethingWithImage(BitmapImage bmpOriginal)
        {
//            var bmp = new BitmapImage()
            return null;
        }
    }
}
