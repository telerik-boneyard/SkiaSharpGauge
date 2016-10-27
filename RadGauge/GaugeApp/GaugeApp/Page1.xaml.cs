using RadGauge;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace GaugeApp
{
    public partial class Page1 : ContentPage
    {
        public Page1()
        {
            InitializeComponent();
            this.root.Children.Add(new RadVerticalGauge() { Ranges = new double[] { 0, 33, 66, 100 } });
        }
    }
}
