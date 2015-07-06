using System;
using System.Linq;
using Fasterflect;

namespace KspHelper.Actions
{
    public static class ActionsHelper
    {
        /// <summary>
        /// Replace any KSP predefined action to custom user action 
        /// </summary>
        /// <param name="module">current part module</param>
        /// <param name="name">action name (Custom01 for example)</param>
        /// <param name="action">link to a new action delegate</param>
        public static void ReplaceAction(this PartModule module, string name, BaseActionDelegate action)
        {
            UpdateCallback(module, name, action);
        }

        /// <summary>
        /// Combine any KSP action with a new delegate
        /// </summary>
        /// <param name="module">current part module</param>
        /// <param name="name">action name (Custom01 for example)</param>
        /// <param name="action">link to a new action delegate</param>
        public static void CombineAction(this PartModule module, string name, BaseActionDelegate action)
        {
            UpdateCallback(module, name, action, false);
        }

        private static void UpdateCallback(PartModule module, string name, BaseActionDelegate action,
            bool replace = true)
        {
            if (!module.Actions.Any(a => a.name.Equals(name, StringComparison.InvariantCultureIgnoreCase))) return;

            BaseAction oldAction = module.Actions[name];

            var callback = (BaseActionDelegate) oldAction.TryGetPropertyValue("onEvent", Flags.InstanceAnyDeclaredOnly);

            if (callback == null) return;

            BaseAction newAction;

            if (replace)
            {
                newAction = new BaseAction(module.Actions, name, action,
                    new KSPAction(oldAction.guiName, oldAction.actionGroup));
            }
            else
            {
                var combined = (BaseActionDelegate) Delegate.Combine(callback, action);
                newAction = new BaseAction(module.Actions, name, combined,
                    new KSPAction(oldAction.guiName, oldAction.actionGroup));
            }

            module.Actions.Remove(oldAction);
            module.Actions.Add(newAction);
        }
    }
}