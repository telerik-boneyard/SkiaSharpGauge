using System;
using RadGauge;
using Xamarin.Forms;

namespace GaugeApp
{
    public partial class Page2 : ContentPage
    {
        public Page2()
        {
            InitializeComponent();

            RadVerticalGauge gauge = new RadVerticalGauge()
            {
                Ranges = new double[] { 0, 45, 60, 70 },
                Colors = new Color[] { Color.Green, Color.Lime, Color.Red },
                Minimum = 30,
                Maximum = 70,
                Step = 10,
                IndicatorValue = 40
            };

            gauge.SetValue(Grid.RowProperty, 1);
            gauge.SetValue(Grid.ColumnProperty, 0);
            gauge.SetValue(RadVerticalGauge.MarginProperty, new Thickness(10));
            this.root.Children.Add(gauge);

            gauge = new RadVerticalGauge()
            {
                Ranges = new double[] { 0, 45, 60, 70 },
                Colors = new Color[] { Color.Green, Color.Lime, Color.Red },
                Minimum = 30,
                Maximum = 70,
                Step = 10,
                IndicatorValue = 56
            };

            gauge.SetValue(Grid.RowProperty, 1);
            gauge.SetValue(Grid.ColumnProperty, 1);
            gauge.SetValue(RadVerticalGauge.MarginProperty, new Thickness(10));
            this.root.Children.Add(gauge);

            gauge = new RadVerticalGauge()
            {
                Ranges = new double[] { 0, 45, 60, 70 },
                Colors = new Color[] { Color.Green, Color.Lime, Color.Red },
                Minimum = 30,
                Maximum = 70,
                Step = 10,
                IndicatorValue = 63
            };

            gauge.SetValue(Grid.RowProperty, 1);
            gauge.SetValue(Grid.ColumnProperty, 2);
            gauge.SetValue(RadVerticalGauge.MarginProperty, new Thickness(10));
            this.root.Children.Add(gauge);
        }
    }
}
