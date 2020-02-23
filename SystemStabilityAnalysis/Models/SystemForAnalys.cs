using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SystemStabilityAnalysis.Models
{
    public class SystemForAnalys: PropertiesSystem
    {
        public SystemForAnalys(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}
