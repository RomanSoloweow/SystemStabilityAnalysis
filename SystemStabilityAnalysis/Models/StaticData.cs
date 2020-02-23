using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemStabilityAnalysis.Helpers;

namespace SystemStabilityAnalysis.Models
{
    public static class StaticData
    {
        public static PropertiesSystem PropertiesSystem { get; private set; } = new PropertiesSystem();

        public static Dictionary<string, SystemForAnalys> Systems { get; private set; } = new Dictionary<string, SystemForAnalys>();

        public static SystemForAnalys AddSystem(SystemForAnalys systemForAnalys)
        {
            Systems.Add(systemForAnalys.Name, systemForAnalys);

            return systemForAnalys;
        }

        public static Dictionary<ParametersName, List<Condition>> Conditions { get; set; } = new Dictionary<ParametersName, List<Condition>>();

    }
}
