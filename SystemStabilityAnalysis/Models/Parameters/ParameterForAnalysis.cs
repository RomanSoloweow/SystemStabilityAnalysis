using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemStabilityAnalysis.Helpers;

namespace SystemStabilityAnalysis.Models.Parameters
{
    public enum NameParameterForAnalysis
    {
        NoCorrect = 0,
        ρ,
        a,
        b,
        f,
        q,
        d      
    }

    public static class ParameterForAnalysisExtension
    {
        public static Dictionary<NameParameterForAnalysis, string> Descriptions = new Dictionary<NameParameterForAnalysis, string>()
        {
            {NameParameterForAnalysis.ρ, "Коэффициент ввода новых элементов (1 группы) в эксплуатацию"},
            {NameParameterForAnalysis.a, "Коэффициент учета влияния недостатка ресурсов на первую группу элементов"},
            {NameParameterForAnalysis.b, "Коэффициент интенсивностивосстановления (перехода элементов 1 группы во 2-ю)"},
            {NameParameterForAnalysis.f, "Коэффициент интенсивности перехода элементов 2 группы в 3-ю"},
            {NameParameterForAnalysis.q, "Коэффициент  уничтожения (неисправности) элементов второй группы"},
            {NameParameterForAnalysis.d, "Коэффициент  использования (убыли) элементов третьей группы в интересах 1-й"},

        };

        public static Dictionary<NameParameterForAnalysis, string> Designations = new Dictionary<NameParameterForAnalysis, string>()
        {
 
        };

        public static Dictionary<NameParameterForAnalysis, UnitType> Units = new Dictionary<NameParameterForAnalysis, UnitType>()
        {
            {NameParameterForAnalysis.ρ, UnitType.NoType},
            {NameParameterForAnalysis.a, UnitType.NoType},
            {NameParameterForAnalysis.b, UnitType.NoType},
            {NameParameterForAnalysis.f,UnitType.NoType},
            {NameParameterForAnalysis.q, UnitType.NoType},
            {NameParameterForAnalysis.d, UnitType.NoType},
        };

        public static string GetDescription(this NameParameterForAnalysis parameter)
        {
            if (Descriptions.TryGetValue(parameter, out string description))
            {
                return description;
            }
            else
            {
                return parameter.ToString();
            }
        }

        public static string GetDesignation(this NameParameterForAnalysis parameter)
        {
            if (Designations.TryGetValue(parameter, out string designation))
            {
                return designation;
            }
            else
            {
                return parameter.ToString();
            }
        }

        public static UnitType GetUnit(this NameParameterForAnalysis parameter)
        {
            if (Units.TryGetValue(parameter, out UnitType unitType))
            {
                return unitType;
            }

            throw new ArgumentException(paramName: parameter.ToString(), message: String.Format("Тип для параметра {0} не найден", parameter.ToString()));
        }

        public static double Calculate(this NameParameterForAnalysis parameter)
        {
            return 0;
        }

        public static string GetName(this NameParameterForAnalysis parameter)
        {
            return Enum.GetName(typeof(NameParameterForAnalysis), parameter);
        }

        public static object ToJson(this NameParameterForAnalysis parameter)
        {
            return new
            {
                Name = parameter.GetDesignation(),
                Value = parameter.GetName(),
                Description = parameter.GetDescription(),
                Unit = parameter.GetUnit().GetDescription()
            };
        }

        public static void AddToRestrictions(this NameParameterForAnalysis parameter, ConditionType conditionType, double value)
        {
            StaticData.ConditionsForParameterForAnalysis.Add(parameter, new Condition(conditionType, value));
        }
        public static bool DeleteFromRestrictions(this NameParameterForAnalysis parameter)
        {

            if (!StaticData.ConditionsForParameterForAnalysis.ContainsKey(parameter))
                return false;

            StaticData.ConditionsForParameterForAnalysis.Remove(parameter);

            return true;
        }
        public static void DeleteAllRestrictions(this NameParameterForAnalysis parameter)
        {
            StaticData.ConditionsForParameterForAnalysis.Clear();
        }

        public static object ToRestriction(this NameParameterForAnalysis parameter, ConditionType conditionType, double value)
        {
            return new
            {
                Status = Status.Success.GetName(),
                Name = parameter.GetDesignation(),
                Description = parameter.GetDescription(),
                Unit = parameter.GetUnit().GetDescription(),
                Condition = conditionType.GetDesignation(),
                Value = value,
                RestrictionName = parameter.GetName()
            };
        }
        public static object ToParameter(this NameParameterForAnalysis parameter, double value, bool correct)
        {
            return new
            {
                Status = Status.Success.GetName(),
                Name = parameter.GetDesignation(),
                Description = parameter.GetDescription(),
                Unit = parameter.GetUnit().GetDescription(),
                Value = double.IsNaN(value) ? "NaN" : value.ToString(),
                Correct = correct
            };
        }
    }

    public class ParameterForAnalysis
    {
        public string Name { get { return TypeParameter.GetName(); } }

        public string Description { get { return TypeParameter.GetDescription(); } }

        public string Designation { get { return TypeParameter.GetDesignation(); } }

        public Unit Unit { get; }

        public NameParameterForAnalysis TypeParameter { get; }

        public double Value { get { return Calculate.Invoke(); } }


        public Func<double> Calculate;

        public ParameterForAnalysis(PropertiesSystem propertiesSystem, NameParameterForAnalysis parameter, Func<double> calculate)
        {
            TypeParameter = parameter;
            Unit = new Unit(TypeParameter.GetUnit());

            propertiesSystem.ParametersForAnalysis.Add(TypeParameter, this);

            Calculate = calculate;
        }

        public ResultVerification Verification()
        {
            ResultVerification result = new ResultVerification() { IsCorrect = true };

            if (!(Value > 0))
            {
                result.IsCorrect = false;
                result.ErrorMessages.Add(String.Format("Значение параметра {0} должно быть > 0", Name));
            }

            if (StaticData.ConditionsForParameterForAnalysis.TryGetValue(this.TypeParameter, out Condition condition))
            {
                result.IsCorrect = condition.InvokeComparison(Value);
                if (!result.IsCorrect)
                {
                    result.ErrorMessages.Add(Description + " " + condition.ErrorMessage);
                }
            }

            return result;
        }
    }
}
