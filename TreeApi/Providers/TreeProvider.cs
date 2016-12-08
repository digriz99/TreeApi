using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreeApi.Models;
using System.Data.Entity;

namespace TreeApi.Providers
{
    public class TreeProvider : ITreeProvider
    {
        private DbModels.TreeApiContext _db;

        public TreeProvider(DbModels.TreeApiContext db)
        {
            _db = db;
        }

        public async Task<World[]> BuildTree()
        {
            if (!(await _db.Worlds.AnyAsync()))
            {
                await FillDatabase();
            }

            var apptsDict = GetDictionary(
                await _db.Appartments.ToListAsync(),
                i => new Appartment() { Name = i.Name });

            var housesDict = GetDictionary(
                await _db.Buildings.ToListAsync(),
                i => new Building() { Name = i.Name, Appartments = GetDictionaryValue(apptsDict, i.Id) });

            var streetsDict = GetDictionary(
                await _db.Streets.ToListAsync(),
                i => new Street() { Name = i.Name, Buildings = GetDictionaryValue(housesDict, i.Id) });

            var citiesDict = GetDictionary(
                await _db.Cities.ToListAsync(),
                i => new City() { Name = i.Name, Streets = GetDictionaryValue(streetsDict, i.Id) });

            var regionsDict = GetDictionary(
                await _db.Regions.ToListAsync(),
                i => new Region() { Name = i.Name, Cities = GetDictionaryValue(citiesDict, i.Id) });

            var countriesDict = GetDictionary(
                await _db.Countries.ToListAsync(),
                i => new Country() { Name = i.Name, Regions = GetDictionaryValue(regionsDict, i.Id) });

            return (await _db.Worlds.ToListAsync())
                .Select(i => new World() { Name = i.Name, Countries = GetDictionaryValue(countriesDict, i.Id) }).ToArray();
        }


        #region Private

        private Dictionary<int, List<T>> GetDictionary<T, Y>(List<Y> items, Func<Y, T> convert)
            where Y : DbModels.IElement
        {
            var dictionary = new Dictionary<int, List<T>>();

            items.ForEach(item =>
            {
                if (dictionary.ContainsKey(item.Pid))
                {
                    dictionary[item.Pid].Add(convert(item));
                }
                else
                {
                    dictionary.Add(item.Pid, new List<T>() { convert(item) });
                }
            });

            return dictionary;
        }


        private T[] GetDictionaryValue<T>(Dictionary<int, List<T>> dict, int key)
        {
            return dict.ContainsKey(key) ? dict[key].ToArray() : new T[0];
        }

        private async Task FillDatabase()
        {
            var worlds = new List<DbModels.World>() { new DbModels.World() { Id = 1, Pid = -1, Name = "World" } };
            var countries = new List<DbModels.Country>();
            var regions = new List<DbModels.Region>();
            var cities = new List<DbModels.City>();
            var streets = new List<DbModels.Street>();
            var buildings = new List<DbModels.Building>();
            var appartments = new List<DbModels.Appartment>();

            FillItem(worlds, countries, "Country");
            FillItem(countries, regions, "Region");
            FillItem(regions, cities, "City");
            FillItem(cities, streets, "Street");
            FillItem(streets, buildings, "Building");
            FillItem(buildings, appartments, "Appartment");

            worlds.ForEach(c => _db.Worlds.Add(c));
            countries.ForEach(c => _db.Countries.Add(c));
            regions.ForEach(c => _db.Regions.Add(c));
            cities.ForEach(c => _db.Cities.Add(c));
            streets.ForEach(c => _db.Streets.Add(c));
            buildings.ForEach(c => _db.Buildings.Add(c));
            appartments.ForEach(c => _db.Appartments.Add(c));

            await _db.SaveChangesAsync();
        }

        private void FillItem<T, Y>(List<T> parent, List<Y> chield, string name)
            where T : DbModels.IElement
            where Y : DbModels.IElement, new()
        {
            parent.ForEach(p =>
            {
                var index = chield.Count + 1;
                for (int i = index; i < index + 5; i++)
                {
                    chield.Add(new Y { Id = i, Pid = p.Id, Name = $"{name}{i}" });
                }
            });
        }

        #endregion

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}