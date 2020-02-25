﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemStabilityAnalysis.Helpers;

namespace SystemStabilityAnalysis.Models.Parameters
{
    public enum NameParameterWithCalculation
    {
        NoCorrect = 0,
        S,
        R,
        Rcyt1,
        Rf1,
        Rcyt2,
        Rf2,
        Rcyt3,
        Rf3,
        Rcyt,
        Wсyt1,
        Wf1,
        Wcyt2,
        Wf2,
        Wcyt3,
        Wf3,
        Wcyt,
        W,
        Smin1,
        Smin2,
        Smin3,
        SminC,
        Smin,
        S1,
        S2,
        S3,
        Sс,
        SN1,
        SN2,
        SN3
    }

    public static class ParameterWithCalculationExtension
    {
        public static Dictionary<NameParameterWithCalculation, string> Descriptions = new Dictionary<NameParameterWithCalculation, string>()
        {
            {NameParameterWithCalculation.R, "Стоимость функционирования системы в период ∆T"},
            {NameParameterWithCalculation.S, "Количество персонала "},
            {NameParameterWithCalculation.Rcyt1, "Расход ресурсов на  1 группу (Стоимость) в сутки"},
            {NameParameterWithCalculation.Rf1, "Расход ресурсов на  1 группу (Стоимость) за период функционирования"},
            {NameParameterWithCalculation.Rcyt2, "Расход ресурсов на 2 группу (Стоимость) в сутки"},
            {NameParameterWithCalculation.Rf2, "Расход ресурсов на  2 группу (Стоимость) за период функционирования"},
            {NameParameterWithCalculation.Rcyt3, "Расход ресурсов на 3 группу (Стоимость) в сутки"},
            {NameParameterWithCalculation.Rf3, "Расход ресурсов на  3 группу (Стоимость) за период функционирования"},
            {NameParameterWithCalculation.Rcyt, "Расход ресурсов (Стоимость) функционирования системы за сутки"},
            {NameParameterWithCalculation.Wсyt1, "Расход человеко-часов на  1 группу в сутки"},
            {NameParameterWithCalculation.Wf1, "Расход человеко-часов на  1 группу за период функционирования"},
            {NameParameterWithCalculation.Wcyt2, "Расход человеко-часов на 2 группу в сутки"},
            {NameParameterWithCalculation.Wf2, "Расход человеко-часов на  2 группу за период функционирования"},
            {NameParameterWithCalculation.Wcyt3, "Расход человеко-часов на 3 группу в сутки"},
            {NameParameterWithCalculation.Wf3, "Расход человеко-часов на  3 группу за период функционирования"},
            {NameParameterWithCalculation.Wcyt, "Расход человеко-часов на функционирование системы за сутки"},
            {NameParameterWithCalculation.W, "Расход человеко-часов на функционирование системы за период (∆T)"},
            {NameParameterWithCalculation.Smin1, "Мин. необходимое количество специалистов в 1 смене для 1 группы"},
            {NameParameterWithCalculation.Smin2, "Мин. необходимое количество специалистов в 1 смене для 2 группы"},
            {NameParameterWithCalculation.Smin3, "Мин. необходимое количество специалистов в 1 смене для 3 группы"},
            {NameParameterWithCalculation.SminC, "Мин. необходимое количество персонала в 1 смене для всей системы"},
            {NameParameterWithCalculation.Smin, "Мин. необходимое кол-во персонала для всей системы на период ∆T"},
            {NameParameterWithCalculation.S1, "Необходимое количество специалистов в  одной смене для 1 группы"},
            {NameParameterWithCalculation.S2, "Необходимое количество специалистов в  одной смене для 2 группы"},
            {NameParameterWithCalculation.S3, "Необходимое количество специалистов в одной смене для 3 группы"},
            {NameParameterWithCalculation.Sс, "Необходимое количество персонала в одной смене для всей системы"},
            {NameParameterWithCalculation.SN1, "Из этого количества: в ремонтно-восстановительные формирования"},
            {NameParameterWithCalculation.SN2, "Непосредственно для обеспечения функционирования (выполнения основных функций) системы"},
            {NameParameterWithCalculation.SN3, "Для подсистемы обеспечения подсистемы хранения запасов и резервирования"}
        };

