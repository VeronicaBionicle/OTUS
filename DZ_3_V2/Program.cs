namespace DZ_3_V2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConsoleKeyInfo key;
            var equation = new QuadraticEquation();
            
            do
            {
                Console.Clear();
                Console.WriteLine("Выберите коэффициент для ввода значения кнопками вверх и вниз на клавиатуре. Для сохранения перейдите на другой коэффициент или нажмите Enter.");
                Console.WriteLine("Чтобы вычислить корни уравнения, нажмите Enter.");
                Console.WriteLine("Чтобы выйти, нажмите Esc.");

                try
                {
                    equation.PrintAndGetCoefficients(out key);

                    switch (key.Key)
                    {
                        case ConsoleKey.DownArrow:
                            equation.CurrentCoeffIndex++;
                            break;
                        case ConsoleKey.UpArrow:
                            equation.CurrentCoeffIndex--;
                            break;
                        case ConsoleKey.Enter:
                            // Для обработки ввода Enterом
                            if (equation.Coefficients.Values.Any(n => n == null)) { // Если хоть один не заполнен, продолжаем ввод с переходом на новую строку
                                equation.CurrentCoeffIndex++;
                                break;
                            }
                            // Если все ввели, можно считать
                            equation.PrintCalculation();
                            key = Console.ReadKey();
                            break;
                        case ConsoleKey.Escape: break;
                        default: break;
                    }
                }
                catch (GetCoefficientException ex)
                {
                    ex.Data["a"] = equation.Coefficients["a"];
                    ex.Data["b"] = equation.Coefficients["b"];
                    ex.Data["c"] = equation.Coefficients["c"];
                    ex.Data[equation.CurrentCoefficientName] = ex.Data["currentValue"];
                    FormatExceptions.FormatData(ex.Message, ex.Severity, ex.Data);
                    key = Console.ReadKey();
                }
                catch (CalculateException ex)
                {
                    ex.Data["a"] = equation.Coefficients["a"];
                    ex.Data["b"] = equation.Coefficients["b"];
                    ex.Data["c"] = equation.Coefficients["c"];
                    FormatExceptions.FormatData(ex.Message, ex.Severity, ex.Data);
                    key = Console.ReadKey();
                }
            } while (key.Key != ConsoleKey.Escape);
        }
    }
}