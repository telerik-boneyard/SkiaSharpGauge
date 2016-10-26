using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp;

namespace RadGauge
{
    public class VerticalGaugeIndicator : VerticalGaugePart
    {
        public override void Render(SKCanvas canvas, SKRect layoutSlot)
        {
            
        }

        internal SKSize Measure(SKSize availableSize)
        {
            return new SKSize(20, 150);
        }
    }
}