        public static Dictionary<NameParameterWithCalculation, string> Designations = new Dictionary<NameParameterWithCalculation, string>()
        {
            {NameParameterWithCalculation.Rcyt1, "Rсут1"},
            {NameParameterWithCalculation.Rf1, "Rф1"},
            {NameParameterWithCalculation.Rcyt2, "Rсут2"},
            {NameParameterWithCalculation.Rf2, "Rф2"},
            {NameParameterWithCalculation.Rcyt3, "Rсут3"},
            {NameParameterWithCalculation.Rf3, "Rф3"},
            {NameParameterWithCalculation.Rcyt, "Rсут"},
            {NameParameterWithCalculation.Wсyt1, "Wсут1"},
            {NameParameterWithCalculation.Wf1, "Wф1"},
            {NameParameterWithCalculation.Wcyt2, "Wcyt2"},
            {NameParameterWithCalculation.Wf2, "Wф2"},
            {NameParameterWithCalculation.Wcyt3, "Wcyt3"},
            {NameParameterWithCalculation.Wf3, "Wф3"},
            {NameParameterWithCalculation.Wcyt, "Wсут"}
        };

        public static Dictionary<NameParameterWithCalculation, UnitType> Units = new Dictionary<NameParameterWithCalculation, UnitType>()
        {
            { NameParameterWithCalculation.Rcyt1, UnitType.ThousandRubles},
            { NameParameterWithCalculation.Rf1, UnitType.ThousandRubles},
            { NameParameterWithCalculation.Rcyt2, UnitType.ThousandRubles},
            { NameParameterWithCalculation.Rf2, UnitType.ThousandRubles},
            { NameParameterWithCalculation.Rcyt3, UnitType.ThousandRubles},
            { NameParameterWithCalculation.Rf3, UnitType.ThousandRubles},
            { NameParameterWithCalculation.Rcyt, UnitType.ThousandRubles},
            { NameParameterWithCalculation.R, UnitType.ThousandRubles},
            { NameParameterWithCalculation.Wсyt1, UnitType.ManHour},
            { NameParameterWithCalculation.Wf1, UnitType.ManHour},
            { NameParameterWithCalculation.Wcyt2, UnitType.ManHour},
            { NameParameterWithCalculation.Wf2, UnitType.ManHour},
            { NameParameterWithCalculation.Wcyt3, UnitType.ManHour},
            { NameParameterWithCalculation.Wf3, UnitType.ManHour},
            { NameParameterWithCalculation.Wcyt, UnitType.ManHour},
            { NameParameterWithCalculation.W, UnitType.ManHour},
            { NameParameterWithCalculation.Smin1, UnitType.Man},
            { NameParameterWithCalculation.Smin2, UnitType.Man},
            { NameParameterWithCalculation.Smin3, UnitType.Man},
            { NameParameterWithCalculation.SminC, UnitType.Man},
            { NameParameterWithCalculation.Smin, UnitType.Man},
            { NameParameterWithCalculation.S1, UnitType.Man},
            { NameParameterWithCalculation.S2, UnitType.Man},
            { NameParameterWithCalculation.S3, UnitType.Man},
            { NameParameterWithCalculation.Sс, UnitType.Man},
            { NameParameterWithCalculation.S, UnitType.Man},
            { NameParameterWithCalculation.SN1, UnitType.Man},
            { NameParameterWithCalculation.SN2, UnitType.Man},
            { NameParameterWithCalculation.SN3, UnitType.Man}
        };

        public static Dictionary<NameParameterWithCalculation, TypeRound> Rounds = new Dictionary<NameParameterWithCalculation, TypeRound>()
        {
            { NameParameterWithCalculation.Smin1, TypeRound.Ceiling},
            { NameParameterWithCalculation.Smin2, TypeRound.Ceiling},
            { NameParameterWithCalculation.Smin3, TypeRound.Ceiling},
            { NameParameterWithCalculation.SminC, TypeRound.Ceiling},
            { NameParameterWithCalculation.Smin, TypeRound.Ceiling},
            { NameParameterWithCalculation.S1, TypeRound.Ceiling},
            { NameParameterWithCalculation.S2, TypeRound.Ceiling},
            { NameParameterWithCalculation.S3, TypeRound.Ceiling},
            { NameParameterWithCalculation.Sс, TypeRound.Ceiling},
            { NameParameterWithCalculation.S, TypeRound.Ceiling},
            { NameParameterWithCalculation.SN1, TypeRound.Ceiling},
            { NameParameterWithCalculation.SN2, TypeRound.Ceiling},
            { NameParameterWithCalculation.SN3, TypeRound.Ceiling}
        };

