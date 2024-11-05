using System;
using System.Xml.Linq;

namespace DZ7_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var catalog = new PlanetCatalog();
            var checkList = new List<string> { "Земля", "Лимония", "Марс" };

            /* Первый цикл проверок */
            Console.WriteLine("Первый цикл проверок");
            var countCall = 0;
            foreach (var planetName in checkList)
            {
                var res = catalog.GetPlanet(planetName, planetName => 
                {
                    countCall++;
                    if (countCall >= 3) {
                        countCall = 0;
                        return "Вы спрашиваете слишком часто";
                    }
                    return null;
                });

                if (res.errorMessage != null)
                {
                    Console.WriteLine($"Для планеты {planetName} ошибка: {res.errorMessage}");
                }
                else
                {
                    Console.WriteLine($"Название планеты: {planetName}, порядковый номер: {res.PlaceFromSun}, длина экватора: {res.EquatorLength}");
                }
            }

            /* Второй цикл проверок */
            Console.WriteLine("Второй цикл проверок");
            foreach (var planetName in checkList)
            {
                var res = catalog.GetPlanet(planetName, planetName => (planetName == "Лимония" ? "Это запретная планета" : null));
                if (res.errorMessage != null)
                {
                    Console.WriteLine($"Для планеты {planetName} ошибка: {res.errorMessage}");
                }
                else
                {
                    Console.WriteLine($"Название планеты: {planetName}, порядковый номер: {res.PlaceFromSun}, длина экватора: {res.EquatorLength}");
                }
            }
        }
    }
}