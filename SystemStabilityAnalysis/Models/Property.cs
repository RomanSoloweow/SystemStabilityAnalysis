using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemStabilityAnalysis.Helpers;

namespace SystemStabilityAnalysis.Models
{
    public class Property
    {
        public Property(string Name, UnitType unitType)
        {

        }

        public string Description { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }
        public Unit Unit { get; }      
        
        public Func<double> Calculate { get; set; }

        public bool Verification()
        {

        }
    }
}
