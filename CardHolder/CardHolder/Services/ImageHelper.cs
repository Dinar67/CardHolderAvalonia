using System.IO;
using Avalonia.Media.Imaging;

namespace CardHolder.Services;

public static class ImageHelper
{
    public static Bitmap GetImageFromBytes(byte[] bytes){
        return new Bitmap(new MemoryStream(bytes));
    } 
    public static byte[] GetBytesFromImage(Bitmap image){
        using (var stream = new MemoryStream())
        {
            image.Save(stream);
            return stream.ToArray();
        }     
    }
}