using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TradeTOS.Extensions
{
    [ContentProperty("Source")]
    public class ImageResourceExtension : IMarkupExtension
    {
        public string Source { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Source == null)
                return null;
            Source = string.Format("TradeTOS.Resources.Images.{0}", Source);
            var imageSource = ImageSource.FromResource(Source);
            return imageSource;
        }
    }

    [ContentProperty("Icon")]
    public class FileImageResourceExtension : IMarkupExtension
    {
        public string Icon { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Icon == null)
                return null;
            string source = string.Format("TradeTOS.Resources.Images.{0}", Icon);
            var imageSource = ImageSource.FromResource(source);
            return imageSource;
        }
    }
}
