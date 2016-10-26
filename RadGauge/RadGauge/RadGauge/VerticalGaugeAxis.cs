using System;
using SkiaSharp;

namespace RadGauge
{
    public class VerticalGaugeAxis : VerticalGaugePart
    {
        double min = 0;
        double max = 100;
        double step = 10;


        public VerticalGaugeAxis()
        {

        }

        public SKRect Measure()
        {
            throw new NotImplementedException();
        }

        public double GetOffset()
        {
            throw new NotImplementedException();
        }


        public override void Render(SKCanvas canvas, SKRect layoutSlot)
        {

        }

        internal SKRect Measure(SKSize availableSize)
        {
            throw new NotImplementedException();
        }
    }
}
