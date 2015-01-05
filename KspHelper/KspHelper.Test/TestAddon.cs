using KspHelper.Window;
using UnityEngine;

namespace KspHelper.Test
{
    [KSPAddon(KSPAddon.Startup.MainMenu, true), WindowState("Hello", true, true)]
    public class TestWindowAddon : KspWindow
    {
        protected override void OnDraw()
        {
            GUILayout.Label("hohoho");
        }
    }
}
