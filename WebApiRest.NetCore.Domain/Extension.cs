using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApiRest.NetCore.Domain.Models.Struct;

namespace WebApiRest.NetCore.Domain
{
    public static class Extension
    {
        public static CoordinateStruct ExtConvertToCoordinateStruct(this IEnumerable<decimal> value)
        {
            return
                new CoordinateStruct()
                {
                    Longitude = value.ElementAt(0),
                    Latitude = value.ElementAt(1)
                };
        }

        public static CoordinateStruct ExtConvertToCoordinateStruct(this string value)
        {
            decimal longitude = 0, latitude = 0;

            try
            {
                decimal.TryParse(value.Split(',').ElementAt(0), out longitude);
                decimal.TryParse(value.Split(',').ElementAt(1), out latitude);
            }
            catch { }

            return
                new CoordinateStruct()
                {
                    Longitude = longitude,
                    Latitude = latitude
                };
        }

        public static IEnumerable<decimal> ExtConvertToArray(this CoordinateStruct? value)
        {
            return
                new decimal[] {
                    value?.Longitude ?? 0,
                    value?.Latitude ?? 0
                };
        }

        public static string ExtConvertToString(this CoordinateStruct? value)
        {
            return
                $"{value?.Longitude ?? 0},{value?.Latitude ?? 0}";
        }

        public static DateTime ExtConvertToDateTime(this string value)
        {
            DateTime date;
            DateTime.TryParse(value, out date);

            return date;
        }

        public static string ExtToString(this Exception value, Formatting formatting = Formatting.Indented)
        {
            return
                JsonConvert.SerializeObject(value, formatting);
        }
    }
}