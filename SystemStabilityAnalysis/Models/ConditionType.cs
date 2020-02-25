using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SystemStabilityAnalysis.Helpers
{
    public enum ConditionType
    {
        NoCorrect= 0,
        /// <summary>
        /// >
        /// </summary>
        More,
        /// <summary>
        /// <
        /// </summary>
        Less,
        /// <summary>
        /// >=
        /// </summary>
        MoreOrEqual,
        /// <summary>
        /// <=
        /// </summary>
        LessOrEqual,
        /// <summary>
        /// =
        /// </summary>
        Equal,
        /// <summary>
        /// <>
        /// </summary>
        NotEqual
    }

    public static class ConditionTypeExtension
    {
        public static Dictionary<ConditionType, string> Designations = new Dictionary<ConditionType, string>()
        {
            {ConditionType.More, ">" },
            {ConditionType.Less, "<" },
            {ConditionType.MoreOrEqual, ">=" },
            {ConditionType.LessOrEqual, "<="},
            {ConditionType.Equal, "=" },
            {ConditionType.NotEqual, "<>" }
        };

        public static Dictionary<ConditionType, Func<double, double, bool>> Comparisons = new Dictionary<ConditionType, Func<double, double, bool>>()
        {
            {ConditionType.More, (x,y)=>x>y},
            {ConditionType.Less, (x,y)=>x<y},
            {ConditionType.MoreOrEqual,(x,y)=>x>=y },
            {ConditionType.LessOrEqual, (x,y)=>x<=y},
            {ConditionType.Equal, (x,y)=>x==y },
            {ConditionType.NotEqual, (x,y)=>x!=y}
        };

        public static bool InvokeComparison(this ConditionType parameter, double leftValue, double rightValue)
        {
            if (Comparisons.TryGetValue(parameter, out Func<double, double, bool> comparison))
            {
                return comparison.Invoke(leftValue, rightValue);
            }

            throw new ArgumentException(paramName: parameter.ToString(), message: String.Format("Метод сравнения для параметра {0} не найден", parameter.ToString()));
        }

        public static string GetDesignation(this ConditionType parameter)
        {
            if (Designations.TryGetValue(parameter, out string designation))
            {
                return designation;
            }

            throw new ArgumentException(paramName: parameter.ToString(), message:String.Format("Обозначение для параметра {0} не найдено", parameter.ToString()));
        }

        public static string GetName(this ConditionType parameter)
        {
            return Enum.GetName(typeof(ConditionType), parameter);
        }

        public static object ToJson(this ConditionType parameter)
        {
            return new
            {
                Name = parameter.GetDesignation(),
                Value = parameter.GetName()
            };
        }
    }

    public class Condition
    {
        public ConditionType ConditionType { get; }

        public double Value { get; set; }

        public string Name { get { return ConditionType.GetName(); } }

        public string Description { get { return ConditionType.GetDesignation();} }

        [JsonIgnore]
        public string ErrorMessage{ get { return string.Format("{0} {1}", Description, Value.ToString());} }

        public Condition(ConditionType conditionType, double value)
        {
            ConditionType = conditionType;
            Value = value;
        }

        public bool InvokeComparison(double rightValue)
        {
            return ConditionType.InvokeComparison(rightValue, Value);
        }
    }
}
