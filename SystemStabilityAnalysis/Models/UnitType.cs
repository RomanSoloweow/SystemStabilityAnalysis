using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace SystemStabilityAnalysis.Helpers
{
    public enum UnitType
    {
        NoCorrect = 0,
        Point,
        ThousandRubles,
        ManHour,
        Shift,
        Hour
    }

    public class Unit
    {
        public UnitType UnitType { get;}
        public string Description { get { return Descriptions[UnitType]; } }
        public static Dictionary<UnitType, string> Descriptions = new Dictionary<UnitType, string>()
        {
            {UnitType.Point, "шт." },
            {UnitType.ThousandRubles, "т. р." },
            {UnitType.ManHour, "чел. ч" },
            {UnitType.Shift, "смена"},
            {UnitType.Hour, "час" }
        };
        public static Unit GetUnit(UnitType unitType)
        {
            return new Unit(unitType);
        }
        private  Unit(UnitType unitType)
        {
            UnitType = unitType;
        }

    }

}
