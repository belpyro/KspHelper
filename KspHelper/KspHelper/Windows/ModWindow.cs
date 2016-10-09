using System;
using KspHelper.Interfaces;
using UnityEngine;
using Random = System.Random;

namespace KspHelper.Windows
{
    public abstract class ModLayoutWindow : ScenarioModule, IGuiBehavior
    {
        private readonly int _id = new Random().Next(123, 99999);
        private Rect _rect = GUIUtil.ScreenCenteredRect(100f, 100f);
        private bool _isVisible;
        private string _title = "";
        private bool _isClamp;

        public override void OnAwake()
        {
            base.OnAwake();
            WindowSettingsAttribute attr = Attribute.GetCustomAttribute(this.GetType(), typeof(WindowSettingsAttribute)) as WindowSettingsAttribute;
            if (attr != null)
            {
                _rect = GUIUtil.ScreenCenteredRect(attr.Width, attr.Height);
                _title = attr.Title;
                _isClamp = attr.ClampToRect;
            }
        }

        public void OnGUI()
        {
            if (!_isVisible) return;
            _rect = GUILayout.Window(_id, _isClamp ? KSPUtil.ClampRectToScreen(_rect) : _rect, DrawWindow, _title, HighLogic.Skin.window);
            GUI.BringWindowToFront(_id);
        }

        private void DrawWindow(int id)
        {
            InternalDraw();
            GUI.DragWindow();
        }

        protected abstract void InternalDraw();

        public void Show()
        {
            _isVisible = true;
        }

        public void Hide()
        {
            _isVisible = true;
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class WindowSettingsAttribute : Attribute
    {
        public WindowSettingsAttribute()
        {
            Width = 100;
            Height = 100;
            ClampToRect = true;
            Title = "";
        }
        public int Width { get; set; }

        public int Height { get; set; }

        public bool ClampToRect { get; set; }

        public string Title { get; set; }
    }
}