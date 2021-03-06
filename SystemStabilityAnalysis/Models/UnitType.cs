﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace SystemStabilityAnalysis.Helpers
{
    public enum UnitType
    {
        NoCorrect = 0,
        NoType,
        /// <summary>
        /// Человек
        /// </summary>
        Man,
        /// <summary>
        /// Сутки
        /// </summary>
        Day,
        /// <summary>
        /// Штука
        /// </summary>
        Point,
        /// <summary>
        /// Тысяча рублей
        /// </summary>
        ThousandRubles,
        /// <summary>
        /// Человек-час
        /// </summary>
        ManHour,
        /// <summary>
        /// Смена
        /// </summary>
        Shift,
        /// <summary>
        /// Час
        /// </summary>
        Hour
    }

    public static class UnitTypeExtension
    {
        public static Dictionary<UnitType, string> Designations = new Dictionary<UnitType, string>()
        {
            {UnitType.NoType, "_" },
            {UnitType.Man, "чел." },
            {UnitType.Day, "сут." },
            {UnitType.Point, "шт." },
            {UnitType.ThousandRubles, "т.р." },
            {UnitType.ManHour, "чел.ч." },
            {UnitType.Shift, "смена"},
            {UnitType.Hour, "час" }
        };

        public static string GetDesignation(this UnitType parameter)
        {
            if (Designations.TryGetValue(parameter, out string description))
            {
                return description;
            }

            throw new ArgumentException(paramName: parameter.ToString(), message: String.Format("Описание для параметра {0} не найдено", parameter.ToString()));
        }
    }

}
