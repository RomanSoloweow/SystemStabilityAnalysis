using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using SystemStabilityAnalysis.Helpers;

namespace SystemStabilityAnalysis.Models.Parameters
{

    public enum NameParameterWithEnter
    {
        NoCorrect = 0,
        DeltaT,
        N1,
        N2,
        N3,
        P1,
        A1,
        B1,
        F1,
        Q2,
        D2,
        H3,
        Lc,
        Tс,
        R1,
        Rv2,
        R2,
        R3,
        W1,
        Wv2,
        W2,
        W3,
        Sn1,
        Sn2,
        Sn3,
    }

    public static class ParameterWithRestrictionExtension
    {
        public static Dictionary<NameParameterWithEnter, string> Descriptions = new Dictionary<NameParameterWithEnter, string>()
        {
            {NameParameterWithEnter.DeltaT, "Период устойчивой эксплуатации системы в сутках"},
            {NameParameterWithEnter.N1, "Количество элементов вводимых в эксплуатацию, 1 группа "},
            {NameParameterWithEnter.N2, "Количество элементов выполняющих функции системы, 2 группа "},
            {NameParameterWithEnter.N3, "Количество др. элементов: подсистема обеспечения, резерв,  неисправных и др., 3 группа"},
            {NameParameterWithEnter.P1, "Количество элементов 1 группы, введенных (восстановленных) в эксплуатацию"},
            {NameParameterWithEnter.A1, "Количество элементов 1 группы, не перешедших во 2 группу из-за недостатков ресурсов "},
            {NameParameterWithEnter.B1, "Количество элементов 1 группы, вышедших из строя  "},
            {NameParameterWithEnter.F1, "Количество элементов 1 группы, перешедших во 2 группу "},
            {NameParameterWithEnter.Q2, "Количество элементов 2 группы,  перешедших в 3 группу (неисправных, но еще подлежащих восстановлению или пригодных для использования)"},
            {NameParameterWithEnter.D2, "Количество не подлежащих восстановлению элементов 2 группы"},
            {NameParameterWithEnter.H3, "Количество элементов 3 группы, перешедших в 1 группу  "},
            {NameParameterWithEnter.Lc, "Количество смен в сутках"},
            {NameParameterWithEnter.Tс, "Время 1 смены в часах"},
            {NameParameterWithEnter.R1, "Макс. расход ресурсов на 1 элемент 1 группы (Стоимость) в сутки"},
            {NameParameterWithEnter.Rv2, "Макс. расход ресурсов на  восстановление (ремонт) элемента 2 группы (Стоимость) в сутки"},
            {NameParameterWithEnter.R2, "Макс. расход ресурсов на  1 элемент 2 группы (Стоимость) в сутки"},
            {NameParameterWithEnter.R3, "Макс. расход ресурсов на  1 элемент 3 группы (Стоимость) в сутки"},
            {NameParameterWithEnter.W1, "Макс. расход человеко-часов на 1 элемент 1 группы в сутки"},
            {NameParameterWithEnter.Wv2, "Макс. расход человеко-часов на  восстановление (ремонт) элемента 2 группы (Стоимость) в сутки"},
            {NameParameterWithEnter.W2, "Макс. расход человеко-часов на  1 элемент 2 группы в сутки"},
            {NameParameterWithEnter.W3, "Макс. расход человеко-часов на  1 элемент 3 группы  в сутки"},
            {NameParameterWithEnter.Sn1, "Прогнозируемое кол-во потерь специалистов 1 группы за одну смену"},
            {NameParameterWithEnter.Sn2, "Прогнозируемое кол-во потерь специалистов 2 группы за одну смену"},
            {NameParameterWithEnter.Sn3, "Прогнозируемое кол-во потерь специалистов 3 группы за одну смену"},
        };

        public static Dictionary<NameParameterWithEnter, string> Designations = new Dictionary<NameParameterWithEnter, string>()
        {
            {NameParameterWithEnter.DeltaT, "∆T"},
            {NameParameterWithEnter.Rv2, "Rв2"},
            {NameParameterWithEnter.Wv2, "Wв2"},

        };

