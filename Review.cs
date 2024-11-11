namespace Top
{
    public static class EnumerableExtensions
    {
        // 0.1 Возможно, вместо названия аргумента "x" можно использовать более "значащее", например, count или percent
        // 0.2 input можно сделать nullable (?), поскольку в коде есть проверка
        public static IEnumerable<T> Top<T>(this IEnumerable<T> input, int x)
        {
            // 1.1 Для большей информативности можно разделить кейсы слишком маленького и слишком большого числа x
            // 1.2 По стилю желательно обрамлять для if действия в фигурные скобки и переносить на новую строку
            // 1.3 Для информативности можно выводить в тексте исключения значение 
            if (x < 1 || x > 100) throw new ArgumentException("The value must be in the range from 1 to 100!");

            // 2.1 Должна ли функция выдавать исключение для пустой коллекции, а не возвращать пустую коллекцию?
            // 10% от 0 элементов => 0 элементов
            // В общем, вкусовщина и зависит от необходимого поведения по ТЗ
            // 2.2 Та же придирка по стилю - желательны фигурные скобки и перенос на новую строку
            if (input == null || input.Count() == 0) throw new Exception("A null or empty value was passed!");

            decimal finalIndex = Math.Ceiling(input.Count() * x / 100m);

            // 3.1 Функция TakeLast вроде не выдает null, только пустую коллекцию
            // И здесь будет как минимум коллекция из 1 элемента и 1 процент элементов, так что null никак не получим (будет выпадать раньше в исключение)
            // И функция Top по своему объявлению возвращает не null (нет символа ?)
            // 3.2 Функция будет просто возвращать последние x процентов элементов коллеции, тогда из названия функции следует,
            // что надо брать x% "верхних" по сортировке элементов 
            IEnumerable<T>? output = input.TakeLast((int)finalIndex);

            return output;
        }

        public static IEnumerable<T> Top<T>(this IEnumerable<T> input, int x, Func<T, int> selector)
        {
            // 4 Та же проблема, что в пункте 3.2.
            // Можно поменять местами очередность Top и OrderByDescending, тогда функция заработает по задуманной логике
            IEnumerable<T> output = input.Top(x).OrderByDescending(selector);
            return output;
        }
    }
}

namespace Top
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Можно добавить тест с несортированными данными
                var list = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                // Можно добавить больше тестов с разными процентами
                Console.WriteLine($"[{string.Join(',', list.Top(30))}]");
                Console.WriteLine();

                List<Person> anotherList = new()
                {
                    new() { Name = "Alice", Age = 25 },
                    new() { Name = "Bob", Age = 15 },
                    new() { Name = "Charlie", Age = 40 },
                    new() { Name = "Dean", Age = 80 },
                    new() { Name = "Eva", Age = 5 },
                    new() { Name = "Frank", Age = 30 },
                    new() { Name = "Greg", Age = 55 },
                    new() { Name = "Hank", Age = 28 },
                    new() { Name = "Ilona", Age = 13 },
                };

                var output = anotherList.Top(30, person => person.Age);
                output.ToList().ForEach(x => Console.WriteLine($"Name - {x.Name}, Age - {x.Age}"));

                // Не хватает тестов с некорректными процентами меньше 0 и больше 100, а также с пустыми и "нулевыми" коллекциями
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}