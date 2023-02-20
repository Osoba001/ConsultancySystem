using Microsoft.AspNetCore.Http;
using System.Drawing;
using System.Drawing.Imaging;

namespace Utilities.UtlityExtensions
{
    public static class ConversionExtension
    {
        public static Bitmap ResizeImage(this IFormFile file, int width,int height)
        {
            var image = Image.FromStream(file.OpenReadStream(), true, true);
            var newImage = new Bitmap(width,height);
            using (var g = Graphics.FromImage(newImage))
            {
                g.DrawImage(image, 0, 0, width, height);
            }
            return newImage;
        }
        public static byte[] BitmapToByteArray(this Bitmap bitmapImage)
        {
            using (var stream = new MemoryStream())
            {
                bitmapImage.Save(stream, ImageFormat.Png);
                return stream.ToArray();
            }
        }
    }
}
