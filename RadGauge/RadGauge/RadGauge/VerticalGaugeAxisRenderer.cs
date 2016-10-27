using SkiaSharp;

namespace RadGauge
{
    public class VerticalGaugeAxisRenderer : IGaugePartRenderer
    {
        double min = 0;
        double max = 100;
        double step = 10;
        float fontSize = 20f;
        float axisWidth = 1f;
        float tickLength = 5f;
        float tickThickness = 1f;
        SKColor axisColor = (SKColor)0xFF000000;
        SKColor tickColor = (SKColor)0xFF000000;
        SKColor labelColor = (SKColor)0xFF000000;
        float labelsOffset = 5f;
        float tickOffset = 0f;

        SKSize maxLabelSize;

        public SKSize MaxLabelSize
        {
            get { return maxLabelSize; }
        }

        public SKSize Measure(SKSize rect)
        {
            this.maxLabelSize = GetMaxLabelSize();
            var width = tickOffset + labelsOffset + maxLabelSize.Width + axisWidth + tickLength + 1;

            return new SKSize(width, rect.Height);
        }

        public void Render(SKCanvas canvas, SKRect layoutSlot)
        {
            float top = maxLabelSize.Height / 2;
            float left = layoutSlot.Left;

            var height = layoutSlot.Height - maxLabelSize.Height;

            var axisLayoutSlot = new SKRect(left, top, left + layoutSlot.Width, top + height);
            var tickLeft = labelsOffset + left + maxLabelSize.Width;

            for (var i = min; i <= max; i += step)
            {
                var position = VerticalGaugeIndicatorRenderer.GetTickPosition(i, min, max, axisLayoutSlot);
                DrawLabel(i.ToString(), left, position, labelColor, canvas);

                var tickTop = position;
                var tickRect = new SKRect(tickLeft, tickTop, tickLeft + tickLength, tickTop + tickThickness);
                DrawRect(tickRect, tickColor, canvas);
            }

            var axisLeft = tickOffset + tickLeft + tickLength;

            DrawRect(new SKRect(axisLeft, top, axisLeft + axisWidth, top + height), axisColor, canvas);
        }

        private void DrawLabel(string text, float x, float y, SKColor color, SKCanvas canvas)
        {
            using (var paint = new SKPaint())
            {
                paint.TextSize = fontSize;
                paint.IsAntialias = true;
                paint.Color = color;
                paint.TextEncoding = SKTextEncoding.Utf32;
                paint.IsStroke = false;

                SKRect bounds;
                paint.MeasureText(text, ref bounds);

                canvas.DrawText(text, x + maxLabelSize.Width - bounds.Width, y - bounds.Top / 2, paint);
            }
        }

        private void DrawRect(SKRect rect, SKColor color, SKCanvas canvas)
        {
            using (var paint = new SKPaint())
            {
                paint.IsAntialias = true;
                paint.Color = color;

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
