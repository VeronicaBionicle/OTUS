namespace DZ7_2
{
    public class PlanetCatalog
    {
        public List<Planet> planets = new List<Planet>();
        public PlanetCatalog() {
            planets.Add(new Planet("Венера", 2, 38025, null));
            planets.Add(new Planet("Земля", 3, 40075, planets.Last()));
            planets.Add(new Planet("Марс", 4, 21165, planets.Last()));
        }

        int count = 0;
        public (int PlaceFromSun, int EquatorLength, string errorMessage) GetPlanet (string planetName)
        {
            count++;
            if (count == 3)
            {
                count = 0;
                return (-1, -1, "Вы спрашиваете слишком часто");
            }

            foreach (Planet planet in planets)
            {
                if (planet.Name == planetName)
                {
                    return (planet.PlaceFromSun, planet.EquatorLength, "");
                }
            }
            return (-1, -1, "Не удалось найти планету");
        }
    }
}
