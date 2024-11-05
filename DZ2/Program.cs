using System.Collections;
using System.Diagnostics;
using System.Linq;

namespace DZ2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var list = new List<int>();
            var arrayList = new ArrayList();
            var linkedList = new LinkedList<int>();
            int n;
            Random rnd = new Random();
            Stopwatch stopwatch = new Stopwatch();

            /* Генерация данных для List */
            stopwatch.Start();

            for (int i = 0; i < 1000000; i++)
            {
                n = rnd.Next();
                list.Add(n);
            }

            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;

            Console.WriteLine("Время заполнения List: {0:00}:{1:00}:{2:00}.{3}",
                            ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);

            /* Генерация данных для ArrayList */
            stopwatch.Start();

            for (int i = 0; i < 1000000; i++)
            {
                n = rnd.Next();
                arrayList.Add(n);
            }

            stopwatch.Stop();
            ts = stopwatch.Elapsed;

            Console.WriteLine("Время заполнения ArrayList: {0:00}:{1:00}:{2:00}.{3}",
                            ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);

            /* Генерация данных для LinkedList */
            stopwatch.Start();

            for (int i = 0; i < 1000000; i++)
            {
                n = rnd.Next();
                linkedList.AddLast(n);
            }

            stopwatch.Stop();
            ts = stopwatch.Elapsed;

            Console.WriteLine("Время заполнения LinkedList: {0:00}:{1:00}:{2:00}.{3}",
                            ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
           
            Console.WriteLine();

            /* Вывод 496753-го элемента List */
            stopwatch.Start();
            Console.WriteLine($"list[496753] = {list[496753]}");
            stopwatch.Stop();
            ts = stopwatch.Elapsed;
            Console.WriteLine("Время поиска элемента в List: {0:00}:{1:00}:{2:00}.{3}",
                            ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);

            /* Вывод 496753-го элемента ArrayList */
            stopwatch.Start();
            Console.WriteLine($"arrayList[496753] = {arrayList[496753]}");
            stopwatch.Stop();
            ts = stopwatch.Elapsed;
            Console.WriteLine("Время поиска элемента в ArrayList: {0:00}:{1:00}:{2:00}.{3}",
                            ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);

            /* Вывод 496753-го элемента LinkedList */
            stopwatch.Start();
            Console.WriteLine($"linkedList[496753] = {linkedList.ElementAt(496753)}");
            stopwatch.Stop();
            ts = stopwatch.Elapsed;
            Console.WriteLine("Время поиска элемента в LinkedList: {0:00}:{1:00}:{2:00}.{3}",
                            ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);

            Console.WriteLine();

            /* Поиск элементов, делящихся на 777 в List */
            stopwatch.Start();
            Console.WriteLine("Поиск в List");
            foreach (var element in list) 
            {
                if (element % 777 == 0)
                {
                    Console.WriteLine(element);
                }
            }
            stopwatch.Stop();
            ts = stopwatch.Elapsed;
            Console.WriteLine("Время поиска элементов, делящихся на 777, в List: {0:00}:{1:00}:{2:00}.{3}",
                            ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);

            /* Поиск элементов, делящихся на 777 в ArrayList */
            stopwatch.Start();
            Console.WriteLine("Поиск в ArrayList");
            foreach (int element in arrayList)
            {
                if (element % 777 == 0)
                {
                    Console.WriteLine(element);
                }
            }
            stopwatch.Stop();
            ts = stopwatch.Elapsed;
            Console.WriteLine("Время поиска элементов, делящихся на 777, в ArrayList: {0:00}:{1:00}:{2:00}.{3}",
                            ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);

            /* Поиск элементов, делящихся на 777 в LinkedList */
            stopwatch.Start();
            Console.WriteLine("Поиск в LinkedList");
            foreach (var element in linkedList)
            {
                if (element % 777 == 0)
                {
                    Console.WriteLine(element);
                }
            }
            stopwatch.Stop();
            ts = stopwatch.Elapsed;
            Console.WriteLine("Время поиска элементов, делящихся на 777, в LinkedList: {0:00}:{1:00}:{2:00}.{3}",
                            ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);

        }
    }
}
