using System.Runtime.CompilerServices;

namespace DZ_14
{
    public static class TExtensionForIEnumerable
    {
        private static void CheckAttribute(int percent) 
        {
            if (percent < 1)
            {
                throw new ArgumentException($"Аргумент percent = {percent} должен быть больше 1.");
            }
            if (percent > 100)
            {
                throw new ArgumentException($"Аргумент percent = {percent} должен быть меньше или равен 100.");
            }
        }
        public static IEnumerable<T> Top<T>(this IEnumerable<T> data, int percent)
        {
            CheckAttribute(percent);
            var n = (int)Math.Ceiling(data.Count() * percent / 100.0);
            return data.OrderByDescending(item => item).Take(n);
        }
        public static IEnumerable<T> Top<T, TKey>(this IEnumerable<T> data, int percent, Func<T, TKey> getBy)
        {
            CheckAttribute(percent);
            var n = (int)Math.Ceiling(data.Count() * percent / 100.0);
            return data.OrderByDescending(getBy).Take(n);
        }
    }
}
