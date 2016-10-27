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
        RadVerticalGauge gauge;
        public Page1()
        {
            InitializeComponent();
            this.gauge = new RadVerticalGauge() { Ranges = new double[] { 0, 33, 66, 100 }, Maximum = 100, HeightRequest = 300 };
            this.root.Children.Add(gauge);

            Button increaseButton = new Button() { Text = "Increase" };
            increaseButton.Clicked += IncreaseButton_Clicked;
            this.root.Children.Add(increaseButton);
        }

        private void IncreaseButton_Clicked(object sender, EventArgs e)
        {
            gauge.Maximum += 20;
        
    }
    }
}
