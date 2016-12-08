using System.Data.Entity;

namespace TreeApi.DbModels
{
    public class TreeApiContext : DbContext
    {
        public TreeApiContext() : base("name=TreeApiContext")
        {
        }

        public DbSet<World> Worlds { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Region> Regions { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Street> Streets { get; set; }

        public DbSet<Building> Buildings { get; set; }

        public DbSet<Appartment> Appartments { get; set; }
    }
}