using System.Net;

namespace DZ_10
{
    internal class ImageDownloader
    {
        public delegate void EventHandler(string message);
        public event EventHandler ? ImageStarted, ImageCompleted;

        public async Task Download(string uri, string fileName, CancellationToken cancellationToken, string extension = ".jpg")
        {
            using (var webClient = new WebClient()) 
            {
                cancellationToken.Register(webClient.CancelAsync); // для вызова отмены

                string fullFileName = $"\"{fileName}{extension}\"";

                try 
                {
                    ImageStarted?.Invoke("Скачивание файла началось");
                    Console.WriteLine($"Качаю {fullFileName} из \"{uri}\".......\n\n");

                    await webClient.DownloadFileTaskAsync(new Uri(uri), fileName + extension);

                    Console.WriteLine($"Успешно скачал {fullFileName} из \"{uri}\"");
                    ImageCompleted?.Invoke("Скачивание файла закончилось");
                }
                catch (WebException ex) when (ex.Status == WebExceptionStatus.RequestCanceled)
                {
                    Console.WriteLine($"Отменено скачивание {fullFileName}");
                    throw new OperationCanceledException();
                }
                catch (AggregateException ex) when (ex.InnerException is WebException exWeb && exWeb.Status == WebExceptionStatus.RequestCanceled)
                {
                    Console.WriteLine($"Отменено скачивание {fullFileName}");
                    throw new OperationCanceledException();
                }
                catch (TaskCanceledException)
                {
                    Console.WriteLine($"Отменено скачивание {fullFileName}");
                    throw new OperationCanceledException();
                }
            }
        }
    }
}
