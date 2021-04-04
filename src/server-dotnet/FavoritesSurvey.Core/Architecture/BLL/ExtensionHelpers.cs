using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace FavoritesSurvey.Core.Architecture.BLL
{
    public static class ExtensionHelpers
    {
        public static DateTime ToDateTime(this string s, bool timestampFormat = false)
        {
            DateTime result;
            var culture = CultureInfo.CurrentCulture;
            var style = DateTimeStyles.None;
            string[] formats = { "MM/dd/yyyy", "MM-dd-yyyy" };

            if (!timestampFormat)
                DateTime.TryParseExact(s, formats, culture, style, out result);
            else
                DateTime.TryParse(s, out result);

            return result;
        }

        public static string ToDateTimeString(this DateTime d)
        {
            return d.ToString("MM/dd/yyyy HH:mm:ss.fff", CultureInfo.InvariantCulture);
        }

        public static string ToMonthDayYearString(this DateTime d)
        {
            return d.ToString("MM/dd/yyyy");
        }

        public static string ToMonthYearString(this DateTime d)
        {
            return d.ToString("MM/yyyy");
        }

        public static string ToDateNameString(this DateTime d)
        {
            return d.ToString("MMMM dd, yyyy");
        }

        public static double ToUnixTimestamp(this DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan diff = date.ToUniversalTime() - origin;
            return Math.Floor(diff.TotalMilliseconds);
        }

        public static string ToRowVersionString(this byte[] rowVersion)
        {
            return Convert.ToBase64String(rowVersion);
        }

        public static void CopyRowVersion(this byte[] target, string vmRowVersion)
        {
            Convert.FromBase64String(vmRowVersion.Replace(" ", "+")).CopyTo(target, 0);
        }

        public static Dictionary<TKey, TValue> ToDictionarySafe<TSource, TKey, TValue>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TValue> valueSelector)
        {
            var dict = new Dictionary<TKey, TValue>();

            if (source == null)
                return dict;

            foreach(var e in source)
            {
                if (e == null)
                    continue;

                var key = keySelector(e);
                if (!dict.ContainsKey(key))
                    dict.Add(key, valueSelector(e));
            }

            return dict;
        }

        public static Dictionary<TKey, List<TValue>> ToDictionaryGroup<TSource, TKey, TValue>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TValue> valueSelector)
        {
            var dict = new Dictionary<TKey, List<TValue>>();

            if (source == null)
                return dict;

            foreach (var e in source)
            {
                if (e == null)
                    continue;

                var key = keySelector(e);

                List<TValue> list = null;

                if (!dict.TryGetValue(key, out list))
                {
                    list = new List<TValue>();
                    dict.Add(key, list);

                }

                list.Add(valueSelector(e));
            }

            return dict;
        }

    }
}
