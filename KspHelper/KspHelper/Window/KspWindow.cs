using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fasterflect;
using KspHelper.Behavior;
using UnityEngine;
using Random = UnityEngine.Random;

namespace KspHelper.Window
{
    public abstract class KspWindow : KspBehavior
    {
        private bool _visible;

        protected override void Awake()
        {
            base.Awake();

            Id = Random.Range(100000, 900000) + GetHashCode();

            var attrValue = this.GetType().Attribute<WindowStateAttribute>();

            if (attrValue != null)
            {
                Title = attrValue.Title;
                WindowRect = attrValue.InitialRect;
                ClampToScreen = attrValue.ClampToScreen;
                Visible = attrValue.IsVisible;
                IsDragEnabled = attrValue.DragEnabled;
            }
            else
            {
                Visible = true;
                IsDragEnabled = true;
            }
        }

        public int Id { get; private set; }

        public Rect WindowRect { get; set; }

        public GUIStyle WindowStyle { get; set; }

        public bool Visible
        {
            get { return _visible; }
            set
            {
                _visible = value;
                if (value)
                {
                    RenderingManager.AddToPostDrawQueue(0, InternalDraw);
                }
                else
                {
                    RenderingManager.RemoveFromPostDrawQueue(0, InternalDraw);
                }
            }
        }

        public bool ClampToScreen { get; set; }

        public string Title { get; set; }

        public bool IsDragEnabled { get; set; }

        protected abstract void OnDraw();

        void InternalDraw()
        {
            GUI.skin = HighLogic.Skin;

            WindowRect = WindowStyle == null ? GUILayout.Window(Id, WindowRect, InternalWindowDraw, Title) : GUILayout.Window(Id, WindowRect, InternalWindowDraw, Title, WindowStyle);
        }

        private void InternalWindowDraw(int id)
        {
            OnDraw();

            if (IsDragEnabled)
                GUI.DragWindow();
        }
    }
}
