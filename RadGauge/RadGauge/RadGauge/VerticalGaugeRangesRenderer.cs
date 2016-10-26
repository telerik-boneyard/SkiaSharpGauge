using SkiaSharp;
using Xamarin.Forms;

namespace RadGauge
{
    public class VerticalGaugeRangesRenderer : IGaugePartRenderer
    {
        private float min = 0;
        private float max = 100;
        private Color[] colors = { Color.Red, Color.Green, Color.White };

        public VerticalGaugeRangesRenderer()
        {
        }

        internal float[] Ranges { get; set; }

        public void Render(SKCanvas canvas, SKRect layoutSlot)
        {
            float totalHeight = layoutSlot.Height;

            for (int i = 0; i < this.Ranges.Length - 1; i++)
            {
                var bottom = VerticalGaugeIndicatorRenderer.GetTickPosition(this.Ranges[i], min, max, layoutSlot);
                var top = VerticalGaugeIndicatorRenderer.GetTickPosition(this.Ranges[i + 1], min, max, layoutSlot);

                SKRect rect = new SKRect(layoutSlot.Left, top, layoutSlot.Right, bottom);

                Color color = this.colors[i];
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
            return new SKSize(50, availableSize.Height);
        }
    }
}
