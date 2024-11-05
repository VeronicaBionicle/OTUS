using System;
using System.Text;

namespace DZ12_HashDict
{
    internal class Program : Test
    {
        static void Main(string[] args)
        {
            var dict = new OtusDictionary<string>();

            // Добавление данных в словарь (в пределах размера) в четных индексах
            try
            {
                for (int i = 0; i < 32; i += 2)
                {
                    dict.Add(i, i.ToString());
                }
                // Пытаемся добавить пустое значение
                dict[3] = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            // Пытаемся взять существующий ключ
            TryGet(dict, 2, getByIndex: false);
            TryGet(dict, 4, getByIndex: true);

            // Пытаемся взять несуществующий ключ
            TryGet(dict, 1, getByIndex: false);
            TryGet(dict, 15, getByIndex: true);

            // Пытаемся взять отрицательный ключ
            TryGet(dict, -1, getByIndex: false);
            TryGet(dict, -2, getByIndex: true);

            // Пытаемся взять слишком большой ключ
            TryGet(dict, 128, getByIndex: false);
            TryGet(dict, 200, getByIndex: true);

            // Добавление данных по старому ключу
            Console.WriteLine("-----------------");
            Console.WriteLine($"Try to add data by key");
            dict[2] = "2 * 32";
            dict.Add(4, "2 * 2 * 32");

            // Опросим все данные словаря
            Console.WriteLine("-----------------");
            Console.WriteLine($"Dictionary data:");
            for (int i = 0; i < dict.Size; i++)
            {
                try
                {
                    var value = dict[i];
                    Console.WriteLine($"For key = {i}\t{value}");
                }
                catch (Exception)
                {
                    // Пропустим, т.к. такого ключа нет
                    //Console.WriteLine($"For key = {i}\tno value");
                }
            }
        }
    }
}
