using System;
using System.Collections;
using System.Security.Cryptography;

namespace DZ_3
{
    internal class Program
    {
        enum Severity
        {
            Notice,
            Warning,
            Error
        };

        class GetCoefficientException : Exception
        {
            public DateTime TimeStamp { get; }
            public Severity Severity { get; }
            public GetCoefficientException(string message, Severity severity = Severity.Notice) : base(message)
            {
                TimeStamp = DateTime.Now;
                Severity = severity;
            }
        }

        class CalculateException : Exception
        {
            public DateTime TimeStamp;
            public Severity Severity;
            public CalculateException(string message, Severity severity = Severity.Notice) : base(message)
            {
                TimeStamp = DateTime.Now;
                Severity = severity;
            }
        }

        static void FormatData(string message, Severity severity, IDictionary data) {
            // Сохранение текущих цветов консоли, чтобы вернуть потом
            var consoleBackgroundColor = Console.BackgroundColor;
            var consoleForegroundColor = Console.ForegroundColor;

            if (severity == Severity.Error)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
            }
            else if (severity == Severity.Warning)
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
            }
            else if (severity == Severity.Notice)
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.White;
            }

            var line = new string('-', 50);

            Console.WriteLine(line);
            Console.WriteLine(message);
            Console.WriteLine(line);
            Console.WriteLine();
            Console.WriteLine($"a = {data["a"]}");
            Console.WriteLine($"b = {data["b"]}");
            Console.WriteLine($"c = {data["c"]}");

            // Возврат цветов консоли
            Console.BackgroundColor = consoleBackgroundColor;
            Console.ForegroundColor = consoleForegroundColor;
        }

        static int ? GetCoefficient(string coefficientName)
        {
            int ? coefficient ;
            Console.WriteLine($"Введите значение {coefficientName}:");
            var input = Console.ReadLine();
            try
            {
                coefficient = int.Parse(input);
                return coefficient;
            }
            catch (OverflowException)
            {
                var ex = new GetCoefficientException($"Введенное значение не вмещается в тип int\nНеобходимо ввести значение от {int.MinValue} до {int.MaxValue}", Severity.Notice);
                ex.Data["currentValue"] = input;
                throw ex;
            }
            catch (FormatException)
            {
                var ex = new GetCoefficientException($"Неверный формат параметра {coefficientName}", Severity.Error);
                ex.Data["currentValue"] = input;
                throw ex;
            }
            catch (ArgumentNullException)
            {
                var ex = new GetCoefficientException($"Параметр {coefficientName} пустой", Severity.Error);
                ex.Data["currentValue"] = input;
                throw ex;
            }
            catch (Exception ex)
            {
                var myEx = new GetCoefficientException($"Ошибка: {ex.Message}", Severity.Error);
                throw myEx;
            }
        }

        static double [] CalculateEquation(Dictionary<string, int?> coefficients)
        {
            double a = (double)coefficients["a"];
            double b = (double)coefficients["b"];
            double c = (double)coefficients["c"];

            double d = b * b - 4 * a * c;

            if (d < 0) {
                var ex = new CalculateException($"Вещественных значений не найдено", Severity.Warning);
                throw ex;
            }
            else if (d == 0)
            {
                var x = -b / (2 * a);
                return new double[] { x };
            }
            else
            {
                var x1 = (b + Math.Sqrt(d)) / (2 * a);
                var x2 = (b - Math.Sqrt(d)) / (2 * a);
                return new double[] { x1, x2 };
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Программа для решения уравнений вида");
            Console.WriteLine("a * x^2 + b * x + c = 0");

            var coefficients = new Dictionary<string, int ?>{
                                        { "a", null },
                                        { "b", null },
                                        { "c", null }
                                    };
            string currentCoeff = "a";
            ConsoleKeyInfo key;
            do
            {
                try
                {
                    foreach (var coeffName in coefficients.Keys)
                    {
                        currentCoeff = coeffName;
                        coefficients[coeffName] = GetCoefficient(coeffName);
                    }

                    var result = CalculateEquation(coefficients);
                    if (result.Length == 1)
                    {
                        Console.WriteLine($"x = {result[0]}");
                    }
                    else
                    {
                        Console.WriteLine($"x1 = {result[0]}, x2 = {result[1]}");
                    }
                }
                catch (GetCoefficientException ex)
                {

                    ex.Data["a"] = coefficients["a"];
                    ex.Data["b"] = coefficients["b"];
                    ex.Data["c"] = coefficients["c"];
                    ex.Data[currentCoeff] = ex.Data["currentValue"];
                    FormatData(ex.Message, ex.Severity, ex.Data);
                }
                catch (CalculateException ex)
                {
                    ex.Data["a"] = coefficients["a"];
                    ex.Data["b"] = coefficients["b"];
                    ex.Data["c"] = coefficients["c"];
                    FormatData(ex.Message, ex.Severity, ex.Data);
                }
                finally
                { 
                    coefficients["a"] = null;
                    coefficients["b"] = null;
                    coefficients["c"] = null;

                    Console.WriteLine("Нажмите любую кнопку, чтобы начать заново. Чтобы выйти, нажмите Esc.");
                    key = Console.ReadKey();
                }
            } while (key.Key != ConsoleKey.Escape);
        }
    }
}