using SkiaSharp;
using Xamarin.Forms;

namespace RadGauge
{
    public class VerticalGaugeRangesRenderer : VerticalGaugePart
    {
        private float min = 0;
        private float max = 100;
        private float[] ranges = { 0, 33, 66, 100 };
        private Color[] colors = { Color.Red, Color.Green, Color.White };

        public override void Render(SKCanvas canvas, SKRect layoutSlot)
        {
            float totalHeight = layoutSlot.Height;

            for (int i = 0; i < ranges.Length - 1; i++)
            {
                var bottom = VerticalGaugeIndicator.GetTickPosition(ranges[i], min, max, layoutSlot);
                var top = VerticalGaugeIndicator.GetTickPosition(ranges[i + 1], min, max, layoutSlot);

                SKRect rect = new SKRect(layoutSlot.Left, top, layoutSlot.Right, bottom);

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
