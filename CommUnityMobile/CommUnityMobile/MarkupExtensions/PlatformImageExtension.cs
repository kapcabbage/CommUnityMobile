using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace FreshWithSQLite.MarkupExtensions
{
    [ContentProperty("SourceImage")]
    public class PlatformImageExtension : IMarkupExtension<string>
    {

        public string SourceImage { get; set; }

        public string ProvideValue(IServiceProvider serviceProvider)
        {
            if (SourceImage == null)
                return null;
            string imagePath = "";
            if (Device.OS == TargetPlatform.Android)
            {
                imagePath = SourceImage;
            }
            else if(Device.OS == TargetPlatform.iOS)
            {
                imagePath = SourceImage + ".png";
            }
            else if(Device.OS == TargetPlatform.WinPhone)
            {
                imagePath = "Images/" + SourceImage + ".png";
            }
            else if (Device.OS == TargetPlatform.Windows)
            {
                imagePath = "Images/" + SourceImage + ".png";
            }

            return imagePath;
        }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return ProvideValue(serviceProvider);
        }
    }
}
