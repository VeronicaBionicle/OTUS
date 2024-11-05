namespace DZ_13_2_Librarian
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var library = new Library();
            var cancellationSource = new CancellationTokenSource();
            var cancellationToken = cancellationSource.Token;

            library.UpdateReading(cancellationToken);

            ConsoleKeyInfo key;
            do
            {
                Console.WriteLine("Нажмите: 1 - добавить книгу; 2 - вывести список непрочитанного; 3 - выйти");

                key = Console.ReadKey();
                switch (key.Key)
                {
                    case var k when k == ConsoleKey.D1 || k == ConsoleKey.NumPad1:
                        Console.WriteLine("Введите название книги: ");
                        var name = Console.ReadLine() ?? "";
                        library.Add(name);
                        break;
                    case var k when k == ConsoleKey.D2 || k == ConsoleKey.NumPad2:
                        library.Print();
                        break;
                    case var k when k == ConsoleKey.D3 || k == ConsoleKey.NumPad3:
                        cancellationSource.Cancel(); // отменяем поток
                        return; // выход из программы
                    default:
                        break;
                }
            } while (key.Key != ConsoleKey.D3 && key.Key != ConsoleKey.NumPad3);
        }
    }
}
