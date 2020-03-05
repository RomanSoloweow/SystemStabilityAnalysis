using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SystemStabilityAnalysis.Helpers
{
    public struct Coords
    {
        public Coords(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double X { get; set; }
        public double Y { get; set; }

        public object ToObject()
        {
            return new
            {
                X,
                Y
            };
        }
    }
}
