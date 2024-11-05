using System;
using System.Xml.Linq;

namespace DZ7_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var catalog = new PlanetCatalog();

            foreach (var planetName in new List<string> { "Земля", "Лимония", "Марс" })
            {
                var res = catalog.GetPlanet(planetName);
                if (res.errorMessage != "")
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
