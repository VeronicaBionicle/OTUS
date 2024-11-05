using System;
using System.Collections.Generic;

namespace DZ_3_V2
{
    public class QuadraticEquation
    {
        private Dictionary<string, int?> _coefficients = new Dictionary<string, int?>
                                    {
                                        { "a", null },
                                        { "b", null },
                                        { "c", null }
                                    };

        public Dictionary<string, int?> Coefficients // "Снаружи" можно только смотреть
        {
            get { return _coefficients; }
        }

        private int _currentCoeffIndex = 0;
        public int CurrentCoeffIndex
        {
            get { return _currentCoeffIndex; }
            set
            {
                if (value >= _coefficients.Count)
                {
                    _currentCoeffIndex = 0;
                }
                else if (value < 0)
                {
                    _currentCoeffIndex = _coefficients.Count - 1;
                }
                else
                {
                    _currentCoeffIndex = value;
                }

            }
        }

        public string CurrentCoefficientName
        {
            get
            {
                return _coefficients.Keys.ElementAt(_currentCoeffIndex);
            }
        }

        private string ReplaceNullNumber(int? number, string replacer = " ")
        {
            return !number.HasValue ? replacer : number.ToString();
        }

        private string FormatEquation()
        {
            string a = ReplaceNullNumber(_coefficients["a"], "a");
            string b = ReplaceNullNumber(_coefficients["b"], "b");
            string c = ReplaceNullNumber(_coefficients["c"], "c");
            return $"{a} * x^2 + {b} * x + {c} = 0";
        }

        private int? GetCoefficient(int? currentValue, out ConsoleKeyInfo lastKey)
        {
            string input = "";
            try
            {
                /* Считываем текст */
                var key = Console.ReadKey();
                while (key.Key != ConsoleKey.Escape && key.Key != ConsoleKey.UpArrow && key.Key != ConsoleKey.DownArrow && key.Key != ConsoleKey.Enter)
                {
                    if (!char.IsControl(key.KeyChar) && !char.IsWhiteSpace(key.KeyChar))
                    {
                        if (input == "") // При вводе первого символа затираем
                        {
                            var pos = Console.GetCursorPosition();
                            Console.SetCursorPosition(pos.Left, pos.Top);
                            Console.Write(new String(' ', ReplaceNullNumber(currentValue).Length)); // Зачищаем строку
                            Console.SetCursorPosition(pos.Left, pos.Top);
                        };

                        input += key.KeyChar;
                    }
                    key = Console.ReadKey();
                }
                lastKey = key;

                // Если передумали вводить, возвращаем старое значение
                if (input == "" && (key.Key == ConsoleKey.Escape || key.Key == ConsoleKey.UpArrow || key.Key == ConsoleKey.DownArrow || key.Key == ConsoleKey.Enter))
                {
                    return currentValue;
                }

                int? coefficient = int.Parse(input);
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
                var ex = new GetCoefficientException($"Неверный формат параметра {CurrentCoefficientName}", Severity.Error);
                ex.Data["currentValue"] = input;
                throw ex;
            }
            catch (ArgumentNullException)
            {
                var ex = new GetCoefficientException($"Параметр {CurrentCoefficientName} пустой", Severity.Error);
                ex.Data["currentValue"] = input;
                throw ex;
            }
            catch (Exception ex)
            {
                var myEx = new GetCoefficientException($"Ошибка: {ex.Message}", Severity.Error);
                myEx.Data["currentValue"] = input;
                throw myEx;
            }
        }

        public void PrintAndGetCoefficients(out ConsoleKeyInfo key)
        {
            var currentCoefficientName = CurrentCoefficientName;
            Console.WriteLine(FormatEquation());
            var startLine = Console.GetCursorPosition().Top;

            foreach (var coeff in _coefficients.Keys)
            {
                Console.WriteLine($"{(currentCoefficientName == coeff ? '>' : ' ')} {coeff}: {_coefficients[coeff]}");
            }

            Console.SetCursorPosition("  : ".Length + currentCoefficientName.Length, startLine + CurrentCoeffIndex); // Выставить курсор на текущую редактируемую строку
            _coefficients[currentCoefficientName] = GetCoefficient(_coefficients[currentCoefficientName], out key); // Считать текущий параметр
        }

        private double[] CalculateEquation()
        {
            double a = (double)_coefficients["a"];
            double b = (double)_coefficients["b"];
            double c = (double)_coefficients["c"];

            double d = b * b - 4 * a * c;

            if (d < 0)
            {
                var ex = new CalculateException($"Вещественных значений уравнения {FormatEquation()} не найдено", Severity.Warning);
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

        public void PrintCalculation()
        {
            var result = CalculateEquation();
            Console.Clear();
            Console.WriteLine($"Решение для уравнения {FormatEquation()}");
            if (result.Length == 1)
            {
                Console.WriteLine($"x = {result[0]}");
            }
            else
            {
                Console.WriteLine($"x1 = {result[0]}, x2 = {result[1]}");
            }
        }
    }
}
