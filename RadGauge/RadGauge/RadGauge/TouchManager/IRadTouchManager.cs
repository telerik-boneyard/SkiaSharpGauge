using System;

namespace RadGauge
{
    public interface IRadTouchManager
    {
        void AddTouchDownHandler(Xamarin.Forms.VisualElement xamarinVisual, EventHandler<TouchEventArgs> handler);
        void AddTouchMoveHandler(Xamarin.Forms.VisualElement xamarinVisual, EventHandler<TouchEventArgs> handler);
    }
}
