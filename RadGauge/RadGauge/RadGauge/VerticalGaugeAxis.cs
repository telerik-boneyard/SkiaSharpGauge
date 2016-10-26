using System;
using SkiaSharp;

namespace RadGauge
{
    public class VerticalGaugeAxis : VerticalGaugePart
    {
        double min = 0;
        double max = 100;
        double step = 10;
        float fontSize = 20f;
        float axisWidth = 1f;
        float tickLength = 5f;
        float tickThickness = 1f;

        SKSize maxLabelSize;

        public SKSize MaxLabelSize
        {
            get { return maxLabelSize; }
        }

        public SKSize Measure(SKSize rect)
        {
            this.maxLabelSize = GetMaxLabelSize();
            var width = maxLabelSize.Width + axisWidth + tickLength;

            return new SKSize(width, rect.Height);
        }

        public override void Render(SKCanvas canvas, SKRect layoutSlot)
        {
            float top = maxLabelSize.Height / 2;
            float left = 0;

            var height = layoutSlot.Height - maxLabelSize.Height;

            var axisLayoutSlot = new SKRect(left, top, left + layoutSlot.Width, top + height);
            var tickLeft = left + maxLabelSize.Width;

            for (var i = min; i <= max; i += step)
            {
                var position = VerticalGaugeIndicator.GetTickPosition(i, min, max, axisLayoutSlot);
                DrawLabel(i.ToString(), left, position, canvas);


                var tickTop = position;

                var tickRect = new SKRect(tickLeft, tickTop, tickLeft + tickLength, tickTop + tickThickness);
                DrawRect(tickRect, canvas);
            }

            var axisLeft = tickLeft + tickLength;


            DrawRect(new SKRect(axisLeft, top, axisLeft + axisWidth, top + height), canvas);
        }

        private void DrawLabel(string text, float left, float top, SKCanvas canvas)
        {
            using (var paint = new SKPaint())
            {
                paint.TextSize = 20f;
                paint.IsAntialias = true;
                paint.Color = (SKColor)0xFFFF0000;
                paint.TextEncoding = SKTextEncoding.Utf32;
                paint.IsStroke = false;

                canvas.DrawText(text, left, top, paint);
            }
        }

        private void DrawRect(SKRect rect, SKCanvas canvas)
        {
            using (var paint = new SKPaint())
            {
                paint.IsAntialias = true;
                paint.Color = (SKColor)0xFFFF0000;

                canvas.DrawRect(rect, paint);
            }
        }



        private SKSize GetMaxLabelSize()
        {
            // TODO
            float maxWidth = 0;
            float maxHeight = 0;
            float lastLabelHeight = 0;

            double i;
            for (i = min; i <= max; i += step)
            {
                var paint = new SKPaint();
                paint.TextSize = fontSize;
                var bounds = new SKRect();
                paint.MeasureText(i.ToString(), ref bounds);
                if (bounds.Width > maxWidth)
                {
                    maxWidth = bounds.Width;
                }

                if (i == min && maxHeight < bounds.Height)
                {
                    maxHeight = bounds.Height;
                }

                lastLabelHeight = bounds.Height;
            }

            if (i > max)
            {
                var paint = new SKPaint();
                paint.TextSize = fontSize;
                var bounds = new SKRect();
                paint.MeasureText(max.ToString(), ref bounds);
                if (bounds.Width > maxWidth)
                {
                    maxWidth = bounds.Width;
                }

                lastLabelHeight = bounds.Height;
            }

            if (maxHeight < lastLabelHeight)
            {
                maxHeight = lastLabelHeight;
            }

            return new SKSize(maxWidth, maxHeight);
        }


    }
}
