using System.Collections.Generic;
using System;
using System.Runtime.CompilerServices;

namespace DZ_14
{
    public static class TExtensionForIEnumerable
    {
        private static void CheckAttributes<T>(IEnumerable<T> ? data, int percent) 
        {
            if (data is null)
            {
                throw new ArgumentNullException($"Коллекция не должна быть равна null.");
            }
            if (percent < 1)
            {
                throw new ArgumentException($"Аргумент percent = {percent} должен быть больше 1.");
            }
            if (percent > 100)
            {
                throw new ArgumentException($"Аргумент percent = {percent} должен быть меньше или равен 100.");
            }
        }

        public static IEnumerable<T> Top<T, TKey>(this IEnumerable<T> ? data, int percent, Func<T, TKey> getBy)
        {
            CheckAttributes(data, percent);
            if (data.Count() == 0) 
            {
                return data;
            }
            var n = (int)Math.Ceiling(data.Count() * percent / 100.0);
            return data.OrderByDescending(getBy).Take(n);
        }

        // Частный случай
        public static IEnumerable<T> Top<T>(this IEnumerable<T> ? data, int percent)
        {
            return data.Top(percent, item => item);
        }
    }
}
