using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using System.Diagnostics;
using System.Threading;
using System.Diagnostics.Contracts;
using System.Collections.ObjectModel;

namespace RadGauge
{
    public partial class RadVerticalGauge : ContentView
    {
        public static BindableProperty RangesProperty =
            BindableProperty.Create("Ranges", typeof(double[]), typeof(RadVerticalGauge),
                new double[0], BindingMode.OneWay, null, OnRangesPropertyChanged);

        public static BindableProperty MaximumProperty =
            BindableProperty.Create("Maximum", typeof(double), typeof(RadVerticalGauge),
                100d, BindingMode.OneWay, null, OnAxisPropertyChanged);

        public static BindableProperty MinimumProperty =
            BindableProperty.Create("Minimum", typeof(double), typeof(RadVerticalGauge),
                0d, BindingMode.OneWay, null, OnAxisPropertyChanged);

        public static BindableProperty StepProperty =
            BindableProperty.Create("Step", typeof(double), typeof(RadVerticalGauge),
                20d, BindingMode.OneWay, null, OnAxisPropertyChanged);

        private SKSize axisSize;
        private float offset;
        private SKSize rangesSize;
        private SKSize indicatorSize;

        private SKSize sizeCache;

        private double axisLineStart;
        private double axisLineEnd;
        private bool touchEventsEstablished;
        private bool isIndicatorInteractive;

        public event EventHandler AnimationCompleted;

        public RadVerticalGauge()
        {
            InitializeComponent();
            //this.Parts = new List<VerticalGaugePart>();
            this.Axis = new VerticalGaugeAxisRenderer(this);
            this.RangesRenderer = new VerticalGaugeRangesRenderer(this);
            this.Indicator = new VerticalGaugeIndicatorRenderer(this) { WidthRequest = 20, Value = 22, };
            this.isIndicatorInteractive = true;
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

        public double Minimum
        {
            get { return (double)this.GetValue(MinimumProperty); }
            set { this.SetValue(MinimumProperty, value); }
        }

        public double Step
        {
            get { return (double)this.GetValue(StepProperty); }
            set { this.SetValue(StepProperty, value); }
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
            this.axisLineStart = offset / 2;
            this.axisLineEnd = axisSize.Height - (offset / 2);
            this.EstablishTouchEvents();

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

        private static void OnAxisPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            RadVerticalGauge gauge = bindable as RadVerticalGauge;

            gauge.canvas.InvalidateSurface();
        }

        public void AnimateTo(double value)
        {
            AnimateTo(value, 200, 10, Easing.CubicOut);
        }

        public void AnimateTo(double value, int duration)
        {
            AnimateTo(value, duration, 10, Easing.CubicOut);
        }

        public void AnimateTo(double value, int duration, int rate)
        {
            AnimateTo(value, duration, rate, Easing.CubicOut);
        }

        public void AnimateTo(double value, int duration, Easing easing)
        {
            AnimateTo(value, duration, 10, easing);
        }

        public void AnimateTo(double value, int duration, int rate, Easing easing)
        {
            Task.Run(async () =>
            {
                this.currentAnimationId++;
                await AnimateAsync(this.Indicator.Value, value, easing, duration, rate, this.currentAnimationId);
            });
        }

        private async Task AnimateAsync(double startValue, double value, Easing easing, int duration, int rate, int animationId)
        {
            int lastTime = 0;
            int i;
            for (i = rate; i <= duration; i += rate)
            {
                await Task.Delay(rate);

                var time = (double)i / duration;
                var newValue = startValue + easing.Ease(time) * (value - startValue);

                if (!UpdateIndicatorValue(newValue, animationId))
                {
                    return;
                }

                lastTime = i;
            }

            if (lastTime < duration)
            {
                await Task.Delay(duration - lastTime);

                if (!UpdateIndicatorValue(value, animationId))
                {
                    return;
                }
            }

            Device.BeginInvokeOnMainThread(() =>
            {
                this.AnimationCompleted?.Invoke(this, EventArgs.Empty);
            });
        }

        private int currentAnimationId;

        private bool UpdateIndicatorValue(double value, int animationId)
        {
            if (animationId != currentAnimationId)
            {
                return false;
            }

            Device.BeginInvokeOnMainThread(() =>
            {
                this.Indicator.Value = value;
                //// TODO: remove this when indicator value has onchanged method
                canvas.InvalidateSurface();
                ////  
            });

            return true;
        }
        private void EstablishTouchEvents()
        {
            if (this.touchEventsEstablished)
            {
                return;
            }

            try
            {
                RadTouchManager.AddTouchDownHandler(this, this.OnTouchMove);
                RadTouchManager.AddTouchMoveHandler(this, this.OnTouchMove);
                this.touchEventsEstablished = true;
            }
            catch
            {
            }
        }

        private void OnTouchMove(object sender, TouchEventArgs args)
        {
            if (this.isIndicatorInteractive)
            {
                this.Indicator.Value = this.ConvertPointToValue(args.Position);
                this.canvas.InvalidateSurface();
            }
        }

        internal double ConvertPointToValue(Point point)
        {
            int min = 0;

            double position = point.Y;
            double relativePosition = 1 - (position - this.axisLineStart) / (this.axisLineEnd - axisLineStart);
            double value = min + (relativePosition * (this.Maximum - min));
            value = Coerce(value, min, this.Maximum);

            return value;
        }

        private double Coerce(double value, double min, double max)
        {
            return Math.Min(max, Math.Max(min, value));
        }
    }
}