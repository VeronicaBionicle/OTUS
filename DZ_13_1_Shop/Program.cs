using Microsoft.VisualBasic;
using System;
using System.ComponentModel;

namespace DZ_13_SpecialCollections
{   
    internal class Program
    {
        static void Main()
        {
            var shop = new Shop();
            var customer = new Customer();

            shop.AddCustomer(customer); // Добавляем покупателя в магазин

            ConsoleKeyInfo key;
            do
            {
                Console.WriteLine("Нажмите A, чтобы добавить новый товар в магазин.");
                Console.WriteLine("Нажмите D, чтобы убрать выбранный товар.");
                Console.WriteLine("Нажмите X, чтобы выйти из программы.");

                key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.A:
                        shop.Add();
                        break;
                    case ConsoleKey.D:
                        int id = -1;
                        bool parseOk = false;
                        while (!parseOk)
                        {
                            Console.WriteLine("Введите id товара, который необходимо удалить:");
                            var input = Console.ReadLine();
                            parseOk = int.TryParse(input, out id);
                            if (!parseOk)
                            {
                                Console.WriteLine("Некорректный id!");
                            }
                        }
                        shop.Remove(id);
                        break;
                    case ConsoleKey.X:
                        return; // выход из программы
                    default:
                        break;
                }
                
            } while (key.Key != ConsoleKey.X);
        }
    }
}
