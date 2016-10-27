using SkiaSharp;
using Xamarin.Forms;

namespace RadGauge
{
    public class VerticalGaugeIndicatorRenderer : IGaugePartRenderer
    {
        double value = 67;

        float actualWidthRequest = 50;
        float widthRequest = 50;

        private Color color = Color.FromHex("55AAEE");
        private SKColor skColor;

        public VerticalGaugeIndicatorRenderer(RadVerticalGauge owner)
        {
            this.Owner = owner;
            this.skColor = ColorExtensions.ToSKColor(this.color);
        }

        public RadVerticalGauge Owner { get; set; }

        public double Value
        {
            get
            {
                return this.value;
            }
            set
            {
                this.value = value;
            }
        }

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

        public Color Color
        {
            get
            {
                return this.color;
            }
            set
            {
                if (this.color != value)
                {
                    this.color = value;
                    this.skColor = ColorExtensions.ToSKColor(this.color);
                }
            }
        }

        public void Render(SKCanvas canvas, SKRect layoutSlot)
        {
            float position = GaugeRenderHelper.GetRelativePosition(this.value, this.Owner.Minimum, this.Owner.Maximum, layoutSlot.Bottom, layoutSlot.Top);
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
            paint.Color = this.skColor;
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
