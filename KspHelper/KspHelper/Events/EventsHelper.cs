using System;
using Fasterflect;

namespace KspHelper.Events
{
    public static class EventsHelper
    {
        /// <summary>
        /// Replace pre-defined KSPEvent to a new delegate
        /// </summary>
        /// <param name="module">current module</param>
        /// <param name="name">event name ("Activate" for example)</param>
        /// <param name="replaceAction">new action delegate</param>
        public static void ReplaceEvent(this PartModule module, string name, BaseEventDelegate replaceAction)
        {
            UpdateEvent(module, name, replaceAction);
        }

        /// <summary>
        /// Combine pre-defined KSPEvent with a new delegate
        /// </summary>
        /// <param name="module">current module</param>
        /// <param name="name">event name ("Activate" for example)</param>
        /// <param name="replaceAction">new action delegate</param>
        public static void CombineEvent(this PartModule module, string name, BaseEventDelegate replaceAction)
        {
            UpdateEvent(module, name, replaceAction, false);
        }

        private static void UpdateEvent(PartModule module, string name, BaseEventDelegate action, bool replace = true)
        {
            if (!module.Events.Contains(name)) return;

            var oldEvent = module.Events[name];

            var callback = oldEvent.TryGetPropertyValue("onEvent");

            if (callback == null) return;

            BaseEvent baseEvent;

            if (replace)
            {
                baseEvent = new BaseEvent(module.Events, name, action);
            }
            else
            {
                var combinedDelegate = (BaseEventDelegate)Delegate.Combine((Delegate)callback, action);
                baseEvent = new BaseEvent(module.Events, name, combinedDelegate);
            }

            oldEvent.CopyDataTo(baseEvent);

            module.Events.Remove(oldEvent);
            module.Events.Add(baseEvent); 
        }

        /// <summary>
        /// Full copy of KSPEvent data to a new event
        /// </summary>
        /// <param name="source">event source</param>
        /// <param name="dest">event destination</param>
        public static void CopyDataTo(this BaseEvent source, BaseEvent dest)
        {
            dest.active = source.active;
            dest.category = source.category;
            dest.externalToEVAOnly = source.externalToEVAOnly;
            dest.guiActive = source.guiActive;
            dest.guiActiveEditor = source.guiActiveEditor;
            dest.guiActiveUnfocused = source.guiActiveUnfocused;
            dest.guiIcon = source.guiIcon;
            dest.guiName = source.guiName;
            dest.unfocusedRange = source.unfocusedRange;
        }


    }
}
