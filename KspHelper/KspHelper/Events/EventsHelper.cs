using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fasterflect;

namespace KspHelper.Events
{
    public static class EventsHelper
    {
        public static void ReplaceEvent(this PartModule module, string name, BaseEventDelegate replaceAction)
        {
            if (!module.Events.Contains(name)) return;

            var oldEvent = module.Events[name];

            var callback = oldEvent.TryGetPropertyValue("onEvent");

            if (callback == null) return;

            var baseEvent = new BaseEvent(module.Events, name, replaceAction);
            
            oldEvent.CopyEventData(baseEvent);

            module.Events.Remove(oldEvent);
            module.Events.Add(baseEvent);
        }

        public static void ReplaceDataEvent(this PartModule module, string name, BaseEventDataDelegate replaceAction)
        {
            if (!module.Events.Contains(name)) return;

            var oldEvent = module.Events[name];

            var callback = oldEvent.TryGetPropertyValue("onDataEvent");

            if (callback == null) return;

            var baseEvent = new BaseEvent(module.Events, name, replaceAction);

            oldEvent.CopyEventData(baseEvent);

            module.Events.Remove(oldEvent);
            module.Events.Add(baseEvent);
        }

        public static void CombineEvent(this PartModule module, string name, BaseEventDelegate replaceAction)
        {
            if (!module.Events.Contains(name)) return;

            var oldEvent = module.Events[name];

            var callback = oldEvent.TryGetPropertyValue("onEvent");

            if (callback == null) return;

            var combinedDelegate = (BaseEventDelegate)Delegate.Combine((Delegate)callback, replaceAction);

            var baseEvent = new BaseEvent(module.Events, name, combinedDelegate);

            oldEvent.CopyEventData(baseEvent);

            module.Events.Remove(oldEvent);
            module.Events.Add(baseEvent);
        }

        public static void CombineDataEvent(this PartModule module, string name, BaseEventDataDelegate replaceAction)
        {
            if (!module.Events.Contains(name)) return;

            var oldEvent = module.Events[name];

            var callback = oldEvent.TryGetPropertyValue("onDataEvent");

            if (callback == null) return;

            var combinedDelegate = (BaseEventDataDelegate)Delegate.Combine((Delegate)callback, replaceAction);

            var baseEvent = new BaseEvent(module.Events, name, combinedDelegate);

            oldEvent.CopyEventData(baseEvent);

            module.Events.Remove(oldEvent);
            module.Events.Add(baseEvent);
        }

        public static void CopyEventData(this BaseEvent source, BaseEvent dest)
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
