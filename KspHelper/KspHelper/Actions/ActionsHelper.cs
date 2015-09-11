using System;
using System.Linq;
using Fasterflect;

namespace KspHelper.Actions
{
    /// <summary>
    /// 
    /// </summary>
    public static class ActionsHelper
    {
        /// <summary>
        /// Replace any KSP predefined action to custom user action 
        /// </summary>
        /// <param name="module">current part module</param>
        /// <param name="name">action name (Custom01 for example)</param>
        /// <param name="action">link to a new action delegate</param>
        /// <param name="isGuiName"></param>
        public static void ReplaceAction(this PartModule module, string name, BaseActionDelegate action, bool isGuiName = false)
        {
            UpdateCallback(module, name, action, isGuiName);
        }

        /// <summary>
        /// Combine any KSP action with a new delegate
        /// </summary>
        /// <param name="module">current part module</param>
        /// <param name="name">action name (Custom01 for example)</param>
        /// <param name="action">link to a new action delegate</param>
        /// <param name="isGuiName"></param>
        public static void CombineAction(this PartModule module, string name, BaseActionDelegate action, bool isGuiName = false)
        {
            UpdateCallback(module, name, action, false, isGuiName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="module"></param>
        /// <param name="name"></param>
        /// <param name="isGuiName"></param>
        /// <returns></returns>
        public static ActionInfo GetAction(this PartModule module, string name, bool isGuiName = false)
        {
            if (!module.Actions.Any(a => a.name.Equals(name, StringComparison.InvariantCultureIgnoreCase))
                && !module.Actions.Any(a => a.guiName.Equals(name, StringComparison.InvariantCultureIgnoreCase))) return null;

            BaseAction oldAction = isGuiName ? module.Actions.SingleOrDefault(a => a.guiName.Equals(name, StringComparison.InvariantCultureIgnoreCase)) : module.Actions[name];

            var callback = (BaseActionDelegate) oldAction?.TryGetPropertyValue("onEvent", Flags.InstanceAnyDeclaredOnly);

            if (callback == null) return null;

            return new ActionInfo() {Callback = callback, CallbackAction = oldAction};
        }

        private static void UpdateCallback(PartModule module, string name, BaseActionDelegate action,
            bool replace = true, bool isGuiName = false)
        {
            ActionInfo info = module.GetAction(name, isGuiName);

            if (info == null) return;

            BaseAction newAction;

            if (replace)
            {
                newAction = new BaseAction(module.Actions, name, action,
                    new KSPAction(info.CallbackAction.guiName, info.CallbackAction.actionGroup));
            }
            else
            {
                var combined = (BaseActionDelegate) Delegate.Combine(info.Callback, action);
                newAction = new BaseAction(module.Actions, name, combined,
                    new KSPAction(info.CallbackAction.guiName, info.CallbackAction.actionGroup));
            }

            module.Actions.Remove(info.CallbackAction);
            module.Actions.Add(newAction);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ActionInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public BaseActionDelegate Callback { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public BaseAction CallbackAction { get; set; }
    }
}