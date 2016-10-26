using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp;
using Xamarin.Forms;

namespace RadGauge
{
    public class VerticalGaugeRangesRenderer : VerticalGaugePart
    {
        //private double min = 0;
        //private double max = 100;
        //private double step = 20;
        //private double value = 22;
        //private double[] ranges = { 0, 20, 40, 100 };
        //private Color[] ranges = { Color.Green, Color.FromRgb( };

        public override void Render(SKCanvas canvas, SKRect layoutSlot)
        {
            
        }

        internal SKSize Measure(SKSize availableSize)
        {
            return new SKSize(20, 150);
        }
    }
}
