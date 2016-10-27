using RadGauge.UWP;
using System;
using System.Collections.Generic;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Xamarin.Forms.Platform.UWP;

[assembly: Xamarin.Forms.Dependency(typeof(RadTouchManagerUWP))]

namespace RadGauge.UWP
{
    public class RadTouchManagerUWP : IRadTouchManager
    {
        private static readonly DependencyProperty TouchEventsHelperProperty = DependencyProperty.RegisterAttached(
            "TouchEventsHelper",
            typeof(TouchEventsHelper),
            typeof(RadTouchManagerUWP),
            new PropertyMetadata(null));

        public void AddTouchDownHandler(Xamarin.Forms.VisualElement xamarinVisual, EventHandler<TouchEventArgs> handler)
        {
            IVisualElementRenderer renderer = Platform.GetRenderer(xamarinVisual);
            Panel panel = renderer as Panel;
            panel.PointerPressed += this.Panel_PointerPressed;

            TouchEventsHelper helper = GetOrCreateTouchEventsHelper(panel);
            helper.touchDownHandlers.Add(handler);
        }

        public void AddTouchMoveHandler(Xamarin.Forms.VisualElement xamarinVisual, EventHandler<TouchEventArgs> handler)
        {
            IVisualElementRenderer renderer = Platform.GetRenderer(xamarinVisual);
            Panel panel = renderer as Panel;
            panel.PointerMoved += this.Panel_PointerMoved;

            TouchEventsHelper helper = GetOrCreateTouchEventsHelper(panel);
            helper.touchMoveHandlers.Add(handler);
        }

        private static TouchEventsHelper GetOrCreateTouchEventsHelper(DependencyObject obj)
        {
            TouchEventsHelper helper = (TouchEventsHelper)obj.GetValue(TouchEventsHelperProperty);
            if (helper == null)
            {
                helper = new TouchEventsHelper();
                obj.SetValue(TouchEventsHelperProperty, helper);
            }

            return helper;
        }

        private void Panel_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            UIElement uiElement = (UIElement)sender;
            PointerPoint pointer = e.GetCurrentPoint(uiElement);
            TouchEventsHelper helper = GetOrCreateTouchEventsHelper(uiElement);
            TouchEventArgs args = new TouchEventArgs(new Xamarin.Forms.Point(pointer.Position.X, pointer.Position.Y));

            foreach (var handler in helper.touchDownHandlers)
            {
                handler(null, args);
            }
        }

        private void Panel_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            UIElement uiElement = (UIElement)sender;
            PointerPoint pointer = e.GetCurrentPoint(uiElement);
            TouchEventsHelper helper = GetOrCreateTouchEventsHelper(uiElement);
            TouchEventArgs args = new TouchEventArgs(new Xamarin.Forms.Point(pointer.Position.X, pointer.Position.Y));

            foreach (var handler in helper.touchMoveHandlers)
            {
                handler(null, args);
            }
        }

        private class TouchEventsHelper
        {
            internal readonly List<EventHandler<TouchEventArgs>> touchDownHandlers;
            internal readonly List<EventHandler<TouchEventArgs>> touchMoveHandlers;

            internal TouchEventsHelper()
            {
                this.touchDownHandlers = new List<EventHandler<TouchEventArgs>>();
                this.touchMoveHandlers = new List<EventHandler<TouchEventArgs>>();
            }
        }
    }
}