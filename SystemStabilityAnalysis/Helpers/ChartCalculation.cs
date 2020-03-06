using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace SystemStabilityAnalysis.Helpers
{
    public struct ChartCalculation
    {
        public ChartCalculation(List<Coords> _values, string _nameSystem)
        {
            values = _values;
            nameSystem = _nameSystem;
        }
        public List<Coords> values { get; set; }
        public string nameSystem { get; set; }
        public object ToObject()
        {
            return new
            {
                nameSystem,
                values = values.Select(x=>x.ToObject())
            };
        }
    }

    public struct ChartCalculationResult
    {
        public List<ChartCalculation> calculations { get; set; }
        public string parameterNameX { get; set; }
        public string parameterNameY { get; set; }
    }
}
