using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SystemStabilityAnalysis.Helpers
{
    public struct DiagramCalculation
    {
        public DiagramCalculation(double _values, string _nameSystem)
        {
            values = _values;
            nameSystem = _nameSystem;
        }

        public string nameSystem { get; set; }

        public double values { get; set; }

        public object ToObject()
        {
            return new
            {
                nameSystem,
                values = values
            };
        }
    }

    public struct DiagramCalculationResult
    {
        public List<DiagramCalculation> calculations { get; set; }
        public string parameterName { get; set; }
    }
}
