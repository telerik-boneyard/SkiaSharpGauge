using SkiaSharp;
using System;

namespace RadGauge
{
    public class VerticalGaugeIndicator : VerticalGaugePart
    {
        int min = 0;
        int max = 100;
        int value = 67;
        int desiredWidth = 5;

        public override void Render(SKCanvas canvas, SKRect layoutSlot)
        {
            float position = (float)GetTickPosition(this.value, this.min, this.max, layoutSlot);
            using (var paint = new SKPaint())
            {
                paint.IsAntialias = true;
                paint.Color = new SKColor(0x55, 0xaa, 0xee);
                paint.StrokeCap = SKStrokeCap.Round;
                paint.StrokeWidth = 5;

                canvas.DrawLine(layoutSlot.Left, position, layoutSlot.Right, position, paint);
            }
        }

        internal SKSize Measure(SKSize availableSize)
        {
            float desiredHeight = availableSize.Height < double.MaxValue ? availableSize.Height : 0;
            return new SKSize(this.desiredWidth, desiredHeight);
        }

        internal static double GetTickPosition(double value, double min, double max, SKRect layoutSlot)
        {
            double relativePosition = (value - min) / (max - min);
            double position = relativePosition * layoutSlot.Height;
            position = layoutSlot.Bottom - position;

            return position;
        }
    }
}
