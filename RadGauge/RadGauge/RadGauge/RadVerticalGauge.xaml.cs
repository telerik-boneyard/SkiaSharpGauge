using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace RadGauge
{
    public partial class RadVerticalGauge : ContentView
    {
        private SKSize axisSize;
        private double offset;
        private SKSize rangesSize;
        private SKSize indicatorSize;

        public RadVerticalGauge()
        {
            InitializeComponent();
            //this.Parts = new List<VerticalGaugePart>();
            this.Axis = new VerticalGaugeAxis();
            this.RangesRenderer = new VerticalGaugeRangesRenderer();
            this.Indicator = new VerticalGaugeIndicator() { WidthRequest = 20, Value = 22, };
        }

        internal VerticalGaugeAxis Axis
        {
            get;
            set;
        }

        internal VerticalGaugeRangesRenderer RangesRenderer
        {
            get;
            set;
        }

        internal VerticalGaugeIndicator Indicator
        {
            get;
            set;
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            if (width == -1) return;

            this.Measure(new SKSize((float)width, (float)height));
        }

        private void Measure(SKSize availableSize)
        {
            axisSize = this.Axis.Measure(availableSize);
            offset = this.Axis.GetOffset();
            rangesSize = this.RangesRenderer.Measure(availableSize);
            indicatorSize = this.Indicator.Measure(availableSize);
        }

        private void Render(SKCanvas canvas)
        {
            this.Axis.Render(canvas, new SKRect() { Top = 0, Left = 0, Size = axisSize });
            this.RangesRenderer.Render(canvas, new SKRect() { Left = rangesSize.Width, Top = (float)offset, Size = rangesSize });
            this.Indicator.Render(canvas, new SKRect() { Left = indicatorSize.Width + indicatorSize.Width, Top = (float)offset, Size = indicatorSize });
        }

        private void OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;
            canvas.Clear(Color.Gray.ToSKColor());

            this.Render(canvas);
        }
    }
}
