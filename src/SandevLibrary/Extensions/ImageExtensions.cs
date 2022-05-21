using System;
using System.Drawing;
using System.IO;

namespace SandevLibrary.Extensions
{
    public static class ImageExtensions
    {
        public static byte[] ImageToByteArray(this Image imageIn)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

                return ms.ToArray();
            }
        }

        public static byte[] ImageToByteArray(this Image imageIn, System.Drawing.Imaging.ImageFormat imageFormat)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                imageIn.Save(ms, imageFormat);

                return ms.ToArray();
            }
        }

        public static Image ByteArrayToImage(this byte[] data)
        {
            var ic = new ImageConverter();
            var img = (Image)ic.ConvertFrom(data);

            return img;
        }

        public static string ImageToBase64(this string path)
        {
            using (Image image = Image.FromFile(path))
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();
                    string base64String = Convert.ToBase64String(imageBytes);

                    return base64String;
                }
            }
        }

        public static string ImageToBase64(this string path, System.Drawing.Imaging.ImageFormat format)
        {
            using (Image image = Image.FromFile(path))
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, format);
                    byte[] imageBytes = m.ToArray();
                    string base64String = Convert.ToBase64String(imageBytes);

                    return base64String;
                }
            }
        }

        public static Image Base64ToImage(this string base64String)
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);
            using (MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                ms.Write(imageBytes, 0, imageBytes.Length);
                Image image = Image.FromStream(ms, true);

                return image;
            }
        }
    }
}
