namespace DZ7_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var venus = new { Name = "Венера", 
                              PlaceFromSun = 2,
                              EquatorLength = 38025, // km
                              PreviousPlanet = new {
                                  Name = "Меркурий",
                                  PlaceFromSun = 1,
                                  EquatorLength = 15329, // km
                                  PreviousPlanet = (object ?) null
                              }
            };

            var earth = new
            {
                Name = "Земля",
                PlaceFromSun = 3,
                EquatorLength = 40075, // km
                PreviousPlanet = venus
            };

            var mars = new
            {
                Name = "Марс",
                PlaceFromSun = 4,
                EquatorLength = 21165, // km
                PreviousPlanet = earth
            };

            var venusDublicate = new
            {
                Name = "Венера",
                PlaceFromSun = 2,
                EquatorLength = 38025, // km
                PreviousPlanet = new
                {
                    Name = "Меркурий",
                    PlaceFromSun = 1,
                    EquatorLength = 15329, // km
                    PreviousPlanet = (object?)null
                }
            };

            Console.WriteLine($"Планета {venus.Name} в {venus.PlaceFromSun} позиции от Солнца. Длина экватора {venus.EquatorLength} км. Предыдущая планета {venus.PreviousPlanet.Name}.");
            Console.WriteLine($"Планета {venus.Name}{(venus.Equals(venus) ? " " : " не ")}эквивалентна Венере");

            Console.WriteLine($"Планета {earth.Name} в {earth.PlaceFromSun} позиции от Солнца. Длина экватора {earth.EquatorLength} км. Предыдущая планета {earth.PreviousPlanet.Name}.");
            Console.WriteLine($"Планета {earth.Name}{(earth.Equals(venus) ? " " : " не ")}эквивалентна Венере");

            Console.WriteLine($"Планета {mars.Name} в {mars.PlaceFromSun} позиции от Солнца. Длина экватора {mars.EquatorLength} км. Предыдущая планета {mars.PreviousPlanet.Name}.");
            Console.WriteLine($"Планета {mars.Name}{(earth.Equals(venus) ? " " : " не ")}эквивалентна Венере");

            Console.WriteLine($"Планета {venusDublicate.Name} в {venusDublicate.PlaceFromSun} позиции от Солнца. Длина экватора {venusDublicate.EquatorLength} км. Предыдущая планета {venusDublicate.PreviousPlanet.Name}.");
            Console.WriteLine($"Планета {venusDublicate.Name}{(venusDublicate.Equals(venus) ? " " : " не ")}эквивалентна Венере");
        }
    }
}
