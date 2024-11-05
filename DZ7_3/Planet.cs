namespace DZ7_3
{
    public class Planet
    {
        public string Name { get; set; }
        public int PlaceFromSun { get; set; }
        public int EquatorLength { get; set; } // km
        public Planet? PreviousPlanet { get; set; }

        public Planet(string name, int placeFromSun, int equatorLength, Planet? previousPlanet)
        {
            Name = name;
            PlaceFromSun = placeFromSun;
            EquatorLength = equatorLength;
            PreviousPlanet = previousPlanet;
        }
    }
}