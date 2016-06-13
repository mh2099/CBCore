namespace CBViewerX.Converter
{
    using System;
    using System.IO;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Media.Imaging;

    public class BinaryImageConverter : IValueConverter
    {
        /// <summary>
        /// Convert Byte array in BitmapImage (System.Windows.Media.Imaging.BitmapImage)
        /// </summary>
        /// <param name="value">Byte array</param>
        /// <param name="targetType">BitmapImage</param>
        /// <returns></returns>
        public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            if (!(value is Byte[])) return null;

            var ms = new MemoryStream((Byte[]) value);

            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = ms;
            bitmapImage.EndInit();

            return bitmapImage;
        }
        /// <summary>
        /// Convert BitmapImage (System.Windows.Media.Imaging.BitmapImage) in Byte array
        /// </summary>
        /// <param name="value">BitmapImage</param>
        /// <param name="targetType">Byte array</param>
        /// <returns></returns>
        public Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            if (!(value is BitmapImage)) return null;

            Byte[] data;
            var encoder = new JpegBitmapEncoder();
            var bitmapImage = (BitmapImage) value;

            encoder.Frames.Add(BitmapFrame.Create(bitmapImage));

            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }

            return data;
        }
    }
}