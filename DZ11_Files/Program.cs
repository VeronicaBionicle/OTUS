using System.Security.AccessControl;
using System.Text;

namespace DZ11_Files
{
    internal class Program
    {
        static public int CreateSubFolders(string mainDirectory, string[] subDirectories) 
        {
            var disk = Path.GetPathRoot(mainDirectory);
            // Проверка существования диска из пути
            if (!Directory.Exists(disk))
            {
                Console.WriteLine($"Диск {disk} не найден.");
                return -1;
            }
            // Проверка возможности создания директорий
            if (!CheckAccess.HasDirectoryAccess(disk, FileSystemRights.CreateDirectories))
            {
                Console.WriteLine($"Нет прав на создание директорий на диске {disk}.");
                return -1;
            }
            // Создание директорий
            foreach (var directory in subDirectories) 
            {
                var currentDirectory = Path.Combine(mainDirectory, directory);
                Directory.CreateDirectory(currentDirectory);
            }
            return 0;
        }

        // Фунция для добавления текста в файлы
        // Подразумеваем, что тексты и файлы в одинаковом количестве
        async static Task AddTextToFiles(List<string> paths, List<string> texts, CancellationToken cancelToken) 
        {
            var tasks = new List<Task>();
            for (int i = 0; i < paths.Count; ++i)
            {
                var fileInfo = new FileInfo(paths[i]);
                if (!fileInfo.Exists)
                {
                    Console.WriteLine($"Файла {paths[i]} не существует.");
                    continue; // идем следующий файл заполнять
                }
                if (!CheckAccess.HasFileAccess(paths[i], FileSystemRights.WriteData))
                {
                    Console.WriteLine($"Недостаточно прав для записи в файл {paths[i]}");
                    continue; // идем следующий файл заполнять
                }
                tasks.Add(File.AppendAllTextAsync(paths[i], texts[i], Encoding.UTF8, cancelToken)); // Добавим в список задач ввода текста в UTF-8
            }
            await Task.WhenAll(tasks); // Дождемся окончания всех записей в файлы
        }

        // Фунция для добавления текста в файлы
        // "Пустое" задание, чтобы совпадала размерность входа и выхода 
        async static Task<string?[]> ReadTextFromFiles(List<string> paths, CancellationToken cancelToken)
        {
            var tasks = new List<Task<string>>();

            for (int i = 0; i < paths.Count; ++i)
            {
                var fileInfo = new FileInfo(paths[i]);
                if (!fileInfo.Exists)
                {
                    Console.WriteLine($"Файла {paths[i]} не существует.");
                    tasks.Add(new Task<string>(() => null, cancelToken));
                    tasks.Last().Start();
                    continue; // идем следующий файл читать
                }
                if (!CheckAccess.HasFileAccess(paths[i], FileSystemRights.ReadData))
                {
                    Console.WriteLine($"Недостаточно прав для чтения из файла {paths[i]}");
                    tasks.Add(new Task<string>(() => null, cancelToken));
                    tasks.Last().Start();
                    continue; // идем следующий файл читать
                }
                tasks.Add(File.ReadAllTextAsync(paths[i], cancelToken)); // Добавим в список задач чтение текста
            }
            string[] result = await Task.WhenAll(tasks);
            return result;
        }

        async static Task Main(string[] args)
        {
            CancellationTokenSource cancellationSource = new CancellationTokenSource(); // для отмены асинхронных задач

            try
            {
                var mainDirectory = @"C:\Otus";
                var directories = new string[] { "TestDir1", "TestDir2" };

                // Создание директорий
                var status = CreateSubFolders(mainDirectory, directories);
                if (status < 0)
                {
                    Console.WriteLine("Директории создать не удалось.");
                    return;
                }

                // Ожидание нажатия, чтобы изменить разрешения или удалить директории (тест)
                Console.WriteLine("Директории созданы, нажмите любую кнопку, чтобы продолжить.");
                Console.ReadLine();

                // Создание файлов
                var fileName = "File";
                var fileExtension = "txt";
                const int maxSize = 10;
                var filePaths = new List<string>();

                for (int i = 0; i < directories.Length; ++i)
                {
                    var fullPath = Path.Combine(mainDirectory, directories[i]);
                    if (!CheckAccess.HasDirectoryAccess(fullPath, FileSystemRights.CreateFiles))
                    {
                        Console.WriteLine($"Нет прав на создание файлов в папке {fullPath}.");
                        continue; // идем следующую папку заполнять
                    }

                    // Создать 10 файлов
                    for (int j = 1; j <= maxSize; ++j)
                    {
                        var fullFileName = Path.Combine(fullPath, $"{fileName}{j}.{fileExtension}");
                        using (FileStream fs = File.Create(fullFileName)) { } // Создать файл
                        filePaths.Add(fullFileName);
                    }

                }

                // Ожидание нажатия, чтобы проверить разрешения или удалить файлы (тест)
                Console.WriteLine("Файлы созданы, нажмите любую кнопку, чтобы продолжить.");
                Console.ReadLine();

                // Заполнение файлов - имя файла
                var cancelToken = cancellationSource.Token;
                await AddTextToFiles(filePaths, filePaths, cancelToken);

                // Заполнение файлов - текущее время
                var listOfStrings = Enumerable.Repeat($"\n{DateTime.Now.ToString()}", filePaths.Count).ToList();
                await AddTextToFiles(filePaths, listOfStrings, cancelToken);

                // Ожидание нажатия, чтобы проверить разрешения или удалить файлы (тест)
                Console.WriteLine("Файлы заполнены, нажмите любую кнопку, чтобы прочитать.");
                Console.ReadLine();

                // Чтение файлов
                var texts = ReadTextFromFiles(filePaths, cancelToken).Result;
                for (int i = 0; i < filePaths.Count; ++i)
                {
                    Console.WriteLine($"{filePaths[i]}:\n{texts[i]}");
                }

            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine("Нет прав на работу с " + ex.Message);
            }
            catch (DriveNotFoundException ex)
            {
                Console.WriteLine("Диск не найден: " + ex.Message);
            }
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine("Директория не найдена: " + ex.Message);
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("Файл не найден: " + ex.Message);
            }
            catch (IOException ex)
            {
                Console.WriteLine("Ошибка ввода-вывода: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }
            finally {
                cancellationSource.Cancel(); // в конце концов всех отменим
            }

        }
    }
}
