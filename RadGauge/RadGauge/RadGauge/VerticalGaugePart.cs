using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadGauge
{
    public abstract class VerticalGaugePart
    {
        public abstract void Render(SKCanvas canvas, SKRect layoutSlot);
    }
}
