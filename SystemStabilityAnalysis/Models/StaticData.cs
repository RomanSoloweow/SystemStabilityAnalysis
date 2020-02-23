using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemStabilityAnalysis.Helpers;

namespace SystemStabilityAnalysis.Models
{
    public static class StaticData
    {

        public static Dictionary<string, SystemForAnalys> Systems { get; private set; } = new Dictionary<string, SystemForAnalys>();

        public static SystemForAnalys AddSystem(SystemForAnalys systemForAnalys)
        {
            Systems.Add(systemForAnalys.Name, systemForAnalys);

            return systemForAnalys;
        }

        public static Dictionary<ParameterName, List<Condition>> Conditions { get; set; } = new Dictionary<ParameterName, List<Condition>>();

    }
}
