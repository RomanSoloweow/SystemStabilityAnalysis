using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SystemStabilityAnalysis.Models
{
    public class Properties
    {
        public Dictionary<string, Property> properties { get; protected set; } = new Dictionary<string, Property>();
        public Property AddProperty(Property property)
        {
            properties.Add(property.Name, property);
            return property;
        }
    }
}
