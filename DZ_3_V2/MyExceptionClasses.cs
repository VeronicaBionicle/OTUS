using System;
using System.Collections;

namespace DZ_3_V2
{
    public enum Severity
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

    static public class FormatExceptions {
        static public void FormatData(string message, Severity severity, IDictionary data)
        {
            // Сохранение текущих цветов консоли, чтобы вернуть потом
            var consoleBackgroundColor = Console.BackgroundColor;
            var consoleForegroundColor = Console.ForegroundColor;

            Console.Clear();

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
    }
}
