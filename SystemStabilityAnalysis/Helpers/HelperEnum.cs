using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SystemStabilityAnalysis.Helpers
{
    public static class HelperEnum
    {
        public static bool IsEnumType(this Type enumType)
        {
            if (enumType == null)
                return false;

            if (!enumType.IsEnum)
                return false;

            return true;

        }

        public static bool Contains(Type enumType, string value, bool Exeption = true)
        {
            if ((!Exeption) && (!IsEnumType(enumType))) return false;

            if (string.IsNullOrEmpty(value)) return false;

            return Enum.IsDefined(enumType, value);

        }

        public static string GetName<TEnum>(TEnum value) where TEnum : struct
        {
            return Enum.GetName(typeof(TEnum), value);
        }

        public static List<string> GetNames(Type enumType, bool Exeption = true)
        {
            if ((!Exeption) && (!IsEnumType(enumType))) return null;

            return Enum.GetNames(enumType).ToList();
        }

        public static List<TEnum> GetValues<TEnum>(bool Exeption = true)
        {
            if ((!Exeption) && (!IsEnumType(typeof(TEnum))))
                return null;
            return Enum.GetValues(typeof(TEnum)).OfType<TEnum>().ToList();
        }
        public static List<TEnum> GetValuesWithoutDefault<TEnum>(bool Exeption = true)
        {
            //Code: Enum.GetName(enumType, 0) - get defult name for Enum
            return GetValues<TEnum>(Exeption)?.Where(x => !x.Equals(default(TEnum))).ToList();
        }

        public static List<string> GetNamesWithoutDefault(Type enumType, bool Exeption = true)
        {
            //Code: Enum.GetName(enumType, 0) - get defult name for Enum
            return GetNames(enumType, Exeption)?.Where(name => name != Enum.GetName(enumType, 0)).ToList();
        }

        public static TEnum GetValue<TEnum>(string value, bool ignoreCase = true, bool Exeption = true) where TEnum : struct
        {
            Type typeEnum = typeof(TEnum);

            if (!Contains(typeEnum, value, Exeption)) return default(TEnum);

            return (TEnum)Enum.Parse(typeEnum, value, ignoreCase);
        }


        public static bool TryGetValue<TEnum>(string value, out TEnum result, bool ignoreCase = true, bool Exeption = true) where TEnum : struct
        {
            Type typeEnum = typeof(TEnum);
            result = default(TEnum);

            if (!Contains(typeEnum, value, Exeption)) return false;

            return Enum.TryParse(value, out result);
        }

        public static Dictionary<string, TEnum> ToDictionary<TEnum>(bool withoutDefault) where TEnum : struct
        {
            return (withoutDefault ? GetValuesWithoutDefault<TEnum>() : GetValuesWithoutDefault<TEnum>()).ToDictionary(x => HelperEnum.GetName(x), x => x);
        }

        public static bool IsDefault<TEnum>(TEnum value) where TEnum : struct
        {
            return  object.Equals(value, default(TEnum));
        }
    }
}
