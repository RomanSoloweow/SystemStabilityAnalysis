using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemStabilityAnalysis.Helpers;

namespace SystemStabilityAnalysis.Models
{

    public struct ResultVerification
    {
        public bool IsCorrect;
        public List<string> ErrorMessages;
    }

    public enum ParameterName
    {
        NoCorrect = 0,
        deltaT,
        S,
        R,
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
        Rcyt1,
        Rf1,
        R2,
        Rcyt2,
        Rf2,
        R3,
        Rcyt3,
        Rf3,
        Rcyt,
        W1,
        Wv2,
        Wсyt1,
        Wf1,
        W2,
        Wcyt2,
        Wf2,
        W3,
        Wcyt3,
        Wf3,
        Wcyt,
        W,
        Smin1,
        Smin2,
        Smin3,
        SminC,
        Smin,
        Sn1,
        Sn2,
        Sn3,
        S1,
        S2,
        S3,
        Sс,
        SN1,
        SN2,
        SN3,
    }

    public static class ParametersExtension
    {
        public static Dictionary<ParameterName, string> Descriptions = new Dictionary<ParameterName, string>()
        {
            {ParameterName.deltaT, "Период устойчивой эксплуатации системы в сутках"},
            {ParameterName.R, "Стоимость функционирования системы в период ∆T"},
            {ParameterName.S, "Количество персонала "},
            {ParameterName.N1, "Количество элементов вводимых в эксплуатацию, 1 группа "},
            {ParameterName.N2, "Количество элементов выполняющих функции системы, 2 группа "},
            {ParameterName.N3, "Количество др. элементов: подсистема обеспечения, резерв,  неисправных и др., 3 группа"},
            {ParameterName.P1, "Количество элементов 1 группы, введенных (восстановленных) в эксплуатацию"},
            {ParameterName.A1, "Количество элементов 1 группы не перешедших во 2 группу из-за недостатков ресурсов "},
            {ParameterName.B1, "Количество элементов 1 группы, вышедших из строя  "},
            {ParameterName.F1, "Количество элементов 1 группы перешедших во 2 группу "},
            {ParameterName.Q2, "Количество элементов 2 группы  перешедших в 3 группу (неисправных, но еще подлежащих восстановлению или пригодных для использования)"},
            {ParameterName.D2, "Количество не подлежащих восстановлению элементов 2 группы"},
            {ParameterName.H3, "Количество элементов 3 группы перешедших в 1 группу  "},
            {ParameterName.Lс, "Количество смен в сутках"},
            {ParameterName.Tс, "Время 1 смены в часах"},
            {ParameterName.R1, "Макс. расход ресурсов на 1 элемент 1 группы (Стоимость) в сутки"},
            {ParameterName.Rv2, "Макс. расход ресурсов на  восстановление (ремонт) элемента 2 группы (Стоимость) в сутки"},
            {ParameterName.Rcyt1, "Расход ресурсов на  1 группу (Стоимость) в сутки"},
            {ParameterName.Rf1, "Расход ресурсов на  1 группу (Стоимость) за период функционирования"},
            {ParameterName.R2, "Макс. расход ресурсов на  1 элемент 2 группы (Стоимость) в сутки"},
            {ParameterName.Rcyt2, "Расход ресурсов на 2 группу (Стоимость) в сутки"},
            {ParameterName.Rf2, "Расход ресурсов на  2 группу (Стоимость) за период функционирования"},
            {ParameterName.R3, "Макс. расход ресурсов на  1 элемент 3 группы (Стоимость) в сутки"},
            {ParameterName.Rcyt3, "Расход ресурсов на 3 группу (Стоимость) в сутки"},
            {ParameterName.Rf3, "Расход ресурсов на  3 группу (Стоимость) за период функционирования"},
            {ParameterName.Rcyt, "Расход ресурсов (Стоимость) функционирования системы за сутки"},
            {ParameterName.W1, "Макс. расход человеко-часов на 1 элемент 1 группы в сутки"},
            {ParameterName.Wv2, "Макс. расход человеко-часов на  восстановление (ремонт) элемента 2 группы (Стоимость) в сутки"},
            {ParameterName.Wсyt1, "Расход человеко-часов на  1 группу в сутки"},
            {ParameterName.Wf1, "Расход человеко-часов на  1 группу за период функционирования"},
            {ParameterName.W2, "Макс. расход человеко-часов на  1 элемент 2 группы в сутки"},
            {ParameterName.Wcyt2, "Расход человеко-часов на 2 группу в сутки"},
            {ParameterName.Wf2, "Расход человеко-часов на  2 группу за период функционирования"},
            {ParameterName.W3, "Макс. расход человеко-часов на  1 элемент 3 группы  в сутки"},
            {ParameterName.Wcyt3, "Расход человеко-часов на 3 группу в сутки"},
            {ParameterName.Wf3, "Расход человеко-часов на  3 группу за период функционирования"},
            {ParameterName.Wcyt, "Расход человеко-часов на функционирование системы за сутки"},
            {ParameterName.W, "Расход человеко-часов на функционирование системы за период (∆T)"},
            {ParameterName.Smin1, "Мин. необходимое количество специалистов в 1 смене для 1 группы"},
            {ParameterName.Smin2, "Мин. необходимое количество специалистов в 1 смене для 2 группы"},
            {ParameterName.Smin3, "Мин. необходимое количество специалистов в 1 смене для 3 группы"},
            {ParameterName.SminC, "Мин. необходимое количество персонала в 1 смене для всей системы"},
            {ParameterName.Smin, "Мин. необходимое кол-во персонала для всей системы на период ∆T"},
            {ParameterName.Sn1, "Прогнозируемое кол-во потерь специалистов 1 группы за одну смену"},
            {ParameterName.Sn2, "Прогнозируемое кол-во потерь специалистов 2 группы за одну смену"},
            {ParameterName.Sn3, "Прогнозируемое кол-во потерь специалистов 3 группы за одну смену"},
            {ParameterName.S1, "Необходимое количество специалистов в  одной смене для 1 группы"},
            {ParameterName.S2, "Необходимое количество специалистов в  одной смене для 2 группы"},
            {ParameterName.S3, "Необходимое количество специалистов в одной смене для 3 группы"},
            {ParameterName.Sс, "Необходимое количество персонала в одной смене для всей системы"},
            {ParameterName.SN1, "Из этого количества: в ремонтно-восстановительные формирования"},
            {ParameterName.SN2, "Непосредственно для обеспечения функционирования (выполнения основных функций) системы"},
            {ParameterName.SN3, "Для подсистемы обеспечения подсистемы хранения запасов и резервирования"}
        };

