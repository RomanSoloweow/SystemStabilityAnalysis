using System;
using System.Collections.Generic;
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
        Lс,
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
            {NameParameterWithEnter.A1, "Количество элементов 1 группы не перешедших во 2 группу из-за недостатков ресурсов "},
            {NameParameterWithEnter.B1, "Количество элементов 1 группы, вышедших из строя  "},
            {NameParameterWithEnter.F1, "Количество элементов 1 группы перешедших во 2 группу "},
            {NameParameterWithEnter.Q2, "Количество элементов 2 группы  перешедших в 3 группу (неисправных, но еще подлежащих восстановлению или пригодных для использования)"},
            {NameParameterWithEnter.D2, "Количество не подлежащих восстановлению элементов 2 группы"},
            {NameParameterWithEnter.H3, "Количество элементов 3 группы перешедших в 1 группу  "},
            {NameParameterWithEnter.Lс, "Количество смен в сутках"},
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
            { NameParameterWithEnter.Lс, UnitType.Day},
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
                Unit = parameter.GetUnit().GetDescription()
            };
        }

        public static void AddToRestrictions(this NameParameterWithEnter parameter, ConditionType conditionType, double value)
        {
            StaticData.ConditionsForParameterWithEnter.Add(parameter, new Condition(conditionType, value));
        }
        public static bool DeleteFromRestrictions(this NameParameterWithEnter parameter)
        {
            if (!StaticData.ConditionsForParameterWithEnter.ContainsKey(parameter))
                return false;

            StaticData.ConditionsForParameterWithEnter.Remove(parameter);

            return true;
        }
        public static object ToParameter(this NameParameterWithEnter parameter, double value, bool correct)
        {
            return new
            {
                Status = Status.Success.GetName(),
                Name = parameter.GetDesignation(),
                Description = parameter.GetDescription(),
                Unit = parameter.GetUnit().GetDescription(),
                Value = value,
                Correct = correct
            };
        }
        public static void DeleteAllRestrictions(this NameParameterWithEnter parameter)
        {
            StaticData.ConditionsForParameterWithEnter.Clear();
        }

        public static object ToRestriction(this NameParameterWithEnter parameter, ConditionType conditionType, double value)
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
    }

    //public enum ParametersName
    //{
    //    NoCorrect = 0,
    //    DeltaT,
    //    S,
    //    R,
    //    N1,
    //    N2,
    //    N3,
    //    P1,
    //    A1,
    //    B1,
    //    F1,
    //    Q2,
    //    D2,
    //    H3,
    //    Lс,
    //    Tс,
    //    R1,
    //    Rv2,
    //    Rcyt1,
    //    Rf1,
    //    R2,
    //    Rcyt2,
    //    Rf2,
    //    R3,
    //    Rcyt3,
    //    Rf3,
    //    Rcyt,
    //    W1,
    //    Wv2,
    //    Wсyt1,
    //    Wf1,
    //    W2,
    //    Wcyt2,
    //    Wf2,
    //    W3,
    //    Wcyt3,
    //    Wf3,
    //    Wcyt,
    //    W,
    //    Smin1,
    //    Smin2,
    //    Smin3,
    //    SminC,
    //    Smin,
    //    Sn1,
    //    Sn2,
    //    Sn3,
    //    S1,
    //    S2,
    //    S3,
    //    Sс,
    //    SN1,
    //    SN2,
    //    SN3,
    //}

    //public static class ParametersExtension
    //{
    //    public static Dictionary<ParametersName, string> Descriptions = new Dictionary<ParametersName, string>()
    //    {
    //        {ParametersName.DeltaT, "Период устойчивой эксплуатации системы в сутках"},
    //        {ParametersName.R, "Стоимость функционирования системы в период ∆T"},
    //        {ParametersName.S, "Количество персонала "},
    //        {ParametersName.N1, "Количество элементов вводимых в эксплуатацию, 1 группа "},
    //        {ParametersName.N2, "Количество элементов выполняющих функции системы, 2 группа "},
    //        {ParametersName.N3, "Количество др. элементов: подсистема обеспечения, резерв,  неисправных и др., 3 группа"},
    //        {ParametersName.P1, "Количество элементов 1 группы, введенных (восстановленных) в эксплуатацию"},
    //        {ParametersName.A1, "Количество элементов 1 группы не перешедших во 2 группу из-за недостатков ресурсов "},
    //        {ParametersName.B1, "Количество элементов 1 группы, вышедших из строя  "},
    //        {ParametersName.F1, "Количество элементов 1 группы перешедших во 2 группу "},
    //        {ParametersName.Q2, "Количество элементов 2 группы  перешедших в 3 группу (неисправных, но еще подлежащих восстановлению или пригодных для использования)"},
    //        {ParametersName.D2, "Количество не подлежащих восстановлению элементов 2 группы"},
    //        {ParametersName.H3, "Количество элементов 3 группы перешедших в 1 группу  "},
    //        {ParametersName.Lс, "Количество смен в сутках"},
    //        {ParametersName.Tс, "Время 1 смены в часах"},
    //        {ParametersName.R1, "Макс. расход ресурсов на 1 элемент 1 группы (Стоимость) в сутки"},
    //        {ParametersName.Rv2, "Макс. расход ресурсов на  восстановление (ремонт) элемента 2 группы (Стоимость) в сутки"},
    //        {ParametersName.Rcyt1, "Расход ресурсов на  1 группу (Стоимость) в сутки"},
    //        {ParametersName.Rf1, "Расход ресурсов на  1 группу (Стоимость) за период функционирования"},
    //        {ParametersName.R2, "Макс. расход ресурсов на  1 элемент 2 группы (Стоимость) в сутки"},
    //        {ParametersName.Rcyt2, "Расход ресурсов на 2 группу (Стоимость) в сутки"},
    //        {ParametersName.Rf2, "Расход ресурсов на  2 группу (Стоимость) за период функционирования"},
    //        {ParametersName.R3, "Макс. расход ресурсов на  1 элемент 3 группы (Стоимость) в сутки"},
    //        {ParametersName.Rcyt3, "Расход ресурсов на 3 группу (Стоимость) в сутки"},
    //        {ParametersName.Rf3, "Расход ресурсов на  3 группу (Стоимость) за период функционирования"},
    //        {ParametersName.Rcyt, "Расход ресурсов (Стоимость) функционирования системы за сутки"},
    //        {ParametersName.W1, "Макс. расход человеко-часов на 1 элемент 1 группы в сутки"},
    //        {ParametersName.Wv2, "Макс. расход человеко-часов на  восстановление (ремонт) элемента 2 группы (Стоимость) в сутки"},
    //        {ParametersName.Wсyt1, "Расход человеко-часов на  1 группу в сутки"},
    //        {ParametersName.Wf1, "Расход человеко-часов на  1 группу за период функционирования"},
    //        {ParametersName.W2, "Макс. расход человеко-часов на  1 элемент 2 группы в сутки"},
    //        {ParametersName.Wcyt2, "Расход человеко-часов на 2 группу в сутки"},
    //        {ParametersName.Wf2, "Расход человеко-часов на  2 группу за период функционирования"},
    //        {ParametersName.W3, "Макс. расход человеко-часов на  1 элемент 3 группы  в сутки"},
    //        {ParametersName.Wcyt3, "Расход человеко-часов на 3 группу в сутки"},
    //        {ParametersName.Wf3, "Расход человеко-часов на  3 группу за период функционирования"},
    //        {ParametersName.Wcyt, "Расход человеко-часов на функционирование системы за сутки"},
    //        {ParametersName.W, "Расход человеко-часов на функционирование системы за период (∆T)"},
    //        {ParametersName.Smin1, "Мин. необходимое количество специалистов в 1 смене для 1 группы"},
    //        {ParametersName.Smin2, "Мин. необходимое количество специалистов в 1 смене для 2 группы"},
    //        {ParametersName.Smin3, "Мин. необходимое количество специалистов в 1 смене для 3 группы"},
    //        {ParametersName.SminC, "Мин. необходимое количество персонала в 1 смене для всей системы"},
    //        {ParametersName.Smin, "Мин. необходимое кол-во персонала для всей системы на период ∆T"},
    //        {ParametersName.Sn1, "Прогнозируемое кол-во потерь специалистов 1 группы за одну смену"},
    //        {ParametersName.Sn2, "Прогнозируемое кол-во потерь специалистов 2 группы за одну смену"},
    //        {ParametersName.Sn3, "Прогнозируемое кол-во потерь специалистов 3 группы за одну смену"},
    //        {ParametersName.S1, "Необходимое количество специалистов в  одной смене для 1 группы"},
    //        {ParametersName.S2, "Необходимое количество специалистов в  одной смене для 2 группы"},
    //        {ParametersName.S3, "Необходимое количество специалистов в одной смене для 3 группы"},
    //        {ParametersName.Sс, "Необходимое количество персонала в одной смене для всей системы"},
    //        {ParametersName.SN1, "Из этого количества: в ремонтно-восстановительные формирования"},
    //        {ParametersName.SN2, "Непосредственно для обеспечения функционирования (выполнения основных функций) системы"},
    //        {ParametersName.SN3, "Для подсистемы обеспечения подсистемы хранения запасов и резервирования"}
    //    };

    //    public static Dictionary<ParametersName, string> Designations = new Dictionary<ParametersName, string>()
    //    {
    //        {ParametersName.DeltaT, "∆T"},
    //        {ParametersName.Rv2, "Rв2"},
    //        {ParametersName.Rcyt1, "Rсут1"},
    //        {ParametersName.Rf1, "Rф1"},
    //        {ParametersName.Rcyt2, "Rсут2"},
    //        {ParametersName.Rf2, "Rф2"},
    //        {ParametersName.Rcyt3, "Rсут3"},
    //        {ParametersName.Rf3, "Rф3"},
    //        {ParametersName.Rcyt, "Rсут"},
    //        {ParametersName.Wv2, "Wв2"},
    //        {ParametersName.Wсyt1, "Wсут1"},
    //        {ParametersName.Wf1, "Wф1"},
    //        {ParametersName.Wcyt2, "Wcyt2"},
    //        {ParametersName.Wf2, "Wф2"},
    //        {ParametersName.Wcyt3, "Wcyt3"},
    //        {ParametersName.Wf3, "Wф3"},
    //        {ParametersName.Wcyt, "Wсут"}
    //    };

    //    public static Dictionary<ParametersName, UnitType> Units = new Dictionary<ParametersName, UnitType>()
    //    {
    //        { ParametersName.DeltaT, UnitType.Point},
    //        { ParametersName.N1, UnitType.Point},
    //        { ParametersName.N2, UnitType.Point},
    //        { ParametersName.N3, UnitType.Point},
    //        { ParametersName.P1, UnitType.Point},
    //        { ParametersName.A1, UnitType.Point},
    //        { ParametersName.B1, UnitType.Point},
    //        { ParametersName.F1, UnitType.Point},
    //        { ParametersName.Q2, UnitType.Point},
    //        { ParametersName.D2, UnitType.Point},
    //        { ParametersName.H3, UnitType.Point},
    //        { ParametersName.Lс, UnitType.Day},
    //        { ParametersName.Tс, UnitType.Hour},
    //        { ParametersName.R1, UnitType.ThousandRubles},
    //        { ParametersName.Rv2, UnitType.ThousandRubles},
    //        { ParametersName.Rcyt1, UnitType.ThousandRubles},
    //        { ParametersName.Rf1, UnitType.ThousandRubles},
    //        { ParametersName.R2, UnitType.ThousandRubles},
    //        { ParametersName.Rcyt2, UnitType.ThousandRubles},
    //        { ParametersName.Rf2, UnitType.ThousandRubles},
    //        { ParametersName.R3, UnitType.ThousandRubles},
    //        { ParametersName.Rcyt3, UnitType.ThousandRubles},
    //        { ParametersName.Rf3, UnitType.ThousandRubles},
    //        { ParametersName.Rcyt, UnitType.ThousandRubles},
    //        { ParametersName.R, UnitType.ThousandRubles},
    //        { ParametersName.W1, UnitType.ManHour},
    //        { ParametersName.Wv2, UnitType.ManHour},
    //        { ParametersName.Wсyt1, UnitType.ManHour},
    //        { ParametersName.Wf1, UnitType.ManHour},
    //        { ParametersName.W2, UnitType.ManHour},
    //        { ParametersName.Wcyt2, UnitType.ManHour},
    //        { ParametersName.Wf2, UnitType.ManHour},
    //        { ParametersName.W3, UnitType.ManHour},
    //        { ParametersName.Wcyt3, UnitType.ManHour},
    //        { ParametersName.Wf3, UnitType.ManHour},
    //        { ParametersName.Wcyt, UnitType.ManHour},
    //        { ParametersName.W, UnitType.ManHour},
    //        { ParametersName.Smin1, UnitType.Point},
    //        { ParametersName.Smin2, UnitType.Point},
    //        { ParametersName.Smin3, UnitType.Point},
    //        { ParametersName.SminC, UnitType.Point},
    //        { ParametersName.Smin, UnitType.Point},
    //        { ParametersName.Sn1, UnitType.Point},
    //        { ParametersName.Sn2, UnitType.Point},
    //        { ParametersName.Sn3, UnitType.Point},
    //        { ParametersName.S1, UnitType.Point},
    //        { ParametersName.S2, UnitType.Point},
    //        { ParametersName.S3, UnitType.Point},
    //        { ParametersName.Sс, UnitType.Point},
    //        { ParametersName.S, UnitType.Point},
    //        { ParametersName.SN1, UnitType.Point},
    //        { ParametersName.SN2, UnitType.Point},
    //        { ParametersName.SN3, UnitType.Point}
    //    };

    //    public static string GetDescription(this ParametersName parameter)
    //    {
    //        if (Descriptions.TryGetValue(parameter, out string description))
    //        {
    //            return description;
    //        }
    //        else
    //        {
    //            return parameter.ToString();
    //        }
    //    }

    //    public static string GetDesignation(this ParametersName parameter)
    //    {
    //        if(Designations.TryGetValue(parameter, out string designation))
    //        {
    //            return designation;
    //        }
    //        else
    //        {
    //            return parameter.ToString();
    //        }
    //    }

    //    public static UnitType GetUnit(this ParametersName parameter)
    //    {
    //        if (Units.TryGetValue(parameter, out UnitType unitType))
    //        {
    //            return unitType;
    //        }

    //        throw new ArgumentException(paramName: parameter.ToString(), message: String.Format("Тип для параметра {0} не найден", parameter.ToString()));
    //    }

    //    public static double Calculate(this ParametersName parameter)
    //    {
    //        return 0;
    //    }

    //    public static string GetName(this ParametersName parameter)
    //    {
    //        return Enum.GetName(typeof(ParametersName), parameter);
    //    }

    //    public static object ToJson(this ParametersName parameter)
    //    {
    //        return new
    //        {
    //            Name = parameter.GetDesignation(),
    //            Value = parameter.GetName(),
    //            Description = parameter.GetDescription(),          
    //            Unit = parameter.GetUnit().GetDescription()
    //        };
    //    }
    //}

    public class ResultVerification
    {
        public bool IsCorrect;

        public List<string> ErrorMessages = new List<string>(); 
    }

    public class ParameterWithEnter
    {

        public string Name { get { return  TypeParameter.GetName(); } }

        public string Description { get { return TypeParameter.GetDescription(); } }

        public string Designation { get { return TypeParameter.GetDesignation(); } }

        public Unit Unit { get; }

        public NameParameterWithEnter TypeParameter { get; }

        public double Value { get; set; }

        public ParameterWithEnter(PropertiesSystem propertiesSystem, NameParameterWithEnter parameter)
        {
            TypeParameter = parameter;

            Unit = new Unit(TypeParameter.GetUnit());

            propertiesSystem.ParametersWithEnter.Add(TypeParameter, this);
        }

        public ResultVerification Verification()
        {
            ResultVerification result = new ResultVerification() { IsCorrect = true };

            if(!(Value>0))
            {
                result.IsCorrect = false;
                result.ErrorMessages.Add(String.Format("Значение параметра {0} должно быть > 0",Name));
            }

            if (StaticData.ConditionsForParameterWithEnter.TryGetValue(this.TypeParameter, out Condition condition))
            {
                result.IsCorrect = condition.InvokeComparison(Value);
                if (!result.IsCorrect)
                {
                    result.ErrorMessages.Add(Description + " " + condition.ErrorMessage);
                }
            }

            return result;
        }


        public static double operator +(ParameterWithEnter c1, double c2)
        {
            return c1.Value+ c2;
        }
        public static double operator +(double  c1, ParameterWithEnter c2)
        {
            return c1 + c2.Value;
        }

        public static double operator *(ParameterWithEnter c1, double c2)
        {
            return c1.Value * c2;
        }
        public static double operator *(double c1, ParameterWithEnter c2)
        {
            return c1 * c2.Value;
        }

        public static double operator -(ParameterWithEnter c1, double c2)
        {
            return c1.Value - c2;
        }
        public static double operator -(double c1, ParameterWithEnter c2)
        {
            return c1 - c2.Value;
        }

        public static double operator /(ParameterWithEnter c1, double c2)
        {
            return c1.Value / c2;
        }
        public static double operator /(double c1, ParameterWithEnter c2)
        {
            return c1 / c2.Value;
        }

        public static double operator *(ParameterWithEnter c1, ParameterWithEnter c2)
        {
            return c1.Value * c2.Value;
        }
        public static double operator /(ParameterWithEnter c1, ParameterWithEnter c2)
        {
            return c1.Value / c2.Value;
        }
        public static double operator +(ParameterWithEnter c1, ParameterWithEnter c2)
        {
            return c1.Value + c2.Value;
        }
        public static double operator -(ParameterWithEnter c1, ParameterWithEnter c2)
        {
            return c1.Value - c2.Value;
        }
    }
}
