using System.Collections.Generic;
using System.Linq;

namespace KspHelper.Utils
{
    public static class VesselUtils
    {
        public static IEnumerable<T> GetModules<T>(this Vessel vessel) where T : PartModule
        {
            var modules = vessel.Parts.Select(x => x.Modules).ToList();
            return modules.SelectMany(x => x.GetModules<T>()).ToList();
        }

        public static IEnumerable<Part> GetPartsWithModules<T>(this Vessel vessel) where T : PartModule
        {
            return vessel.Parts.Where(x => x.Modules.GetModules<T>().Any()).ToList();
        }
    }
}
