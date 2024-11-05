using System.Collections.Concurrent;

namespace DZ_13_2_Librarian
{
    public class Library 
    {
        private ConcurrentDictionary<string, int> books;
        public void Add(string name) 
        {
            books.TryAdd(name, 0);
        }
        public void Print() 
        {
            foreach (var book in books) 
            {
                Console.WriteLine($"{book.Key} - {book.Value}%");
            }
        }

        public async Task UpdateReading(CancellationToken cancellationToken)
        {
            while (true) // бесконечный цикл...
            {
                await Task.Run(() =>
                {
                    foreach (var book in books)
                    {
                        if (book.Value == 99) // если должно стать 100% - прочитали, удаляем 
                        {
                            books.TryRemove(book.Key, out _);
                        }
                        else // Увеличить процент чтения на 1%
                        {
                            books.TryUpdate(book.Key, book.Value + 1, book.Value);
                        }
                    }
                    Thread.Sleep(1000); // раз в 1 сек
                }, cancellationToken);
            }
        }

        public Library()
        {
            books = new ConcurrentDictionary<string, int>();
        }
    }
}
