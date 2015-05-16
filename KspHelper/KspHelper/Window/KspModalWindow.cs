using UnityEngine;

namespace KspHelper.Window
{
    public abstract class KspModalWindow: KspWindow
    {
        protected override void InternalDraw()
        {
            GUI.skin = HighLogic.Skin;

            WindowRect = WindowStyle == null
                ? GUI.ModalWindow(Id, WindowRect, InternalWindowDraw, Title)
                : GUI.ModalWindow(Id, WindowRect, InternalWindowDraw, Title, WindowStyle);
        }

    }
}
