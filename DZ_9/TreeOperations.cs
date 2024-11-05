using System.Collections.Generic;
using SalaryType = int;

namespace DZ_9
{
    public class TreeOperations
    {
        // Обход дерева In-order
        public static void Traverse(EmployeeNode? rootNode)
        {
            if (rootNode == null)
            {
                Console.WriteLine("Нет информации о сотрудниках");
                return;
            }

            // Берем левый
            if (rootNode.Left != null)
            {
                Traverse(rootNode.Left);
            }

            // Берем родительский
            Console.WriteLine(rootNode.Name + " - " + rootNode.Salary);

            // Берем правый
            if (rootNode.Right != null)
            {
                Traverse(rootNode.Right);
            }
        }

        // Добавление узла
        public static void AddNode(EmployeeNode rootNode, EmployeeNode nodeToAdd)
        {
            // Добавляемый элемент меньше корневого? - Идем в левое поддерево
            if (nodeToAdd.Salary < rootNode.Salary)
            {
                if (rootNode.Left != null)
                {
                    AddNode(rootNode.Left, nodeToAdd);
                }
                else
                {
                    rootNode.Left = nodeToAdd;
                }
            }
            else // Иначе идем в правое поддерево
            {
                if (rootNode.Right != null)
                {
                    AddNode(rootNode.Right, nodeToAdd);
                }
                else
                {
                    rootNode.Right = nodeToAdd;
                }
            }
        }

        // Поиск узла
        public static EmployeeNode? FindNode(EmployeeNode ? rootNode, SalaryType salary)
        {
            if (rootNode == null)
                return null;

            if (salary < rootNode.Salary) // Ищем в левом поддереве
            {
                if (rootNode.Left != null)
                {
                    return FindNode(rootNode.Left, salary);
                }
                return null;
            }

            if (salary > rootNode.Salary) // ищем в правом поддереве
            {
                if (rootNode.Right != null)
                {
                    return FindNode(rootNode.Right, salary);
                }
                return null;
            }

            return rootNode; // при равенстве возвращаем
        }

        // Поиск по зарплате
        public static void FindBySalary(EmployeeNode ? rootNode, SalaryType salary)
        {
            var node = FindNode(rootNode, salary);
            if (node != null)
            {
                Console.WriteLine($"Найден сотрудник {node.Name} с зарплатой {node.Salary}");
            }
            else
            {
                Console.WriteLine("Такой сотрудник не найден");
            }
        }

        // Поиск наименьшего элемента в узле
        public static SalaryType GetMinSalary(EmployeeNode rootNode)
        {
            // Ищем самый "левый" элемент
            if (rootNode.Left != null)
            {
                return GetMinSalary(rootNode.Left);
            }

            return rootNode.Salary;
        }

        // Удаление узла
        public static EmployeeNode? DeleteNodeBySalary(EmployeeNode? rootNode, SalaryType salary)
        {
            if (rootNode == null)
                return rootNode;

            if (salary < rootNode.Salary) // удаляемое меньше текущего
            {
                rootNode.Left = DeleteNodeBySalary(rootNode.Left, salary);
            }
            else if (salary > rootNode.Salary) // удаляемое больше текущего
            {
                rootNode.Right = DeleteNodeBySalary(rootNode.Right, salary);
            }
            else // удаляемое равно текущему
            {
                if (rootNode.Left == null && rootNode.Right == null) // листовой элемент
                {
                    return null;
                }

                if (rootNode.Left == null) // если есть только правый дочерний
                {
                    return rootNode.Right;
                }

                if (rootNode.Right == null) // если только левый дочерний
                {
                    return rootNode.Left;
                }

                // Если текущий узел имеет два дочерних узла, нужно найти наименьший в правом поддереве и поменять с удаляемым местами
                var minSalary = GetMinSalary(rootNode.Right);
                rootNode.Salary = minSalary;
                rootNode.Right = DeleteNodeBySalary(rootNode.Right, minSalary);
            }
            return rootNode;
        }
    }
}
