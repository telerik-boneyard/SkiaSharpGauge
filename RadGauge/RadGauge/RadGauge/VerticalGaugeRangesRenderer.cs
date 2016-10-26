using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp;
using Xamarin.Forms;

namespace RadGauge
{
    public class VerticalGaugeRangesRenderer : VerticalGaugePart
    {
        private float min = 0;
        private float max = 100;
        private float[] ranges = { 0, 33, 66, 100 };
        private Color[] colors = { Color.White, Color.Green, Color.Red };

        public override void Render(SKCanvas canvas, SKRect layoutSlot)
        {
            float totalHeight = layoutSlot.Height;

            float bottom = layoutSlot.Bottom;

            for (int i = 0; i < ranges.Length - 1; i++)
            {
                float ratio = (ranges[i + 1] - ranges[i]) / (max - min);

                float rangeHeight = totalHeight * ratio;

                SKRect rect = new SKRect(layoutSlot.Left, bottom - rangeHeight, layoutSlot.Right, bottom);
                bottom = bottom - rangeHeight;

                Color color = colors[i];
                DrawRectangle(canvas, rect, color);
            }
        }

        private static void DrawRectangle(SKCanvas canvas, SKRect rect, Color xamarinColor)
        {
            using (SKPaint paint = new SKPaint())
            {
                paint.Color = xamarinColor.ToSKColor();
                paint.IsAntialias = true;

                canvas.DrawRect(rect, paint);
            }
        }

        internal SKSize Measure(SKSize availableSize)
        {
            return new SKSize(20, 150);
        }
    }
}
