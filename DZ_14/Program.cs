using System;
using System.Collections;

namespace DZ_14
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Тест для коллекции = null
            try
            {
                List<int> nullList = null;
                var partList = nullList.Top(10);
                Console.WriteLine($"Top {10}% of list:\t{{{String.Join(", ", partList)}}}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }

            // Тест для пустой коллекции
            try
            {
                var emptyList = new List<int>();
                //List<int> emptyList = null;
                var partList = emptyList.Top(10);
                Console.WriteLine($"Top {10}% of list:\t{{{String.Join(", ", partList)}}}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }

            // Тесты для версии функции без входной Func
            var list = new List<int>{ 1, 7, 8, 4, 5, 6, 2, 1, 9 };
            var percents = new List<int> { 0, 10, 20, 30, 33, 50, 80, 100, 101 };

            foreach (var percent in percents) 
            {
                try 
                {
                    var partList = list.Top(percent);
                    Console.WriteLine($"Top {percent}% of list:\t{{{String.Join(", ", partList)}}}");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            // Тесты для функции с перегрузкой
            var persons = new List<Person> 
            {
                new Person(3,   "Steve",    12),
                new Person(4,   "Jordan",   25),
                new Person(1,   "Leny",     36),
                new Person(10,  "Ilon",     22),
                new Person(5,   "Andrew",   45),
                new Person(12,  "John",     99),
                new Person(50,  "Alex",     22),
                new Person(15,  "Trace",    98)
            };

            foreach (var percent in percents)
            {
                try
                {
                    // Сортировка по возрасту
                    var partPersons = persons.Top(percent, person => person.Age);
                    Console.WriteLine($"Top {percent}% of persons by Age");
                    partPersons.ToList().ForEach(el => Console.WriteLine($"{el.Id}\t{el.Name}\t{el.Age}"));
                    
                    // Сортировка по Id
                    partPersons = persons.Top(percent, person => person.Id);
                    Console.WriteLine($"Top {percent}% of persons by Id");
                    partPersons.ToList().ForEach(el => Console.WriteLine($"{el.Id}\t{el.Name}\t{el.Age}"));
                    
                    // Сортировка по имени
                    partPersons = persons.Top(percent, person => person.Name);
                    Console.WriteLine($"Top {percent}% of persons by Name");
                    partPersons.ToList().ForEach(el => Console.WriteLine($"{el.Id}\t{el.Name}\t{el.Age}"));
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

        }
    }
}