        public static string GetDescription(this NameParameterWithCalculation parameter)
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

        public static string GetDesignation(this NameParameterWithCalculation parameter)
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

        public static UnitType GetUnit(this NameParameterWithCalculation parameter)
        {
            if (Units.TryGetValue(parameter, out UnitType unitType))
            {
                return unitType;
            }

            throw new ArgumentException(paramName: parameter.ToString(), message: String.Format("Тип для параметра {0} не найден", parameter.ToString()));
        }

        public static double Calculate(this NameParameterWithCalculation parameter)
        {
            return 0;
        }

        public static string GetName(this NameParameterWithCalculation parameter)
        {
            return Enum.GetName(typeof(NameParameterWithCalculation), parameter);
        }

        public static object ToJson(this NameParameterWithCalculation parameter)
        {
            return new
            {
                Name = parameter.GetDesignation(),
                Value = parameter.GetName(),
                Description = parameter.GetDescription(),
                Unit = parameter.GetUnit().GetDescription()
            };
        }

        public static double Round (this NameParameterWithCalculation parameter, double value)
        {
            if (Rounds.TryGetValue(parameter, out TypeRound round))
            {
                return round.Round(value);
            }

            return value;

        }

        public static IEnumerable<object> GetForRestrictions(this NameParameterWithCalculation parameter)
        {
            return null;
        }

        public static void AddToRestrictions(this NameParameterWithCalculation parameter, ConditionType conditionType, double value)
        {
            StaticData.ConditionsForParameterWithCalculation.Add(parameter, new Condition(conditionType, value));
        }

        public static bool DeleteFromRestrictions(this NameParameterWithCalculation parameter)
        {
            if (!StaticData.ConditionsForParameterWithCalculation.ContainsKey(parameter))
                return false;

            StaticData.ConditionsForParameterWithCalculation.Remove(parameter);

            return true;
        }

        public static void DeleteAllRestrictions(this NameParameterWithCalculation parameter)
        {
            StaticData.ConditionsForParameterWithCalculation.Clear();
        }

        public static object ToRestriction(this NameParameterWithCalculation parameter, ConditionType conditionType, double value)
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

        public static object ToParameter(this NameParameterWithCalculation parameter, double? value, bool correct)
        {
            return new
            {
                Status = Status.Success.GetName(),
                Name = parameter.GetDesignation(),
                Description = parameter.GetDescription(),
                Unit = parameter.GetUnit().GetDescription(),
                Value = value.HasValue ? value.Value.ToString() : "_",
                Correct = correct
            };
        }

        public static object ToPair(this NameParameterWithCalculation parameter)
        {
            return new
            {
                Name = parameter.GetDesignation(),
                Description = parameter.GetDescription(),
                Value = parameter.GetName()
            };
        }
    }

    public class ParameterWithCalculation
    {

        public string Name { get { return TypeParameter.GetName(); } }

        public string Description { get { return TypeParameter.GetDescription(); } }

        public string Designation { get { return TypeParameter.GetDesignation(); } }

        public Unit Unit { get; }

        public NameParameterWithCalculation TypeParameter { get; }

        public double? Value{ get {  return Calculate.Invoke(); }}

        public Func<double?> Calculate;

        public ParameterWithCalculation(PropertiesSystem propertiesSystem, NameParameterWithCalculation parameter, Func<double?> calculate)
        {
            TypeParameter = parameter;
            Unit = new Unit(TypeParameter.GetUnit());

            propertiesSystem.ParametersWithCalculation.Add(TypeParameter, this);

            Calculate = calculate;
        }

        public ResultVerification Verification()
        {
            ResultVerification result = new ResultVerification() { IsCorrect = true };
            if (!Value.HasValue)
            {
                result.AddError(String.Format("Значение параметра {0} не указано", Designation));
            }
            else
            {
                if (Value.Value < 0)
                {
                    result.AddError(String.Format("Значение параметра {0} должно быть > 0", Designation));
                }
                else if (StaticData.ConditionsForParameterWithCalculation.TryGetValue(this.TypeParameter, out Condition condition))
                {
                    result.IsCorrect = condition.InvokeComparison(Value.Value);
                    if (!result.IsCorrect)
                    {
                        result.ErrorMessages.Add(Description + " " + condition.ErrorMessage);
                    }
                }
            }
            return result;
        }
    }
}
