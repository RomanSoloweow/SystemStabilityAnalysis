using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SystemStabilityAnalysis.Helpers
{
    public struct DiagramCalculation
    {
        public DiagramCalculation(double _value, string _nameSystem)
        {
            value = _value;
            nameSystem = _nameSystem;
        }

        public string nameSystem { get; set; }

        public double value { get; set; }

        public object ToObject()
        {
            return new
            {
                nameSystem,
                value = value
            };
        }
    }

    public struct DiagramCalculationResult
    {
        public List<DiagramCalculation> calculations { get; set; }
        public string parameterName { get; set; }
    }
}
