using SkiaSharp;
using Xamarin.Forms;

namespace RadGauge
{
    public static class ColorExtensions
    {
        public static SKColor ToSKColor(this Color color)
        {
            return new SKColor(
                    (byte)(color.R * 255),
                    (byte)(color.G * 255),
                    (byte)(color.B * 255),
                    (byte)(color.A * 255));
        }
    }
}
