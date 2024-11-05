namespace DZ12_HashDict
{
    internal class Test
    {
        public static void TryGet<T>(OtusDictionary<T> dict, int key, bool getByIndex = false)
        {
            Console.WriteLine("-----------------");
            Console.WriteLine($"Try to get by key = {key}");
            T value;

            try
            {
                if (getByIndex)
                {
                    value = dict[key];
                }
                else
                {
                    value = dict.Get(key);
                }
                Console.WriteLine($"Value = {value}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}