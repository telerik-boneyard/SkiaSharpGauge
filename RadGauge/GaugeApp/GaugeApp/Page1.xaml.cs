using System;
using RadGauge;
using Xamarin.Forms;

namespace GaugeApp
{
    public partial class Page1 : ContentPage
    {
        static int i;
        RadVerticalGauge gauge;

        public Page1()
        {
            InitializeComponent();

            this.gauge = new RadVerticalGauge()
            {
                Ranges = new double[] { 0, 33, 66, 100 },
                Colors = new Color[] { Color.White, Color.Green, Color.Red},
                Maximum = 100,
                HeightRequest = 300
            };
            this.root.Children.Add(gauge);

            Button increaseMaxButton = new Button() { Text = "Increase Maximum" };
            increaseMaxButton.Clicked += this.IncreaseMaxButton_Clicked;
            this.root.Children.Add(increaseMaxButton);

            Button increaseMinButton = new Button() { Text = "Increase Minimum" };
            increaseMinButton.Clicked += this.IncreaseMinButton_Clicked;
            this.root.Children.Add(increaseMinButton);

            Button increaseStepButton = new Button() { Text = "Increase Step" };
            increaseStepButton.Clicked += this.IncreaseStepButton_Clicked;
            this.root.Children.Add(increaseStepButton);

            var animateButton = new Button { Text = "animate" };
            animateButton.Clicked += Animate;

            this.root.Children.Add(animateButton);
        }

        void Animate(object sender, EventArgs e)
        {
            gauge.AnimateTo(i++ % 2 == 0 ? 85 : 22, 500);
        }

        private void IncreaseMaxButton_Clicked(object sender, EventArgs e)
        {
            gauge.Maximum += 20;
        }

        private void IncreaseMinButton_Clicked(object sender, EventArgs e)
        {
            gauge.Minimum += 20;
        }

        private void IncreaseStepButton_Clicked(object sender, EventArgs e)
        {
            gauge.Step += 2;
        }
    }
}
