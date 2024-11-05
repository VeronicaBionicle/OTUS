namespace DZ4_Stack
{
    internal class Program
    {
        /* Тесты */
        static void Main(string[] args)
        {
            try
            {
                var s = new Stack("a", "b", "c"); // Работа конструктора с несколькими входными строками
                Console.WriteLine(s.ToString());

                Console.WriteLine($"size = {s.Size}, Top = '{s.Top}'"); // size = 3, Top = 'c'
             
                var deleted = s.Pop();
                Console.WriteLine($"Извлек верхний элемент '{deleted}' Size = {s.Size} Top = '{s.Top}'"); // Извлек верхний элемент 'c' Size = 2
            
                s.Add("d");
                Console.WriteLine($"size = {s.Size}, Top = '{s.Top}'");// size = 3, Top = 'd'

                s.Pop();
                s.Pop();
                s.Pop();
                Console.WriteLine($"size = {s.Size}, Top = {(s.Top == null ? "null" : s.Top)}"); // size = 0, Top = null

                /* Проверка Merge */
                var x = new Stack("a", "b", "c");
                x.Merge(new Stack("1", "2", "3"));
                x.Merge(new Stack()); // ничего не добавляет
                Console.WriteLine(x.ToString());
                // в стеке s теперь элементы - "a", "b", "c", "3", "2", "1" <- верхний

                /* Проверка Concat */
                var xx = Stack.Concat(new Stack("a", "b", "c"), new Stack("1", "2", "3"), new Stack("А", "Б", "В"), new Stack(), new Stack("X"));
                Console.WriteLine(xx.ToString());
                // в стеке s теперь элементы - "c", "b", "a" "3", "2", "1", "В", "Б", "А", "X" <- верхний
            
                s.Pop(); // Стек пустой

                /* Теперь дает ошибку
                 * var stIt = new StackItem("a");
                 * var stIt = new Stack.StackItem("a");
                */
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
            
        }
    }
}
