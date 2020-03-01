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
        d,
        h
    }

    public static class ParameterForAnalysisExtension
    {
        public static Dictionary<NameParameterForAnalysis, string> Descriptions = new Dictionary<NameParameterForAnalysis, string>()
        {
            {NameParameterForAnalysis.ρ, "Коэффициент ввода новых элементов (1 группы) в эксплуатацию"},
            {NameParameterForAnalysis.a, "Коэффициент учета влияния недостатка ресурсов на первую группу элементов"},
            {NameParameterForAnalysis.b, "Коэффициент уничтожения элементов первой группы"},
            {NameParameterForAnalysis.f, "Коэффициент интенсивности восстановления (перехода элементов 1 группы во 2-ю)"},
            {NameParameterForAnalysis.q, "Коэффициент интенсивности перехода элементов 2 группы в 3-ю"},
            {NameParameterForAnalysis.d, "Коэффициент уничтожения (неисправности) элементов второй группы"},
            {NameParameterForAnalysis.h, "Коэффициент использования (убыли) элементов третьей группы в интересах 1-й"}
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
            {NameParameterForAnalysis.h, UnitType.NoType},
        };
        public static Dictionary<NameParameterForAnalysis, List<NameParameterWithEnter>> Dependences = new Dictionary<NameParameterForAnalysis, List<NameParameterWithEnter>>()
        {
            {NameParameterForAnalysis.ρ, new List<NameParameterWithEnter>(){ NameParameterWithEnter.P1, NameParameterWithEnter.N1 }},
            {NameParameterForAnalysis.a, new List<NameParameterWithEnter>(){ NameParameterWithEnter.A1, NameParameterWithEnter.N1 }},
            {NameParameterForAnalysis.b, new List<NameParameterWithEnter>(){ NameParameterWithEnter.B1, NameParameterWithEnter.N1 }},
            {NameParameterForAnalysis.f,new List<NameParameterWithEnter>(){ NameParameterWithEnter.F1, NameParameterWithEnter.N1 }},
            {NameParameterForAnalysis.q, new List<NameParameterWithEnter>(){ NameParameterWithEnter.Q2, NameParameterWithEnter.N2 }},
            {NameParameterForAnalysis.d, new List<NameParameterWithEnter>(){ NameParameterWithEnter.D2, NameParameterWithEnter.N2 }},
             {NameParameterForAnalysis.h, new List<NameParameterWithEnter>(){ NameParameterWithEnter.H3, NameParameterWithEnter.N3 }}
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

        public static List<NameParameterWithEnter> GetDependences(this NameParameterForAnalysis parameter)
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
                Unit = parameter.GetUnit().GetDesignation()
            };
        }

        public static bool AddedToRestrictions(this NameParameterForAnalysis parameter)
        {
            return StaticData.ConditionsForParameterForAnalysis.Keys.Contains(parameter);
        }

        public static void AddToRestrictions(this NameParameterForAnalysis parameter, ConditionType conditionType, double value)
        {          
            StaticData.ConditionsForParameterForAnalysis.Add(parameter, new Condition(conditionType, value));
            parameter.VerificateParameterForCurrentSystem();
        }
        private static void VerificateParameterForCurrentSystem(this NameParameterForAnalysis parameter)
        {
            StaticData.CurrentSystems.ParametersForAnalysis[parameter].Verification(out string message);
        }
        //public static bool AddToRestrictions(this NameParameterForAnalysis parameter, ConditionType conditionType, double value, out string message)
        //{
        //    message = null;
        //    if (!StaticData.ConditionsForParameterForAnalysis.TryAdd(parameter, new Condition(conditionType, value)))
        //    {
        //        message = string.Format("Ограничение для параметра {0} уже добавлено", parameter.GetDesignation());
        //        return false;
        //    }
        //    return false;
        //}

        public static bool DeleteFromRestrictions(this NameParameterForAnalysis parameter)
        {

            if (!StaticData.ConditionsForParameterForAnalysis.ContainsKey(parameter))
                return false;

            StaticData.ConditionsForParameterForAnalysis.Remove(parameter);
            parameter.VerificateParameterForCurrentSystem();
            return true;
        }

        public static void DeleteAllRestrictions(this NameParameterForAnalysis parameter)
        {
            foreach (var parameterForAnalysis in StaticData.ConditionsForParameterForAnalysis.Keys)
            {
                parameterForAnalysis.DeleteFromRestrictions();
            }
        }

        public static object ToRestriction(this NameParameterForAnalysis parameter, ConditionType conditionType, double value)
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

        public static object ToParameter(this NameParameterForAnalysis parameter, double? value, bool correct)
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

        public static object ToPair(this NameParameterForAnalysis parameter)
        {
            return new
            {
                Name = parameter.GetDesignation(),
                Description = parameter.GetDescription(),
                Value = parameter.GetName()
            };
        }

    }

    public class ParameterForAnalysis
    {
        public PropertiesSystem propertiesSystem { get; set; }

        public string Name { get { return TypeParameter.GetName(); } }

        public string Description { get { return TypeParameter.GetDescription(); } }

        public string Designation { get { return TypeParameter.GetDesignation(); } }

        public UnitType UnitType { get { return TypeParameter.GetUnit(); } }

        public NameParameterForAnalysis TypeParameter { get; }

        public bool isCorrect { get; set; }

        public double? Value { get { return isCorrect ? Calculate.Invoke() : null; } }


        public Func<double?> Calculate;

        public ParameterForAnalysis(PropertiesSystem _propertiesSystem, NameParameterForAnalysis parameter, Func<double?> calculate)
        {
            TypeParameter = parameter;
            propertiesSystem = _propertiesSystem;
            _propertiesSystem.ParametersForAnalysis.Add(TypeParameter, this);

            Calculate = calculate;
        }

        public bool Verification(out string message)
        {
            message = "";
            isCorrect = propertiesSystem.VerificationParametersWithEnter(TypeParameter.GetDependences(), out List<string> messages);
            string postfix =  string.Format("Проверьте правильность полей: {0}", string.Join(',', messages));
            if (!Value.HasValue)
            {
                message = String.Format("Не удалось расчитать значение параметра {0}. {1}", Designation, postfix);
                isCorrect = false;
            }
            else
            {
                if (Value.Value < 1)
                {
                    message = String.Format("Значение параметра {0} должно быть > 0. {1}", Designation, postfix);
                    isCorrect = false;
                }
                else if (StaticData.ConditionsForParameterForAnalysis.TryGetValue(this.TypeParameter, out Condition condition))
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

        public double? Pow(double y)
        {
           return Value.HasValue?(double?)Math.Pow(Value.Value, y):null;
        }

        public static double? operator +(ParameterForAnalysis c1, double? c2)
        {
            return c1.Value + c2;
        }
        public static double? operator +(double? c1, ParameterForAnalysis c2)
        {
            return c1 + c2.Value;
        }
        public static double? operator *(ParameterForAnalysis c1, double? c2)
        {
            return c1.Value * c2;
        }
        public static double? operator *(double? c1, ParameterForAnalysis c2)
        {
            return c1 * c2.Value;
        }
        public static double? operator -(ParameterForAnalysis c1, double? c2)
        {
            return c1.Value - c2;
        }
        public static double? operator -(double? c1, ParameterForAnalysis c2)
        {
            return c1 - c2.Value;
        }
        public static double? operator /(ParameterForAnalysis c1, double? c2)
        {
            return c1.Value / c2;
        }
        public static double? operator /(double? c1, ParameterForAnalysis c2)
        {
            return c1 / c2.Value;
        }
        public static double? operator *(ParameterForAnalysis c1, ParameterForAnalysis c2)
        {
            return c1.Value * c2.Value;
        }
        public static double? operator /(ParameterForAnalysis c1, ParameterForAnalysis c2)
        {
            return c1.Value / c2.Value;
        }
        public static double? operator +(ParameterForAnalysis c1, ParameterForAnalysis c2)
        {
            return c1.Value + c2.Value;
        }
        public static double? operator -(ParameterForAnalysis c1, ParameterForAnalysis c2)
        {
            return c1.Value - c2.Value;
        }
    }
}
