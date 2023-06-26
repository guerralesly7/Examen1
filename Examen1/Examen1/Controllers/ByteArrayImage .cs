using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.IO;
using Xamarin.Forms;

namespace Examen1.Controllers
{
    public class ByteArrayImage: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ImageSource image = null;

            if (value != null)
            {
                byte[] bytesImage = (byte[])value;
                var stream = new MemoryStream(bytesImage);
                image = ImageSource.FromStream(() => stream);
            }

            return image;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