        public static Dictionary<NameParameterWithEnter, UnitType> Units = new Dictionary<NameParameterWithEnter, UnitType>()
        {
            { NameParameterWithEnter.DeltaT, UnitType.Point},
            { NameParameterWithEnter.N1, UnitType.Point},
            { NameParameterWithEnter.N2, UnitType.Point},
            { NameParameterWithEnter.N3, UnitType.Point},
            { NameParameterWithEnter.P1, UnitType.Point},
            { NameParameterWithEnter.A1, UnitType.Point},
            { NameParameterWithEnter.B1, UnitType.Point},
            { NameParameterWithEnter.F1, UnitType.Point},
            { NameParameterWithEnter.Q2, UnitType.Point},
            { NameParameterWithEnter.D2, UnitType.Point},
            { NameParameterWithEnter.H3, UnitType.Point},
            { NameParameterWithEnter.Lc, UnitType.Day},
            { NameParameterWithEnter.Tс, UnitType.Hour},
            { NameParameterWithEnter.R1, UnitType.ThousandRubles},
            { NameParameterWithEnter.Rv2, UnitType.ThousandRubles},
            { NameParameterWithEnter.R2, UnitType.ThousandRubles},
            { NameParameterWithEnter.R3, UnitType.ThousandRubles},
            { NameParameterWithEnter.W1, UnitType.ManHour},
            { NameParameterWithEnter.Wv2, UnitType.ManHour},
            { NameParameterWithEnter.W2, UnitType.ManHour},
            { NameParameterWithEnter.W3, UnitType.ManHour},
            { NameParameterWithEnter.Sn1, UnitType.Man},
            { NameParameterWithEnter.Sn2, UnitType.Man},
            { NameParameterWithEnter.Sn3, UnitType.Man},
        };

        public static string GetDescription(this NameParameterWithEnter parameter)
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

        public static string GetDesignation(this NameParameterWithEnter parameter)
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

        public static UnitType GetUnit(this NameParameterWithEnter parameter)
        {
            if (Units.TryGetValue(parameter, out UnitType unitType))
            {
                return unitType;
            }

            throw new ArgumentException(paramName: parameter.ToString(), message: String.Format("Тип для параметра {0} не найден", parameter.ToString()));
        }

        public static double Calculate(this NameParameterWithEnter parameter)
        {
            return 0;
        }

        public static string GetName(this NameParameterWithEnter parameter)
        {
            return Enum.GetName(typeof(NameParameterWithEnter), parameter);
        }

        public static object ToJson(this NameParameterWithEnter parameter)
        {
            return new
            {
                Name = parameter.GetDesignation(),
                Value = parameter.GetName(),
                Description = parameter.GetDescription(),
                Unit = parameter.GetUnit().GetDesignation()
            };
        }

        public static bool AddedToRestrictions(this NameParameterWithEnter parameter)
        {
            
            return StaticData.ConditionsForParameterWithEnter.Keys.Contains(parameter);
        }

        public static void AddToRestrictions(this NameParameterWithEnter parameter, ConditionType conditionType, double value)
        {           
            StaticData.ConditionsForParameterWithEnter.Add(parameter, new Condition(conditionType, value));
            parameter.VerificateParameterForCurrentSystem();
        }
        private static void VerificateParameterForCurrentSystem(this NameParameterWithEnter parameter)
        {
            StaticData.CurrentSystems.ParametersWithEnter[parameter].Verification(out string message);
        }
        public static bool DeleteFromRestrictions(this NameParameterWithEnter parameter)
        {
            if (!StaticData.ConditionsForParameterWithEnter.ContainsKey(parameter))
                return false;

            StaticData.ConditionsForParameterWithEnter.Remove(parameter);
            parameter.VerificateParameterForCurrentSystem();
            return true;
        }
        public static object ToParameter(this NameParameterWithEnter parameter, double? value, bool correct)
        {
            return new
            {
                Name = parameter.GetName(),
                Designation = parameter.GetDesignation(),
                Description = parameter.GetDescription(),
                Unit = parameter.GetUnit().GetDesignation(),
                Value = value.HasValue?value.Value.ToString():"",
                Correct = correct
            };
    
        }
        public static void DeleteAllRestrictions(this NameParameterWithEnter parameter)
        {
            foreach(var parameterWithEnter in StaticData.ConditionsForParameterWithEnter.Keys)
            {
                parameterWithEnter.DeleteFromRestrictions();
            }
        }

        public static object ToRestriction(this NameParameterWithEnter parameter, ConditionType conditionType, double value)
        {
            dynamic result = new ExpandoObject();
            result.Name = parameter.GetDesignation();
            result.Description = parameter.GetDescription();
            result.Unit = parameter.GetUnit().GetDesignation();
            result.Condition = conditionType.GetDesignation();
            result.Value = value;
            result.RestrictionName = parameter.GetName();
            return result;
        }

        /// <summary>
        /// Для вкладки анализа
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static object ToPair(this NameParameterWithEnter parameter)
        {
            return new
            {
                Name = parameter.GetDesignation(),
                Description = parameter.GetDescription(),
                Value = parameter.GetName()
            };
        }

    }

