using System.Net;

namespace DZ_10
{
    internal class Program
    {
        static void Main()
        {
            var downloader = new MultiDownloader();

            // Подписываемся на события касса "скачивателя"
            var subscriber = new Subscriber();
            downloader.AddSubscriber(subscriber, "ImageStarted");
            downloader.AddSubscriber(subscriber, "ImageCompleted");

            // Список из 10 картинок
            var downloadDictionary = new Dictionary<string, string>() {
                { "Mountains1", "https://i.pinimg.com/originals/69/d1/32/69d132af5c66c1b1312d5f5e6f4492d5.jpg" },
                { "Mountains2", "https://i.pinimg.com/originals/67/0b/79/670b79788824bd2914db5dde3efcebc6.jpg" },
                { "Mountains3", "https://avatars.mds.yandex.net/get-mpic/1544149/img_id1879478330078875690.jpeg/orig" },
                { "Mountains4", "https://avatars.mds.yandex.net/i?id=428b47b83c7d20ac8d7c97087a48928b_l-10702810-images-thumbs&n=13" },
                { "Mountains5", "https://cdn1.ozone.ru/s3/multimedia-2/6458272082.jpg" },
                { "Mountains6", "https://avatars.mds.yandex.net/get-mpic/5395693/img_id5066227332817371498.jpeg/orig"},
                { "Island", "https://cdn1.ozone.ru/s3/multimedia-o/6463892244.jpg"},
                { "Fiordes", "https://yandex-images.clstorage.net/9GnV9y380/e2e70a22/XKJxKdxL-kPXzNPVW255MH9KlEdTNqEE62N4NNkam8iVjg3wB0bwB87nUQEdYIKTQRL9ib0oqOZi_hsquMo9l_mWJH0ZX5-np8yCkevKuJC-S8CGhwcAIWugG4MejT1PdozfxwCS8AI7pzWV7FShNdVWeH5LHXwg9xytXkg5u9HeM5PqXyo6ZmYfkrIaPQlxvehxt5NHtvd8gZpAbIxNzcVsbYTj0BIBiUdGMD9ns4aS1rp9WV684mWMfq_5mFgyPZITSP4r2xZGvjJT216YUm5L8iYhtYTEH7MZY7_MLfzWzu3VYPMDkmn2dSPeBmHHUURK3slPv-D3HE1dqblrIkwiIbnM6_s0Zj4RkTicu1LviUPlo7eWpx3hC1M__63v58x8t1KhICLJ9aTyDWTy56Hmqw-andwjpr-ND8nL-8JeQaBo_tj5RZZNs-GqvWuAHWnzlIOGtMadERiRf4w8XbQP7WdzATNzqJfEMT_3waaRFNkO2Gy9wOVNTv2Za6kyfGGTSjyYGHdWvZHy-t6JIb8os8bix7dXP-KL8R3PvJ_XDO9k4kFy8GllRZNeBcNXsdSrjHgfHUPG34w-2Ahqwg4z8csPOHsVRb6i8vhc-SL_eHA1EuaEZN_S6iKdvW5PZrytxaFz4wG4hbezb8aTx2JXGMxp313A9dz8zEoI6bJfg_GILmi45HRdM9EaDtkDHJtBxuDnpUV_ARkwL73fLfce3JUzEANCy4Q2k29XwHZRdkq8id2tInUfjP9Im2mhDJPzSI1YGwf1LqPxaO7pot9bcodRhZamnsH5E74-D48F_Qy18oDw8PiFVOOf5wFHEDdKXXiNHoOmv209C8mawI5BA0qPGFvUpc-Cktl_-aM-eEAFYPS05m-x-9NsvXyOxM4fx0BDgkCJ1RYijVeS1RBHKE-bDNzC9_z9TWkYq5HP8eC5vkibhpa_wUDKDXrRbklQRqA3xxYOUcvxLY1cE" },
                { "Car", "https://cdn1.tenchat.ru/static/vbc-gostinder/2024-04-17/compressed/cae9c6a4-7249-4e15-be04-cd3054feee22.jpeg"},
                { "Piramid", "https://sun9-16.userapi.com/impg/JHuQrSt_q33upJbZszNll0w_mY3nSjKnZI_fMA/PrdnM02W74w.jpg?size=1280x720&quality=96&sign=05ebb63fd6ea22cb79c176ca2b018d13&c_uniq_tag=OPeiEHJdrQMsS3pxaL_FkLWUPnsbH6HLN7Vf49fgf-0&type=album"}
            };

            // Добавление файлов в закачку
            foreach (var item in downloadDictionary)
            {
                var imageName = item.Key;
                var imageUri = item.Value;
                downloader.AddDownload(imageName, imageUri);
            }

            // Цикл для пользователя
            while (true)
            {
                Console.WriteLine("Нажмите клавишу A для выхода или любую другую клавишу для проверки статуса скачивания");
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.A)
                {
                    downloader.CancelDownload(); // отмена всех заданий
                    break; // выход из цикла и программы
                }
                else
                {
                    // Выдать состояние закачки: загружена / не загружена
                    foreach (var item in downloader.DownloadList)
                    {
                        Console.WriteLine($"Изображение \"{item.Key}\" {(!item.Value.IsCompleted ? "не " : "")}загружено");
                    }
                }
            }
        }
    }
}
