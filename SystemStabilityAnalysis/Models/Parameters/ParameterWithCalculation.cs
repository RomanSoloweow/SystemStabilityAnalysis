using System;
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
            {NameParameterWithCalculation.R, "Стоимость функционирования системы в период функционирования"},
            {NameParameterWithCalculation.S, "Необходимое количество персонала для всей системы на период функционирования"},
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
            {NameParameterWithCalculation.W, "Расход человеко-часов на функционирование системы за период функционирования "},
            {NameParameterWithCalculation.Smin1, "Мин. необходимое количество специалистов в 1 смене для 1 группы"},
            {NameParameterWithCalculation.Smin2, "Мин. необходимое количество специалистов в 1 смене для 2 группы"},
            {NameParameterWithCalculation.Smin3, "Мин. необходимое количество специалистов в 1 смене для 3 группы"},
            {NameParameterWithCalculation.SminC, "Мин. необходимое количество персонала в 1 смене для всей системы"},
            {NameParameterWithCalculation.Smin, "Мин. необходимое кол-во персонала для всей системы на период функционирования"},
            {NameParameterWithCalculation.S1, "Необходимое количество специалистов в  одной смене для 1 группы"},
            {NameParameterWithCalculation.S2, "Необходимое количество специалистов в  одной смене для 2 группы"},
            {NameParameterWithCalculation.S3, "Необходимое количество специалистов в одной смене для 3 группы"},
            {NameParameterWithCalculation.Sс, "Необходимое количество персонала в одной смене для всей системы"},
            {NameParameterWithCalculation.SN1, "Необходимое количество персонала в ремонтно-восстановительные формирования на период функционирования"},
            {NameParameterWithCalculation.SN2, "Необходимое количество персонала непосредственно для обеспечения функционирования (выполнения основных функций) системы на период функционирования"},
            {NameParameterWithCalculation.SN3, "Необходимое количество персонала для обеспечения подсистемы хранения запасов и резервирования на период функционирования"}
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
            {NameParameterWithCalculation.Wcyt2, "Wсут2"},
            {NameParameterWithCalculation.Wf2, "Wф2"},
            {NameParameterWithCalculation.Wcyt3, "Wсут3"},
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

        public static Dictionary<NameParameterWithCalculation, List<NameParameterWithEnter>> Dependences = new Dictionary<NameParameterWithCalculation, List<NameParameterWithEnter>>()
        {
            { NameParameterWithCalculation.Rcyt1, new List<NameParameterWithEnter>()
            {   
                NameParameterWithEnter.N1,
                NameParameterWithEnter.R1,
                NameParameterWithEnter.P1,
                NameParameterWithEnter.Rv2,
                NameParameterWithEnter.Lc
                }},
            { NameParameterWithCalculation.Rf1, new List<NameParameterWithEnter>()
            {
                NameParameterWithEnter.N1,
                NameParameterWithEnter.R1,
                NameParameterWithEnter.P1,
                NameParameterWithEnter.Rv2,
                NameParameterWithEnter.Lc,
                NameParameterWithEnter.DeltaT
            }},
            { NameParameterWithCalculation.Rcyt2, new List<NameParameterWithEnter>()
            {
                NameParameterWithEnter.N2,
                NameParameterWithEnter.R2,
                NameParameterWithEnter.Lc
            }},
            { NameParameterWithCalculation.Rf2, new List<NameParameterWithEnter>()
            {
                NameParameterWithEnter.N2,
                NameParameterWithEnter.R2,
                NameParameterWithEnter.Lc,
                NameParameterWithEnter.DeltaT
            }},
            { NameParameterWithCalculation.Rcyt3, new List<NameParameterWithEnter>()
            {
                NameParameterWithEnter.N3,
                NameParameterWithEnter.R3,
                NameParameterWithEnter.Lc
            }},
            { NameParameterWithCalculation.Rf3, new List<NameParameterWithEnter>()
            {
                NameParameterWithEnter.N3,
                NameParameterWithEnter.R3,
                NameParameterWithEnter.Lc,
                NameParameterWithEnter.DeltaT
            }},
            { NameParameterWithCalculation.Rcyt, new List<NameParameterWithEnter>()
            {
                NameParameterWithEnter.N1,
                NameParameterWithEnter.R1,
                NameParameterWithEnter.P1,
                NameParameterWithEnter.Rv2,
                NameParameterWithEnter.N2,
                NameParameterWithEnter.R2,
                NameParameterWithEnter.N3,
                NameParameterWithEnter.R3,
                NameParameterWithEnter.Lc
                }},
            { NameParameterWithCalculation.R, new List<NameParameterWithEnter>()
            {
                NameParameterWithEnter.N1,
                NameParameterWithEnter.R1,
                NameParameterWithEnter.P1,
                NameParameterWithEnter.Rv2,
                NameParameterWithEnter.N2,
                NameParameterWithEnter.R2,
                NameParameterWithEnter.N3,
                NameParameterWithEnter.R3,
                NameParameterWithEnter.Lc,
                NameParameterWithEnter.DeltaT
            }},
            { NameParameterWithCalculation.Wсyt1, new List<NameParameterWithEnter>()
            {
                NameParameterWithEnter.N1,
                NameParameterWithEnter.W1,
                NameParameterWithEnter.Wv2,
                NameParameterWithEnter.P1
            }},
            { NameParameterWithCalculation.Wf1, new List<NameParameterWithEnter>()
            {
                NameParameterWithEnter.N1,
                NameParameterWithEnter.W1,
                NameParameterWithEnter.Wv2,
                NameParameterWithEnter.P1,
                NameParameterWithEnter.DeltaT
            }},
            { NameParameterWithCalculation.Wcyt2, new List<NameParameterWithEnter>()
            {
                NameParameterWithEnter.N2,
                NameParameterWithEnter.W2
            }},
            { NameParameterWithCalculation.Wf2, new List<NameParameterWithEnter>()
            {
                NameParameterWithEnter.N2,
                NameParameterWithEnter.W2,
                NameParameterWithEnter.DeltaT
            }},
            { NameParameterWithCalculation.Wcyt3, new List<NameParameterWithEnter>()
            {
                NameParameterWithEnter.N3,
                NameParameterWithEnter.W3
            }},
            { NameParameterWithCalculation.Wf3, new List<NameParameterWithEnter>()
            {
                NameParameterWithEnter.N3,
                NameParameterWithEnter.W3,
                NameParameterWithEnter.DeltaT
            }},
            { NameParameterWithCalculation.Wcyt, new List<NameParameterWithEnter>()
            {
                NameParameterWithEnter.N1,
                NameParameterWithEnter.W1,
                NameParameterWithEnter.Wv2,
                NameParameterWithEnter.P1,
                NameParameterWithEnter.N2,
                NameParameterWithEnter.W2,
                NameParameterWithEnter.N3,
                NameParameterWithEnter.W3
            }},
            { NameParameterWithCalculation.W, new List<NameParameterWithEnter>()
            {
                NameParameterWithEnter.N1,
                NameParameterWithEnter.W1,
                NameParameterWithEnter.Wv2,
                NameParameterWithEnter.P1,
                NameParameterWithEnter.N2,
                NameParameterWithEnter.W2,
                NameParameterWithEnter.N3,
                NameParameterWithEnter.W3,
                NameParameterWithEnter.DeltaT
            }},
            { NameParameterWithCalculation.Smin1, new List<NameParameterWithEnter>()
            {
                NameParameterWithEnter.N1,
                NameParameterWithEnter.W1,
                NameParameterWithEnter.Wv2,
                NameParameterWithEnter.P1,
                NameParameterWithEnter.Lc,
                NameParameterWithEnter.Tс
            }},
            { NameParameterWithCalculation.Smin2, new List<NameParameterWithEnter>()
            {
                NameParameterWithEnter.N2,
                NameParameterWithEnter.W2,
                NameParameterWithEnter.Lc,
                NameParameterWithEnter.Tс
            }},
            { NameParameterWithCalculation.Smin3, new List<NameParameterWithEnter>()
            {
                NameParameterWithEnter.N3,
                NameParameterWithEnter.W3,
                NameParameterWithEnter.Lc,
                NameParameterWithEnter.Tс
            }},
            { NameParameterWithCalculation.SminC, new List<NameParameterWithEnter>()
            {
                NameParameterWithEnter.N1,
                NameParameterWithEnter.W1,
                NameParameterWithEnter.Wv2,
                NameParameterWithEnter.P1,
                NameParameterWithEnter.N2,
                NameParameterWithEnter.W2,
                NameParameterWithEnter.N3,
                NameParameterWithEnter.W3,
                NameParameterWithEnter.Lc,
                NameParameterWithEnter.Tс
            }},
            { NameParameterWithCalculation.Smin, new List<NameParameterWithEnter>()
            {
                NameParameterWithEnter.N1,
                NameParameterWithEnter.W1,
                NameParameterWithEnter.Wv2,
                NameParameterWithEnter.P1,
                NameParameterWithEnter.N2,
                NameParameterWithEnter.W2,
                NameParameterWithEnter.N3,
                NameParameterWithEnter.W3,
                NameParameterWithEnter.Tс
            }},
            { NameParameterWithCalculation.S1, new List<NameParameterWithEnter>()
            {
                NameParameterWithEnter.N1,
                NameParameterWithEnter.W1,
                NameParameterWithEnter.Wv2,
                NameParameterWithEnter.P1,
                NameParameterWithEnter.Lc,
                NameParameterWithEnter.Tс,
                NameParameterWithEnter.Sn2
            }},
            { NameParameterWithCalculation.S2, new List<NameParameterWithEnter>()
            { 
                NameParameterWithEnter.N2,
                NameParameterWithEnter.W2,
                NameParameterWithEnter.Lc,
                NameParameterWithEnter.Tс,
                NameParameterWithEnter.Sn2
            }},
            { NameParameterWithCalculation.S3, new List<NameParameterWithEnter>()
            {
                NameParameterWithEnter.N3,
                NameParameterWithEnter.W3,
                NameParameterWithEnter.Lc,
                NameParameterWithEnter.Tс,
                NameParameterWithEnter.Sn3
            }},
            { NameParameterWithCalculation.Sс, new List<NameParameterWithEnter>()
            {
                NameParameterWithEnter.N1,
                NameParameterWithEnter.W1,
                NameParameterWithEnter.Wv2,
                NameParameterWithEnter.P1,
                NameParameterWithEnter.N2,
                NameParameterWithEnter.W2,
                NameParameterWithEnter.N3,
                NameParameterWithEnter.W3,
                NameParameterWithEnter.Lc,
                NameParameterWithEnter.Tс,
                NameParameterWithEnter.Sn2,
                NameParameterWithEnter.Sn3}},
            { NameParameterWithCalculation.S, new List<NameParameterWithEnter>()
            {
                NameParameterWithEnter.N1,
                NameParameterWithEnter.W1,
                NameParameterWithEnter.Wv2,
                NameParameterWithEnter.P1,
                NameParameterWithEnter.N2,
                NameParameterWithEnter.W2,
                NameParameterWithEnter.N3,
                NameParameterWithEnter.W3,
                NameParameterWithEnter.DeltaT,
                NameParameterWithEnter.Tс,
                NameParameterWithEnter.Sn2,
                NameParameterWithEnter.Sn3,
                NameParameterWithEnter.Lc,
                NameParameterWithEnter.DeltaT
            }},
            { NameParameterWithCalculation.SN1, new List<NameParameterWithEnter>()
            {
                NameParameterWithEnter.N1,
                NameParameterWithEnter.W1,
                NameParameterWithEnter.Wv2,
                NameParameterWithEnter.P1,
                NameParameterWithEnter.Tс,
                NameParameterWithEnter.Sn2,
                NameParameterWithEnter.Lc,
                NameParameterWithEnter.DeltaT
            }},
            { NameParameterWithCalculation.SN2, new List<NameParameterWithEnter>()
            {
                NameParameterWithEnter.N2,
                NameParameterWithEnter.W2,
                NameParameterWithEnter.DeltaT,
                NameParameterWithEnter.Tс,
                NameParameterWithEnter.Sn2,
                NameParameterWithEnter.Lc,
                NameParameterWithEnter.DeltaT
            }},
            { NameParameterWithCalculation.SN3, new List<NameParameterWithEnter>(){
                NameParameterWithEnter.N3,
                NameParameterWithEnter.W3,
                NameParameterWithEnter.DeltaT,
                NameParameterWithEnter.Tс,
                NameParameterWithEnter.Sn3,
                NameParameterWithEnter.Lc,
                NameParameterWithEnter.DeltaT
            }}
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

        public static List<NameParameterWithEnter> GetDependences(this NameParameterWithCalculation parameter)
        {
            List<NameParameterWithEnter> dependences = new List<NameParameterWithEnter>();
            if (Dependences.TryGetValue(parameter, out dependences))
            {
                return dependences;
            }
            else
            {
                return dependences;
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
                Unit = parameter.GetUnit().GetDesignation()
            };
        }

        public static double? Round (this NameParameterWithCalculation parameter, double? value)
        {
            if ((value.HasValue)&&(Rounds.TryGetValue(parameter, out TypeRound round)))
            {
                return round.Round(value.Value);
            }

            return value;

        }

        public static IEnumerable<object> GetForRestrictions(this NameParameterWithCalculation parameter)
        {
            return null;
        }

        public static bool AddedToRestrictions(this NameParameterWithCalculation parameter)
        {
            return StaticData.ConditionsForParameterWithCalculation.Keys.Contains(parameter);
        }

        //public static bool AddToRestrictions(this NameParameterWithCalculation parameter, ConditionType conditionType, double value, out string message)
        //{
        //    message = null;
        //    if (!StaticData.ConditionsForParameterWithCalculation.TryAdd(parameter, new Condition(conditionType, value)))
        //    {
        //        message = string.Format("Ограничение для параметра {0} уже добавлено", parameter.GetDesignation());
        //        return false;
        //    }
        //    return false;
        //}
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
                Unit = parameter.GetUnit().GetDesignation(),
                Condition = conditionType.GetDesignation(),
                Value = value,
                RestrictionName = parameter.GetName()
            };
        }

        public static object ToParameter(this NameParameterWithCalculation parameter, double? value, bool correct)
        {
            return new
            {
                Name = parameter.GetName(),
                Designation = parameter.GetDesignation(),
                Description = parameter.GetDescription(),
                Unit = parameter.GetUnit().GetDesignation(),
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
        public PropertiesSystem propertiesSystem;

        public string Name { get { return TypeParameter.GetName(); } }

        public string Description { get { return TypeParameter.GetDescription(); } }

        public string Designation { get { return TypeParameter.GetDesignation(); } }

        public UnitType UnitType { get { return TypeParameter.GetUnit(); } }

        public NameParameterWithCalculation TypeParameter { get; }

        public bool isCorrect { get; set; }

        public double? Value{ get {  return isCorrect ? TypeParameter.Round(Calculate.Invoke()):null; }}

        public Func<double?> Calculate;

        public ParameterWithCalculation(PropertiesSystem _propertiesSystem, NameParameterWithCalculation parameter, Func<double?> calculate)
        {
            TypeParameter = parameter;

            propertiesSystem = _propertiesSystem;
            _propertiesSystem.ParametersWithCalculation.Add(TypeParameter, this);

            Calculate = calculate;
        }

        public bool Verification(out string message)
        {
            message = "";
            isCorrect = propertiesSystem.VerificationParametersWithEnter(TypeParameter.GetDependences(), out List<string> messages);
            string postfix = string.Format("Проверьте правильность полей: {0}", string.Join(',', messages));
            if (!Value.HasValue)
            {
                message = String.Format("Не удалось расчитать значение параметра {0}. {1}", Designation, postfix);
                isCorrect = false;
            }
            else
            {
                if (Value.Value < 0)
                {
                    message = String.Format("Значение параметра {0} должно быть > 0. {1}", Designation, postfix);
                    isCorrect = false;
                }
                else if (StaticData.ConditionsForParameterWithCalculation.TryGetValue(this.TypeParameter, out Condition condition))
                {
                    if (!condition.InvokeComparison(Value.Value))
                    {
                        message = String.Format("Значение параметра {0} должно быть {1}. {2}", Designation, condition.ErrorMessage, postfix);
                        isCorrect = false;
                    }
                }
            }
            return isCorrect;
        }
    }
}