    public class ParameterWithEnter
    {
        [Name("Наименование показателя")]
        public string Description { get { return TypeParameter.GetDescription(); } }

        [Name("Обозначение")]       
        public string Designation { get { return TypeParameter.GetDesignation(); }set { SetParameterType(value); } }

        [Name("Единица измерения")]
        public string Unit {get {return UnitType.GetDesignation();}}

        
        [Name("Значение")]
        public double? Value 
        { 
            get 
            {
                return _value;
            }
            set 
            {
                isCorrect = false; _value = value;
            } 
        }

        [Ignore]
        private double? _value;
        [Ignore]
        public bool isCorrect { get; set; }
        //[Ignore]
        //public string Name { get { return TypeParameter.GetName(); } }

        [Ignore]
        public PropertiesSystem propertiesSystem;

        [Ignore]
        public UnitType UnitType { get { return TypeParameter.GetUnit(); } }

        [Ignore]
        public NameParameterWithEnter TypeParameter { get; set; }

        [Ignore]
        public string Name { get { return TypeParameter.GetName(); } }


        public ParameterWithEnter()
        {

        }
        public ParameterWithEnter(PropertiesSystem _propertiesSystem, NameParameterWithEnter parameter)
        {
            TypeParameter = parameter;
        
            propertiesSystem = _propertiesSystem;

            _propertiesSystem.ParametersWithEnter.Add(TypeParameter, this);
        }

        public bool Verification(out string message)
        {
            isCorrect = true;
            message = "";
            if (!Value.HasValue)
            {
                message = String.Format("Значение параметра {0} не указано", Designation);
                isCorrect = false;
            }
            else
            {
                if(Value.Value <= 0)
                {
                    message = String.Format("Значение параметра {0} должно быть > 0", Designation);
                    isCorrect = false;
                }
                else if (StaticData.ConditionsForParameterWithEnter.TryGetValue(this.TypeParameter, out Condition condition))
                {
                    if (!condition.InvokeComparison(Value.Value))
                    {
                        message = String.Format("Значение параметра {0} должно быть {1}.", Designation, condition.ErrorMessage);
                        isCorrect = false;
                    }
                }
            }
            return isCorrect;
        }

        //public bool EazyVerification()
        //{
        //    if (!Value.HasValue)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        if (Value.Value < 0)
        //        {
        //            return false;
        //        }
        //        else if (StaticData.ConditionsForParameterWithEnter.TryGetValue(this.TypeParameter, out Condition condition))
        //        {
        //            if (!condition.InvokeComparison(Value.Value))
        //            {
        //                return false;
        //            }
        //        }
        //    }
        //    return true;
        //}

        public double? Pow(double y)
        {
            return Value.HasValue ? (double?)Math.Pow(Value.Value, y) : null;
        }

        public void SetParameterType(string designation)
        {

            TypeParameter = HelperEnum.GetValuesWithoutDefault<NameParameterWithEnter>().SingleOrDefault(x => x.GetDesignation() == designation);
            if (!HelperEnum.IsDefault(TypeParameter))
                return;

            throw new ArgumentException(paramName: designation, message: "Неккоректное описание");
        }

        public static double? operator +(ParameterWithEnter c1, double? c2)
        {
            return c1.Value+ c2;
        }
        public static double? operator +(double? c1, ParameterWithEnter c2)
        {
            return c1 + c2.Value;
        }
        public static double? operator *(ParameterWithEnter c1, double? c2)
        {
            return c1.Value * c2;
        }
        public static double? operator *(double? c1, ParameterWithEnter c2)
        {
            return c1 * c2.Value;
        }
        public static double? operator -(ParameterWithEnter c1, double? c2)
        {
            return c1.Value - c2;
        }
        public static double? operator -(double? c1, ParameterWithEnter c2)
        {
            return c1 - c2.Value;
        }
        public static double? operator /(ParameterWithEnter c1, double? c2)
        {
            return c1.Value / c2;
        }
        public static double? operator /(double? c1, ParameterWithEnter c2)
        {
            return c1 / c2.Value;
        }
        public static double? operator *(ParameterWithEnter c1, ParameterWithEnter c2)
        {
            return c1.Value * c2.Value;
        }
        public static double? operator /(ParameterWithEnter c1, ParameterWithEnter c2)
        {
            return c1.Value / c2.Value;
        }
        public static double? operator +(ParameterWithEnter c1, ParameterWithEnter c2)
        {
            return c1.Value + c2.Value;
        }
        public static double? operator -(ParameterWithEnter c1, ParameterWithEnter c2)
        {
            return c1.Value - c2.Value;
        }
    }
}
