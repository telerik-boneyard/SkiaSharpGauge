using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadGauge
{
    public interface IGaugePartRenderer
    {
        void Render(SKCanvas canvas, SKRect layoutSlot);
    }
}
