namespace DZ7_3
{
    public class PlanetCatalog
    {
        public List<Planet> planets = new List<Planet>();

        public PlanetCatalog()
        {
            planets.Add(new Planet("Венера", 2, 38025, null));
            planets.Add(new Planet("Земля", 3, 40075, planets.Last()));
            planets.Add(new Planet("Марс", 4, 21165, planets.Last()));
        }

        public (int PlaceFromSun, int EquatorLength, string ? errorMessage) GetPlanet(string planetName, Func <string, string ?> PlanetValidator)
        {
            var message = PlanetValidator(planetName);
            if (message != null)
            {
                return (-1, -1, message);
            }

            foreach (Planet planet in planets)
            {
                if (planet.Name == planetName)
                {
                    return (planet.PlaceFromSun, planet.EquatorLength, null);
                }
            }
            return (-1, -1, "Не удалось найти планету");
        }
    }
}