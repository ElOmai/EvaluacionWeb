using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace EvaluacionWeb
{
    public static class Extensores
    {
        public static bool EsNulo(this object obj)
        {
            return obj == null;
        }
        public static int ToInt(this object obj)
        {
            if (obj.EsNulo())
                return 0;
            int.TryParse(obj.ToString(), out int value);
            return value;
        }
        public static Decimal ToDecimal(this object obj)
        {
            Decimal.TryParse(obj.ToString(), out Decimal value);
            return value;
        }
        public static DateTime ToDatetime(this object obj)
        {
            DateTime.TryParse(obj.ToString(), out DateTime value);
            return value;
        }
        static readonly string FECHA_FORMAT = "yyyy-MM-dd";
        public static string ToFormatDate(this DateTime dateTime)
        {
            return dateTime.ToString(FECHA_FORMAT);
        }
        public static string GetDescription<T>(this T e) where T : IConvertible
        {
            if (e is Enum)
            {
                Type type = e.GetType();
                Array values = System.Enum.GetValues(type);

                foreach (int val in values)
                {
                    if (val == e.ToInt32(CultureInfo.InvariantCulture))
                    {
                        var memInfo = type.GetMember(type.GetEnumName(val));

                        if (memInfo[0]
                            .GetCustomAttributes(typeof(DescriptionAttribute), false)
                            .FirstOrDefault() is DescriptionAttribute descriptionAttribute)
                        {
                            return descriptionAttribute.Description;
                        }
                    }
                }
            }
            return null;
        }
    }

}

