using System.Net;

namespace DZ_10
{
    public class MultiDownloader
    {
        private ImageDownloader _downloadMaster;
        private Dictionary<string, Task> _downloadList;
        private CancellationTokenSource _downloadCancellationSource; // Отменяем всех одним источником
        public IReadOnlyDictionary<string, Task> DownloadList { get { return _downloadList; } } // чтобы просматривать загрузки, не давая менять их

        public MultiDownloader() 
        {
            _downloadMaster = new ImageDownloader();
            _downloadList = new Dictionary<string, Task>();
            _downloadCancellationSource = new CancellationTokenSource();
        }

        // Добавить задание на загрузку файла
        public void AddDownload(string fileName, string fileUri) 
        {
            
                var cancelToken = _downloadCancellationSource.Token;

                _downloadList.Add(
                        fileName,                                                // имя картинки
                        _downloadMaster.Download(fileUri, fileName, cancelToken) // задание на ее скачивание
                        );
            
        }

        // Добавление подписчиков
        public void AddSubscriber(Subscriber subscriber, string eventType) 
        {
            switch (eventType) 
            {
                case "ImageStarted":
                    _downloadMaster.ImageStarted += subscriber.OnDataReceived;
                    break;
                case "ImageCompleted":
                    _downloadMaster.ImageCompleted += subscriber.OnDataReceived;
                    break;
                default:
                    Console.WriteLine($"События с типом \"{eventType}\" не существует");
                    break;
            }
            
        }

        // Отмена всех заданий на загрузку
        public void CancelDownload() 
        {
            _downloadCancellationSource.Cancel();
        }
    }
}
