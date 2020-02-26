using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemStabilityAnalysis.Models.Parameters;
namespace SystemStabilityAnalysis.Models
{
    public class Restriction
    {
        public Restriction()
        {

        }
        public Restriction(string parameterName, ConditionType condition, double value)
        {
            ParameterName = parameterName;
            Condition = condition;
            Value = value;
        }
        public Restriction(string parameterName, Condition condition)
        {
            ParameterName = parameterName;
            Condition = condition.ConditionType;
            Value = condition.Value;
        }
        public string ParameterName { get; set; }
        public ConditionType Condition { get; set; }
        public double Value { get; set; }

        public static List<Restriction> GetRestctions()
        {
            List<Restriction> restrictions = new List<Restriction>();

            foreach (var condition in StaticData.ConditionsForParameterWithEnter)
            {
                restrictions.Add(new Restriction(condition.Key.GetName(), condition.Value));
            }

            foreach (var condition in StaticData.ConditionsForParameterWithCalculation)
            {
                restrictions.Add(new Restriction(condition.Key.GetName(), condition.Value));
            }

            foreach (var condition in StaticData.ConditionsForParameterForAnalysis)
            {
                restrictions.Add(new Restriction(condition.Key.GetName(), condition.Value));
            }
            return restrictions;
        }
    }
}
