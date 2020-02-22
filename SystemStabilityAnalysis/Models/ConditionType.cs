using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SystemStabilityAnalysis.Helpers
{
    public enum ConditionType
    {
        NoCorrect=0,
        More=1,
        Less,
        MoreOrEqual,
        LessOrEqual,
        Equal,
        NotEqual
    }

    public class Condition
    {
        public ConditionType ConditionType { get; }

        public double Value { get; set; }

        public string Description { get { return Designations[ConditionType]; } }

        public bool Comparison(double value)
        {
            return Comparisons[ConditionType](value, Value);
        }

        public static Dictionary<ConditionType, Func<double, double, bool>> Comparisons = new Dictionary<ConditionType, Func<double, double, bool>>()
        {
            {ConditionType.More, (x,y)=>x>y},
            {ConditionType.Less, (x,y)=>x<y},
            {ConditionType.MoreOrEqual,(x,y)=>x>=y },
            {ConditionType.LessOrEqual, (x,y)=>x<=y},
            {ConditionType.Equal, (x,y)=>x==y },
            {ConditionType.NotEqual, (x,y)=>x!=y}
        };

        public static Dictionary<ConditionType, string> Designations = new Dictionary<ConditionType, string>()
        {
            {ConditionType.More, ">" },
            {ConditionType.Less, "<" },
            {ConditionType.MoreOrEqual, ">=" },
            {ConditionType.LessOrEqual, "<="},
            {ConditionType.Equal, "=" },
            {ConditionType.NotEqual, "<>" }
        };
        public Condition(ConditionType conditionType, double value)
        {
            ConditionType = conditionType;
            Value = value;
        }
    }
}
