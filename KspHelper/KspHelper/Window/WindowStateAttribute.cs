using System;
using UnityEngine;

namespace KspHelper.Window
{
    [AttributeUsage(AttributeTargets.Class)]
    public class WindowStateAttribute : Attribute
    {
        public string Title { get; private set; }

        public Rect InitialRect { get; private set; }

        public bool ClampToScreen { get; private set; }

        public bool IsVisible { get; private set; }

        public bool DragEnabled { get; private set; }

        public WindowStateAttribute(string title, bool initialRect, bool clampToScreen, bool isVisible = true, bool dragEnabled = true)
        {
            Title = title;
            if (initialRect)
            {
                InitialRect = new Rect(0, 0, 200, 100);
            }
            ClampToScreen = clampToScreen;
            IsVisible = isVisible;
            DragEnabled = dragEnabled;
        }
    }
}
