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
        public static BindableProperty RangesProperty =
            BindableProperty.Create("Ranges", typeof(double[]), typeof(RadVerticalGauge),
                new double[0], BindingMode.OneWay, null, OnRangesPropertyChanged);

        public static BindableProperty MaximumProperty =
            BindableProperty.Create("Maximum", typeof(double), typeof(RadVerticalGauge),
                0d, BindingMode.OneWay, null, OnMaximumPropertyChanged);

        private SKSize axisSize;
        private float offset;
        private SKSize rangesSize;
        private SKSize indicatorSize;

        private SKSize sizeCache;

        public RadVerticalGauge()
        {
            InitializeComponent();
            //this.Parts = new List<VerticalGaugePart>();
            this.Axis = new VerticalGaugeAxisRenderer();
            this.RangesRenderer = new VerticalGaugeRangesRenderer();
            this.Indicator = new VerticalGaugeIndicatorRenderer() { WidthRequest = 20, Value = 22, };
        }

        public double[] Ranges
        {
            get { return (double[])this.GetValue(RangesProperty); }
            set { this.SetValue(RangesProperty, value); }
        }

        public double Maximum
        {
            get { return (double)this.GetValue(MaximumProperty); }
            set { this.SetValue(MaximumProperty, value); }
        }

        internal VerticalGaugeAxisRenderer Axis
        {
            get;
            set;
        }

        internal VerticalGaugeRangesRenderer RangesRenderer
        {
            get;
            set;
        }

        internal VerticalGaugeIndicatorRenderer Indicator
        {
            get;
            set;
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            if (width == -1) return;

            this.sizeCache = new SKSize((float)width, (float)height);
        }

        private void Measure(SKSize availableSize)
        {
            axisSize = this.Axis.Measure(availableSize);
            offset = this.Axis.MaxLabelSize.Height;

            var inflatedSize = new SKSize(availableSize.Width, availableSize.Height - offset);

            rangesSize = this.RangesRenderer.Measure(inflatedSize);
            indicatorSize = this.Indicator.Measure(inflatedSize);
        }

        private void Render(SKCanvas canvas)
        {
            this.Axis.Render(canvas, new SKRect() { Top = 0, Left = 0, Size = axisSize });
            this.RangesRenderer.Render(canvas, new SKRect() { Left = axisSize.Width, Top = offset / 2, Size = rangesSize });
            this.Indicator.Render(canvas, new SKRect() { Left = axisSize.Width + rangesSize.Width, Top = offset / 2, Size = indicatorSize });
        }

        private void OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            this.Measure(this.sizeCache);
            var canvas = e.Surface.Canvas;
            canvas.Clear(Color.Gray.ToSKColor());

            this.Render(canvas);
        }

        private static void OnRangesPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            RadVerticalGauge gauge = bindable as RadVerticalGauge;
            gauge.RangesRenderer.Ranges = gauge.Ranges;
            gauge.InvalidateLayout();
        }

        private static void OnMaximumPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            RadVerticalGauge gauge = bindable as RadVerticalGauge;
            gauge.Axis.Maximum = gauge.Maximum;
            
            gauge.canvas.InvalidateSurface();
        }

        
    }
}
