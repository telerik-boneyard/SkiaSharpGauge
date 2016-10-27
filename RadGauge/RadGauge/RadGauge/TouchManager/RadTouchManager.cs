using System;
using Xamarin.Forms;

namespace RadGauge
{
    public static class RadTouchManager
    {
        public static void AddTouchDownHandler(Xamarin.Forms.VisualElement xamarinVisual, EventHandler<TouchEventArgs> handler)
        {
            IRadTouchManager touchManager = DependencyService.Get<IRadTouchManager>();
            if (touchManager != null)
            {
                touchManager.AddTouchDownHandler(xamarinVisual, handler);
            }
        }
        public static void AddTouchMoveHandler(Xamarin.Forms.VisualElement xamarinVisual, EventHandler<TouchEventArgs> handler)
        {
            IRadTouchManager touchManager = DependencyService.Get<IRadTouchManager>();
            touchManager.AddTouchMoveHandler(xamarinVisual, handler);
        }
    }
}
