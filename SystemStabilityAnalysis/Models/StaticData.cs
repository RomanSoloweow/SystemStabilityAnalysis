using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemStabilityAnalysis.Helpers;
using SystemStabilityAnalysis.Models.Parameters;

using SystemStabilityAnalysis.Models;
using System.Dynamic;

namespace SystemStabilityAnalysis.Models
{
    public static class StaticData
    {
        public static Dictionary<string, SystemForAnalys> Systems { get; private set; } = new Dictionary<string, SystemForAnalys>();

        public static SystemForAnalys CurrentSystems { get; set; } = new SystemForAnalys("Test");

        public static SystemForAnalys AddSystem(SystemForAnalys systemForAnalys)
        {
            Systems.Add(systemForAnalys.Name, systemForAnalys);

            return systemForAnalys;
        }

        public static Dictionary<NameParameterWithEnter, Condition> ConditionsForParameterWithEnter { get; set; } = new Dictionary<NameParameterWithEnter, Condition>();

        public static Dictionary<NameParameterWithCalculation, Condition> ConditionsForParameterWithCalculation { get; set; } = new Dictionary<NameParameterWithCalculation, Condition>();

        public static Dictionary<NameParameterForAnalysis, Condition> ConditionsForParameterForAnalysis { get; set; } = new Dictionary<NameParameterForAnalysis, Condition>();
    
        public static void SaveCurrentSystem()
        {
            AddSystem(CurrentSystems);
        }

        public static dynamic ChartCalculation { get; set; } = new ExpandoObject();
        public static dynamic DiagramCalculation { get; set; } = new ExpandoObject();
    }
}
