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

        public static object AddToRestriction(string name, ConditionType conditionType, double value, bool addToCondition, out bool correct)
        {
            correct = true;
            if (Enum.TryParse(name, out NameParameterWithEnter parameterWithEnter))
            {
                if ((addToCondition)&&(!parameterWithEnter.AddedToRestrictions()))
                {
                    parameterWithEnter.AddToRestrictions(conditionType, value);
                    return parameterWithEnter.ToRestriction(conditionType, value);                
                }
            }
            else if (Enum.TryParse(name, out NameParameterWithCalculation parameterWithCalculation))
            {
                if (addToCondition)
                {
                    if ((addToCondition) && (!parameterWithEnter.AddedToRestrictions()))
                    {
                        parameterWithCalculation.AddToRestrictions(conditionType, value);
                        return parameterWithCalculation.ToRestriction(conditionType, value);
                    }
                }
            }
            else if (Enum.TryParse(name, out NameParameterForAnalysis parameterForAnalysis))
            {
                if (addToCondition)
                {
                    if ((addToCondition) && (!parameterWithEnter.AddedToRestrictions()))
                    {
                        parameterForAnalysis.AddToRestrictions(conditionType, value);
                        return parameterForAnalysis.ToRestriction(conditionType, value);
                    }
                }
            }
            else
            {
                correct = false;
               
            }
                            
            return null;              
        }


        public static List<Restriction> GetRestctions()
        {
            List<Restriction> restrictions = new List<Restriction>();

            foreach (var condition in StaticData.ConditionsForParameterWithEnter)
            {
                restrictions.Add(new Restriction(condition.Key, condition.Value));
            }

            foreach (var condition in StaticData.ConditionsForParameterWithCalculation)
            {
                restrictions.Add(new Restriction(condition.Key, condition.Value));
            }

            foreach (var condition in StaticData.ConditionsForParameterForAnalysis)
            {
                restrictions.Add(new Restriction(condition.Key, condition.Value));
            }
            return restrictions;
        }
    }
}
