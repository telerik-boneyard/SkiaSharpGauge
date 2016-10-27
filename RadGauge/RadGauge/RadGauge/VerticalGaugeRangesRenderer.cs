using SkiaSharp;
using Xamarin.Forms;

namespace RadGauge
{
    public class VerticalGaugeRangesRenderer : IGaugePartRenderer
    {
        public VerticalGaugeRangesRenderer(RadVerticalGauge owner)
        {
            this.Owner = owner;
        }

        public RadVerticalGauge Owner { get; set; }

        public void Render(SKCanvas canvas, SKRect layoutSlot)
        {
            float totalHeight = layoutSlot.Height;

            for (int i = 0; i < this.Owner.Ranges.Length - 1; i++)
            {
                var bottom = GaugeRenderHelper.GetRelativePosition(this.Owner.Ranges[i], this.Owner.Minimum, this.Owner.Maximum, layoutSlot.Bottom, layoutSlot.Top, true);
                var top = GaugeRenderHelper.GetRelativePosition(this.Owner.Ranges[i + 1], this.Owner.Minimum, this.Owner.Maximum, layoutSlot.Bottom, layoutSlot.Top, true);

                SKRect rect = new SKRect(layoutSlot.Left, top, layoutSlot.Right, bottom);

                Color color = this.Owner.Colors[i];
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
