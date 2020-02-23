using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SystemStabilityAnalysis.Models
{
    public class SystemForAnalys: Properties
    {
        public SystemForAnalys(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}
