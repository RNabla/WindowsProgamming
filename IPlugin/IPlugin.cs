using System.Windows.Media.Imaging;

namespace IPlugin
{
    public interface IPlugin
    {
        string Name { get; }
        BitmapImage Do(BitmapImage image);
    }
}