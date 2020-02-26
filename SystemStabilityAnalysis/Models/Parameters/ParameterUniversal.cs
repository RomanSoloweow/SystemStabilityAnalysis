using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemStabilityAnalysis.Helpers;

namespace SystemStabilityAnalysis.Models.Parameters
{
    public static class ParameterUniversal
    {
        public static bool DeleteFromRestrictions(string name, out bool correct)
        {
            correct = true;
            if (Enum.TryParse(name, out NameParameterWithEnter parameterWithEnter))
            {
                return parameterWithEnter.DeleteFromRestrictions();
            }
            else if (Enum.TryParse(name, out NameParameterWithCalculation parameterWithCalculation))
            {
                return parameterWithCalculation.DeleteFromRestrictions();
            }
            else if (Enum.TryParse(name, out NameParameterForAnalysis parameterForAnalysis))
            {
                return parameterForAnalysis.DeleteFromRestrictions();
            }
            correct = false;
            return false;
        }
        public static void DeleteAllRestriction()
        {
            StaticData.ConditionsForParameterWithCalculation.Clear();
            StaticData.ConditionsForParameterForAnalysis.Clear();
            StaticData.ConditionsForParameterWithEnter.Clear();
        }

        public static object AddToRestriction(string name, ConditionType conditionType, double value, out bool correct)
        {
            correct = true;
            if (Enum.TryParse(name, out NameParameterWithEnter parameterWithEnter))
            {
                parameterWithEnter.AddToRestrictions(conditionType, value);
                return parameterWithEnter.ToRestriction(conditionType, value);
            }
            else if (Enum.TryParse(name, out NameParameterWithCalculation parameterWithCalculation))
            {
                parameterWithCalculation.AddToRestrictions(conditionType, value);
                return parameterWithCalculation.ToRestriction(conditionType, value);
            }
            else if (Enum.TryParse(name, out NameParameterForAnalysis parameterForAnalysis))
            {
                parameterForAnalysis.AddToRestrictions(conditionType, value);
                return parameterForAnalysis.ToRestriction(conditionType, value);
            }
            correct = false;
            return null;
        }
    }
}
