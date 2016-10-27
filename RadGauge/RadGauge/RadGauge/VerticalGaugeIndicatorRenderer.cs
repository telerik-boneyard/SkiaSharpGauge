using SkiaSharp;
using Xamarin.Forms;

namespace RadGauge
{
    public class VerticalGaugeIndicatorRenderer : IGaugePartRenderer
    {
        float actualWidthRequest = 50;
        float widthRequest = 50;

        public VerticalGaugeIndicatorRenderer(RadVerticalGauge owner)
        {
            this.Owner = owner;
        }

        public RadVerticalGauge Owner { get; set; }

        public float WidthRequest
        {
            get
            {
                return this.widthRequest;
            }
            set
            {
                if (this.widthRequest == value)
                {
                    return;
                }
                this.widthRequest = value;

                if (0 <= value && value <= float.MaxValue)
                {
                    this.actualWidthRequest = value;
                }
                else
                {
                    this.actualWidthRequest = 0;
                }
            }
        }

        public void Render(SKCanvas canvas, SKRect layoutSlot)
        {
            float position = GaugeRenderHelper.GetRelativePosition(this.Owner.IndicatorValue, this.Owner.Minimum, this.Owner.Maximum, layoutSlot.Bottom, layoutSlot.Top);
            if (position > layoutSlot.Bottom && position > layoutSlot.Top || position < layoutSlot.Bottom && position < layoutSlot.Top)
            {
                return;
            }

            using (var paint = this.CreatePaint())
            {
                using (var path = new SKPath())
                {
                    path.MoveTo(layoutSlot.Left, position);
                    path.LineTo(layoutSlot.Right, position - (layoutSlot.Width / 2));
                    path.LineTo(layoutSlot.Right, position + (layoutSlot.Width / 2));
                    path.Close();

                    canvas.DrawPath(path, paint);
                }
            }
        }

        private SKPaint CreatePaint()
        {
            SKPaint paint = new SKPaint();
            paint.IsAntialias = true;
            paint.Color = ColorExtensions.ToSKColor(this.Owner.IndicatorColor);
            paint.StrokeCap = SKStrokeCap.Round;
            paint.StrokeWidth = 5;

            return paint;
        }

        internal SKSize Measure(SKSize availableSize)
        {
            float desiredHeight = availableSize.Height < double.MaxValue ? availableSize.Height : 0;
            return new SKSize(this.actualWidthRequest, desiredHeight);
        }
    }
}