        public static Dictionary<ParameterName, string> Calculations = new Dictionary<ParameterName, string>()
        {
            {ParameterName.deltaT, "Описание"},
            {ParameterName.R, "Описание"},
            {ParameterName.S, "Описание"},
            {ParameterName.N1, "Описание"},
            {ParameterName.N2, "Описание"},
            {ParameterName.N3, "Описание"},
            {ParameterName.P1, "Описание"},
            {ParameterName.A1, "Описание"},
            {ParameterName.B1, "Описание"},
            {ParameterName.F1, "Описание"},
            {ParameterName.Q2, "Описание"},
            {ParameterName.D2, "Описание"},
            {ParameterName.H3, "Описание"},
            {ParameterName.Lс, "Описание"},
            {ParameterName.Tс, "Описание"},
            {ParameterName.R1, "Описание"},
            {ParameterName.Rv2, "Описание"},
            {ParameterName.Rcyt1, "Описание"},
            {ParameterName.Rf1, "Описание"},
            {ParameterName.R2, "Описание"},
            {ParameterName.Rcyt2, "Описание"},
            {ParameterName.Rf2, "Описание"},
            {ParameterName.R3, "Описание"},
            {ParameterName.Rcyt3, "Описание"},
            {ParameterName.Rf3, "Описание"},
            {ParameterName.Rcyt, "Описание"},
            {ParameterName.W1, "Описание"},
            {ParameterName.Wv2, "Описание"},
            {ParameterName.Wсyt1, "Описание"},
            {ParameterName.Wf1, "Описание"},
            {ParameterName.W2, "Описание"},
            {ParameterName.Wcyt2, "Описание"},
            {ParameterName.Wf2, "Описание"},
            {ParameterName.W3, "Описание"},
            {ParameterName.Wcyt3, "Описание"},
            {ParameterName.Wf3, "Описание"},
            {ParameterName.Wcyt, "Описание"},
            {ParameterName.W, "Описание"},
            {ParameterName.Smin1, "Описание"},
            {ParameterName.Smin2, "Описание"},
            {ParameterName.Smin3, "Описание"},
            {ParameterName.SminC, "Описание"},
            {ParameterName.Smin, "Описание"},
            {ParameterName.Sn1, "Описание"},
            {ParameterName.Sn2, "Описание"},
            {ParameterName.Sn3, "Описание"},
            {ParameterName.S1, "Описание"},
            {ParameterName.S2, "Описание"},
            {ParameterName.S3, "Описание"},
            {ParameterName.Sс, "Описание"},
            {ParameterName.SN1, "Описание"},
            {ParameterName.SN2, "Описание"},
            {ParameterName.SN3, "Описание"}
        };

