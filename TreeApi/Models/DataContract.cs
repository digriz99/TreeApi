namespace TreeApi.Models
{
    public class Element
    {
        public string Name { get; set; }
    }

    public class Appartment : Element
    {
    }

    public class Building : Element
    {
        public Appartment[] Appartments { get; set; }
    }

    public class Street : Element
    {
        public Building[] Buildings { get; set; }
    }

    public class City : Element
    {
        public Street[] Streets { get; set; }
    }

    public class Region : Element
    {
        public City[] Cities { get; set; }
    }

    public class Country : Element
    {
        public Region[] Regions { get; set; }
    }

    public class World : Element
    {
        public Country[] Countries { get; set; }
    }
}