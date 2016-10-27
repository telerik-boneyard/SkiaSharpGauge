using System;
using Xamarin.Forms;

namespace RadGauge
{
    public class TouchEventArgs : EventArgs
    {
        public readonly Point Position;

        public TouchEventArgs(Point position)
        {
            this.Position = position;
        }
    }
}
