using System;
using System.Linq;
using Fasterflect;

namespace KspHelper.Events
{
    /// <summary>
    /// Helper for managing KspEvents
    /// </summary>
    public static class EventsHelper
    {
        /// <summary>
        /// Replace pre-defined KSPEvent to a new delegate
        /// </summary>
        /// <param name="module">current module</param>
        /// <param name="name">event name ("Activate" for example)</param>
        /// <param name="replaceAction">new action delegate</param>
        /// <param name="isGuiName">name is guiName of event</param>
        public static void ReplaceEvent(this PartModule module, string name, BaseEventDelegate replaceAction, bool isGuiName = false)
        {
            UpdateEvent(module, name, replaceAction, isGuiName);
        }

        /// <summary>
        /// Combine pre-defined KSPEvent with a new delegate
        /// </summary>
        /// <param name="module">current module</param>
        /// <param name="name">event name ("Activate" for example)</param>
        /// <param name="replaceAction">new action delegate</param>
        /// <param name="isGuiName"></param>
        public static void CombineEvent(this PartModule module, string name, BaseEventDelegate replaceAction, bool isGuiName = false)
        {
            UpdateEvent(module, name, replaceAction, false, isGuiName);
        }

        private static void UpdateEvent(PartModule module, string name, BaseEventDelegate action, bool replace = true, bool isGuiName = false)
        {
            EventInfo info = GetEvent(module, name, isGuiName);

            if (info == null) return;

            BaseEvent baseEvent;

            if (replace)
            {
                baseEvent = new BaseEvent(module.Events, name, action);
            }
            else
            {
                var combinedDelegate = (BaseEventDelegate)Delegate.Combine((Delegate)info.CallBack, action);
                baseEvent = new BaseEvent(module.Events, name, combinedDelegate);
            }

            info.CallbackEvent.MapFields(baseEvent);

            module.Events.Remove(info.CallbackEvent);
            module.Events.Add(baseEvent);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="module"></param>
        /// <param name="name"></param>
        /// <param name="isGuiName"></param>
        /// <returns></returns>
        public static EventInfo GetEvent(this PartModule module, string name, bool isGuiName = false)
        {
            if (!module.Events.Contains(name) && !module.Events.Any(e => e.GUIName.Equals(name, StringComparison.InvariantCultureIgnoreCase))) return null;

            var oldEvent = isGuiName ? module.Events.SingleOrDefault(e => e.GUIName.Equals(name, StringComparison.InvariantCultureIgnoreCase)) : module.Events[name];

            var callback = oldEvent?.TryGetPropertyValue("onEvent") as BaseEventDelegate;

            if (callback == null) return null;

            return new EventInfo()
            {
                CallBack = callback,
                CallbackEvent = oldEvent
            };
        }


    }
    public class EventInfo
    {
        public BaseEventDelegate CallBack { get; set; }

        public BaseEvent CallbackEvent { get; set; }
    }
}
