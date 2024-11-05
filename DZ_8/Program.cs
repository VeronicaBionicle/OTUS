using System.Diagnostics;
using System.Reflection;

namespace DZ_8
{
    internal class Program
    {
        static int FibonacciRecursive (int n)
        {
            if (n <= 0) return 0;
            if (n == 1) return 1;
            return FibonacciRecursive(n - 1) + FibonacciRecursive(n - 2);
        }

        static int FibonacciCycled(int n)
        {
            if (n <= 0) return 0;

            var sum = 0;
            var fibbonacci = new int[2] { 0, 1 };

            for (int i = 2; i <= n; ++i)
            {
                sum = fibbonacci[0] + fibbonacci[1];
                fibbonacci[0] = fibbonacci[1];
                fibbonacci[1] = sum;
            }
            return fibbonacci[1];
        }

        static void BenchFunction(int inputArgument, Func <int, int> TestFunc)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            var res = TestFunc(inputArgument);

            stopWatch.Stop();
            
            // Информация по результату и времени выполнения
            Console.WriteLine($"{TestFunc.Method.Name}({inputArgument}) = {res}");
            Console.WriteLine("Такты таймера: " + stopWatch.ElapsedTicks);
            Console.WriteLine("Время выполнения, нс: " + stopWatch.Elapsed.TotalNanoseconds);
        }

        static void Main(string[] args)
        {
            var testN = new int[] {5, 10, 20};
            foreach (int i in testN)
            {
                BenchFunction(i, FibonacciRecursive);
                Console.WriteLine();
                BenchFunction(i, FibonacciCycled);
                Console.WriteLine("------------------------------------------");
            }
        }
    }
}
