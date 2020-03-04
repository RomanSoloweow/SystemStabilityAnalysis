using System;
using System.Collections.Generic;
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
        public List<Coords> values;
        public string nameSystem;
    }
}
