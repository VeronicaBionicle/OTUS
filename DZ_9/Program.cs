using SalaryType = int;

namespace DZ_9
{
    internal class Program
    {
        // Значения для "меню"
        const string RepeatProgram = "0";
        const string RepeatSearch = "1";

        public static bool ReadSalary(out SalaryType salary)
        {
            Console.WriteLine("Введите зарплату сотрудника:");

            var str = Console.ReadLine();
            var isParsed = SalaryType.TryParse(str, out salary);

            if (!isParsed)
            {
                Console.WriteLine($"Ошибка ввода зарплаты: {str}");
                salary = -1;
                return false;
            }
            return true;
        }

        static void Main(string[] args)
        {
            string ? name;
            SalaryType salary;
            bool isParsed;
            string ? answer; // для взаимодействия с меню

            do // Цикл ввода, вывода и поиска сотрудников
            {
                EmployeeNode? employees = null;

                Console.WriteLine("Ввод данных сотрудников. Чтобы закончить ввод, введите пустое имя");

                do
                {
                    Console.WriteLine("Введите имя сотрудника:");
                    name = Console.ReadLine();

                    if (name == "" || name == null) // останов ввода по пустой строке
                        break;

                    isParsed = ReadSalary(out salary);
                    if (!isParsed)
                        break;

                    // Добавление нового узла в дерево
                    if (employees == null)
                    {
                        employees = new EmployeeNode(name, salary);
                    }
                    else
                    {
                        TreeOperations.AddNode(employees, new EmployeeNode(name, salary));
                    }
                } while (name != "");

                // Вывод всех сотрудников в порядке возрастания зарплаты
                TreeOperations.Traverse(employees);

                do // Цикл поиска
                {
                    Console.WriteLine("Поиск сотрудника:");
                    isParsed = ReadSalary(out salary);
                    if (isParsed)
                    {
                        TreeOperations.FindBySalary(employees, salary);
                    }

                    // Меню
                    Console.WriteLine("Введите 0, чтобы перейти к началу программы и начать ввод сотрудников заново.");
                    Console.WriteLine("Введите 1, чтобы выполнить поиск сотрудника по зарплате.");
                    Console.WriteLine("Для выхода введите любую другую строку.");
                    answer = Console.ReadLine();
                } while (answer == RepeatSearch);
            } while (answer == RepeatProgram || answer == RepeatSearch);
        }
    }
}
