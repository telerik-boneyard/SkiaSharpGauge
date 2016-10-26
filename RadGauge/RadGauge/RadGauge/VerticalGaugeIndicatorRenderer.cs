using SkiaSharp;
using System;

namespace RadGauge
{
    public class VerticalGaugeIndicatorRenderer : IGaugePartRenderer
    {
        int min = 0;
        int max = 100;

        double value = 67;

        float actualWidthRequest = 50;
        float widthRequest = 50;

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

        public void Render(SKCanvas canvas, SKRect layoutSlot)
        {
            float position = (float)GetTickPosition(this.value, this.min, this.max, layoutSlot);
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
            paint.Color = new SKColor(0x55, 0xaa, 0xee);
            paint.StrokeCap = SKStrokeCap.Round;
            paint.StrokeWidth = 5;

            return paint;
        }

        internal SKSize Measure(SKSize availableSize)
        {
            float desiredHeight = availableSize.Height < double.MaxValue ? availableSize.Height : 0;
            return new SKSize(this.actualWidthRequest, desiredHeight);
        }

        internal static float GetTickPosition(double value, double min, double max, SKRect layoutSlot)
        {
            double relativePosition = (value - min) / (max - min);
            double position = relativePosition * layoutSlot.Height;
            position = layoutSlot.Bottom - position;

            return (float)position;
        }
    }
}
