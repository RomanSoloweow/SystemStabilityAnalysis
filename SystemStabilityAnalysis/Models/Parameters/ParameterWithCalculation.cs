using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SystemStabilityAnalysis.Models
{
    //public enum NameParameterWithCalculation
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
    //    R2,
    //    R3,
    //    W1,
    //    Wv2,
    //    W2,
    //    W3,
    //    Sn1,
    //    Sn2,
    //    Sn3,
    //}

    //public static class ParameterWithCalculationExtension
    //{
    //    public static Dictionary<NameParameterWithCalculation, string> Descriptions = new Dictionary<NameParameterWithCalculation, string>()
    //    {
    //        {NameParameterWithRestriction.DeltaT, "Период устойчивой эксплуатации системы в сутках"},
    //        {NameParameterWithRestriction.S, "Количество персонала "},
    //        {NameParameterWithRestriction.R, "Стоимость функционирования системы в период ∆T"},
    //        {NameParameterWithRestriction.N1, "Количество элементов вводимых в эксплуатацию, 1 группа "},
    //        {NameParameterWithRestriction.N2, "Количество элементов выполняющих функции системы, 2 группа "},
    //        {NameParameterWithRestriction.N3, "Количество др. элементов: подсистема обеспечения, резерв,  неисправных и др., 3 группа"},
    //        {NameParameterWithRestriction.P1, "Количество элементов 1 группы, введенных (восстановленных) в эксплуатацию"},
    //        {NameParameterWithRestriction.A1, "Количество элементов 1 группы не перешедших во 2 группу из-за недостатков ресурсов "},
    //        {NameParameterWithRestriction.B1, "Количество элементов 1 группы, вышедших из строя  "},
    //        {NameParameterWithRestriction.F1, "Количество элементов 1 группы перешедших во 2 группу "},
    //        {NameParameterWithRestriction.Q2, "Количество элементов 2 группы  перешедших в 3 группу (неисправных, но еще подлежащих восстановлению или пригодных для использования)"},
    //        {NameParameterWithRestriction.D2, "Количество не подлежащих восстановлению элементов 2 группы"},
    //        {NameParameterWithRestriction.H3, "Количество элементов 3 группы перешедших в 1 группу  "},
    //        {NameParameterWithRestriction.Lс, "Количество смен в сутках"},
    //        {NameParameterWithRestriction.Tс, "Время 1 смены в часах"},
    //        {NameParameterWithRestriction.R1, "Макс. расход ресурсов на 1 элемент 1 группы (Стоимость) в сутки"},
    //        {NameParameterWithRestriction.Rv2, "Макс. расход ресурсов на  восстановление (ремонт) элемента 2 группы (Стоимость) в сутки"},
    //        {NameParameterWithRestriction.R2, "Макс. расход ресурсов на  1 элемент 2 группы (Стоимость) в сутки"},
    //        {NameParameterWithRestriction.R3, "Макс. расход ресурсов на  1 элемент 3 группы (Стоимость) в сутки"},
    //        {NameParameterWithRestriction.W1, "Макс. расход человеко-часов на 1 элемент 1 группы в сутки"},
    //        {NameParameterWithRestriction.Wv2, "Макс. расход человеко-часов на  восстановление (ремонт) элемента 2 группы (Стоимость) в сутки"},
    //        {NameParameterWithRestriction.W2, "Макс. расход человеко-часов на  1 элемент 2 группы в сутки"},
    //        {NameParameterWithRestriction.W3, "Макс. расход человеко-часов на  1 элемент 3 группы  в сутки"},
    //        {NameParameterWithRestriction.Sn1, "Прогнозируемое кол-во потерь специалистов 1 группы за одну смену"},
    //        {NameParameterWithRestriction.Sn2, "Прогнозируемое кол-во потерь специалистов 2 группы за одну смену"},
    //        {NameParameterWithRestriction.Sn3, "Прогнозируемое кол-во потерь специалистов 3 группы за одну смену"},
    //    };

    //    public static Dictionary<NameParameterWithCalculation, string> Designations = new Dictionary<NameParameterWithCalculation, string>()
    //    {
    //        {NameParameterWithCalculation.DeltaT, "∆T"},
    //        {NameParameterWithCalculation.Rv2, "Rв2"},
    //        {NameParameterWithCalculation.Wv2, "Wв2"},

    //    };

    //    public static Dictionary<NameParameterWithCalculation, UnitType> Units = new Dictionary<NameParameterWithCalculation, UnitType>()
    //    {
    //        { NameParameterWithCalculation.DeltaT, UnitType.Point},
    //        { NameParameterWithCalculation.S, UnitType.Man},
    //        { NameParameterWithCalculation.R, UnitType.ThousandRubles},
    //        { NameParameterWithCalculation.N1, UnitType.Point},
    //        { NameParameterWithCalculation.N2, UnitType.Point},
    //        { NameParameterWithCalculation.N3, UnitType.Point},
    //        { NameParameterWithCalculation.P1, UnitType.Point},
    //        { NameParameterWithCalculation.A1, UnitType.Point},
    //        { NameParameterWithCalculation.B1, UnitType.Point},
    //        { NameParameterWithRestriction.F1, UnitType.Point},
    //        { NameParameterWithRestriction.Q2, UnitType.Point},
    //        { NameParameterWithRestriction.D2, UnitType.Point},
    //        { NameParameterWithRestriction.H3, UnitType.Point},
    //        { NameParameterWithRestriction.Lс, UnitType.Day},
    //        { NameParameterWithRestriction.Tс, UnitType.Hour},
    //        { NameParameterWithRestriction.R1, UnitType.ThousandRubles},
    //        { NameParameterWithRestriction.Rv2, UnitType.ThousandRubles},
    //        { NameParameterWithRestriction.R2, UnitType.ThousandRubles},
    //        { NameParameterWithRestriction.R3, UnitType.ThousandRubles},
    //        { NameParameterWithRestriction.W1, UnitType.ManHour},
    //        { NameParameterWithRestriction.Wv2, UnitType.ManHour},
    //        { NameParameterWithRestriction.W2, UnitType.ManHour},
    //        { NameParameterWithRestriction.W3, UnitType.ManHour},
    //        { NameParameterWithRestriction.Sn1, UnitType.Man},
    //        { NameParameterWithRestriction.Sn2, UnitType.Man},
    //        { NameParameterWithRestriction.Sn3, UnitType.Man},
    //    };

    //    public static string GetDescription(this NameParameterWithRestriction parameter)
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

    //    public static string GetDesignation(this NameParameterWithRestriction parameter)
    //    {
    //        if (Designations.TryGetValue(parameter, out string designation))
    //        {
    //            return designation;
    //        }
    //        else
    //        {
    //            return parameter.ToString();
    //        }
    //    }

    //    public static UnitType GetUnit(this NameParameterWithRestriction parameter)
    //    {
    //        if (Units.TryGetValue(parameter, out UnitType unitType))
    //        {
    //            return unitType;
    //        }

    //        throw new ArgumentException(paramName: parameter.ToString(), message: String.Format("Тип для параметра {0} не найден", parameter.ToString()));
    //    }

    //    public static double Calculate(this NameParameterWithRestriction parameter)
    //    {
    //        return 0;
    //    }

    //    public static string GetName(this NameParameterWithRestriction parameter)
    //    {
    //        return Enum.GetName(typeof(NameParameterWithRestriction), parameter);
    //    }

    //    public static object ToJson(this NameParameterWithRestriction parameter)
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
}
