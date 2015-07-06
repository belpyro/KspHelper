using System.Collections.Generic;
using System.Linq;

namespace KspHelper.Utils
{
    public static class VesselUtils
    {
        /// <summary>
        ///  Get all modules from vessel by type
        /// </summary>
        /// <typeparam name="T">type of module(ModuleEngines ex.)</typeparam>
        /// <param name="vessel">current vessel</param>
        /// <returns></returns>
        public static IEnumerable<T> GetModules<T>(this Vessel vessel) where T : PartModule
        {
            var modules = vessel.Parts.Select(x => x.Modules).ToList();
            return modules.SelectMany(x => x.GetModules<T>()).ToList();
        }

        /// <summary>
        /// Return all parts which contains module by type
        /// </summary>
        /// <typeparam name="T">type of module (ModuleEngines ex.)</typeparam>
        /// <param name="vessel">current vessel</param>
        /// <returns></returns>
        public static IEnumerable<Part> GetPartsWithModules<T>(this Vessel vessel) where T : PartModule
        {
            return vessel.Parts.Where(x => x.Modules.GetModules<T>().Any()).ToList();
        }
    }
}
