using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SystemStabilityAnalysis.Models
{
    public enum TypeRound
    {
        NoRound = 0,
        /// <summary>
        /// Стандартное округление
        /// </summary>
        Standart,
        /// <summary>
        /// Округление в большую сторону
        /// </summary>
        Ceiling
    }
    public static class TypeRoundExtension
    {
        public static double Round(this TypeRound parameter, double value)
        {
            if (parameter == TypeRound.Ceiling)
                return Math.Ceiling(value);


            if (parameter == TypeRound.Standart)
                return Math.Round(value, MidpointRounding.AwayFromZero);

            return value;
        }

    }
}