        public static Dictionary<ParameterName, string> Designations = new Dictionary<ParameterName, string>()
        {
            {ParameterName.deltaT, "∆T"},
            {ParameterName.Rv2, "Rв2"},
            {ParameterName.Rcyt1, "Rсут1"},
            {ParameterName.Rf1, "Rф1"},
            {ParameterName.Rcyt2, "Rсут2"},
            {ParameterName.Rf2, "Rф2"},
            {ParameterName.Rcyt3, "Rсут3"},
            {ParameterName.Rf3, "Rф3"},
            {ParameterName.Rcyt, "Rсут"},
            {ParameterName.Wv2, "Wв2"},
            {ParameterName.Wсyt1, "Wсут1"},
            {ParameterName.Wf1, "Wф1"},
            {ParameterName.Wcyt2, "Wcyt2"},
            {ParameterName.Wf2, "Wф2"},
            {ParameterName.Wcyt3, "Wcyt3"},
            {ParameterName.Wf3, "Wф3"},
            {ParameterName.Wcyt, "Wсут"}
        };
        
        public static string GetDescription(this ParameterName parameter)
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

        public static string GetDesignation(this ParameterName parameter)
        {
            if(Designations.TryGetValue(parameter, out string designation))
            {
                return designation;
            }
            else
            {
                return parameter.ToString();
            }
        }

        public static double Calculate(this ParameterName parameter)
        {
            return 0;
        }
    }

    public class Property
    {
        public Unit Unit { get; }

        public ParameterName Name { get; }

        public Property(ParameterName parameter, UnitType unitType)
        {
           Unit = new Unit(unitType);
           Name = parameter;
        }

        public Properties Properties { get; }

        public string Description { get { return Name.GetDescription(); } }

        public double Value { get; set; }

        public Func<double, double> Calculate { get; }

        //public double Calculate()
        //{
        //    return StaticData.Calculations[this.Name].Invoke(this.Value);
        //}

        public ResultVerification Verification()
        {
            ResultVerification result = new ResultVerification() { IsCorrect = true };
            if (StaticData.Conditions.TryGetValue(this.Name, out List<Condition> conditions))
            {
                foreach(Condition condition in conditions)
                {
                    result.IsCorrect = result.IsCorrect & condition.InvokeComparison(Value);

                    if(!result.IsCorrect)
                    {
                        result.ErrorMessages.Add(Description + " " + condition.ErrorMessage);
                    }
                }
            }

            return result;
        }
    }
}
