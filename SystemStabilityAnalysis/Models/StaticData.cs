using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemStabilityAnalysis.Helpers;

namespace SystemStabilityAnalysis.Models
{
    public static class StaticData
    {

        public static Dictionary<string, SystemForAnalys> Systems { get; set; } = new Dictionary<string, SystemForAnalys>();

        public static Dictionary<string, Condition> Conditions { get; set; } = new Dictionary<string, Condition>();

        //public static Dictionary<string, Condition> OtherConditions { get; set; } = new Dictionary<string, Condition>()
        //{
            //Сюда добавить встроенная ограничения
        //}
    }
}
