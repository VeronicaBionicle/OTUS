using SalaryType = int;

namespace DZ_9
{
    public class EmployeeNode
    {
        public string ? Name { get; set; }
        private SalaryType salary;
        public SalaryType Salary {
            get
            {
                return salary;
            }
            
            set 
            {
                if (value < 0)
                {
                    throw new Exception("Зарплата должна быть больше ноля");
                }
                salary = value; 
            } 
        }
        public EmployeeNode? Left { get; set; }
        public EmployeeNode? Right { get; set; }

        public EmployeeNode() { } // "Пустой" конструктор
        public EmployeeNode(string name, SalaryType salary)
        {
            Name = name;
            Salary = salary;
        }
    }
}
