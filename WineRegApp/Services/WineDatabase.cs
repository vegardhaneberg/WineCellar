using System;
using WineRegApp.Models;
using SQLite;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WineRegApp.Services
{
    public class WineDatabase
    {
        readonly SQLiteAsyncConnection database;

        public WineDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Wine>().Wait();
        }

        // Get methods
        public Task<List<Wine>> GetAllWinesAsync()
        {
            return database.Table<Wine>().ToListAsync();
        }
        public async Task<List<Wine>> GetAllWinesAsync(WineRequest request)
        {
            if (request.SortValue == WineSortCategory.Name)
            {
                if (request.Ascending) return await database.Table<Wine>().OrderBy(obj => obj.Name).ToListAsync();
                else return await database.Table<Wine>().OrderByDescending(obj => obj.Name).ToListAsync();
            }
            else if (request.SortValue == WineSortCategory.Year)
            {
                if (request.Ascending) return await database.Table<Wine>().OrderBy(obj => obj.Year).ToListAsync();
                else return await database.Table<Wine>().OrderByDescending(obj => obj.Year).ToListAsync();
            }
            else if (request.SortValue == WineSortCategory.Price)
            {
                if (request.Ascending) return await database.Table<Wine>().OrderBy(obj => obj.Price).ToListAsync();
                else return await database.Table<Wine>().OrderByDescending(obj => obj.Year).ToListAsync();
            }
            // Assumes that the final sort category is CanDrinkToDate (Drink Urgency).
            // If more sorting options are added, include them as else if above this one. 
            else
            {
                if (request.Ascending) return await database.Table<Wine>().OrderBy(obj => obj.CanDrinkToDate).ToListAsync();
                else return await database.Table<Wine>().OrderByDescending(obj => obj.CanDrinkToDate).ToListAsync();

            }
        }
        public Task<Wine> GetWineAsync(int id)
        {
            return database.Table<Wine>()
                .Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        // Save Mathods
        public Task<int> SaveWineAsync(Wine wine)
        {
            if (wine.ID != 0)
            {
                return database.UpdateAsync(wine);
            }
            else
            {
                return database.InsertAsync(wine);
            }
        }
        public async Task<int> SaveWineAsync(List<Wine> wines)
        {
            int rowsAdded = 0;
            foreach (Wine w in wines)
            {
                rowsAdded += await SaveWineAsync(w);
            }
            return rowsAdded;
        }

        // Delete Methods 
        public Task<int> DeleteWineAsync(Wine wine)
        {
            return database.DeleteAsync(wine);
        }

        public Task<int> DeleteAllWinesAsync()
        {
            return database.DeleteAllAsync<Wine>();
        }

        // Insert Dummy Data Method
        public async Task<List<Wine>> InsertDummyData()
        {
            List<Wine> wines = await database.Table<Wine>().ToListAsync();

            if (wines.Count == 0)
            {
                Wine w1 = new Wine
                {
                    Name = "Domini Veneti Valpolicella Ripasso",
                    Place = "Valpolicella",
                    Region = "Valpolicella",
                    Country = "Italy",
                    Price = 169,
                    WineType = WineType.Red,
                    Year = 2020,
                    CanDrinkFromDate = new DateTime(2022, 10, 20),
                    CanDrinkToDate = new DateTime(2024, 10, 20),
                    StoredDate = new DateTime(2022, 1, 1)
                };
                Wine w2 = new Wine
                {
                    Name = "Periquita Rosé",
                    Place = "Terras do Sado",
                    Region = "Terras do Sado",
                    Country = "Portugal",
                    Price = 135,
                    WineType = WineType.Rose,
                    Year = 2020,
                    CanDrinkFromDate = new DateTime(2027, 10, 20),
                    CanDrinkToDate = new DateTime(2029, 10, 20),
                    StoredDate = new DateTime(2020, 10, 1)
                };
                Wine w3 = new Wine
                {
                    Name = "Viña Ardanza Reserva",
                    Place = "Rijoa",
                    Region = "Rijoa",
                    Country = "Spain",
                    Price = 279,
                    WineType = WineType.Red,
                    Year = 2015,
                    CanDrinkFromDate = new DateTime(2029, 10, 20),
                    CanDrinkToDate = new DateTime(2030, 10, 20),
                    StoredDate = new DateTime(2018, 1, 1)
                };
                Wine w4 = new Wine
                {
                    Name = "Domaine d'Andezon",
                    Place = "Rhone",
                    Region = "Rhone",
                    Country = "France",
                    Price = 149,
                    WineType = WineType.Red,
                    Year = 2021,
                    CanDrinkFromDate = new DateTime(2022, 8, 20),
                    CanDrinkToDate = new DateTime(2023, 5, 21),
                    StoredDate = new DateTime(2021, 6, 13)
                };
                Wine w5 = new Wine
                {
                    Name = "Giovanni Rosso Langhe Nebbiolo",
                    Place = "Langhe",
                    Region = "Langhe",
                    Country = "Italy",
                    Price = 209,
                    WineType = WineType.Red,
                    Year = 2019,
                    CanDrinkFromDate = new DateTime(2025, 10, 20),
                    CanDrinkToDate = new DateTime(2028, 10, 20),
                    StoredDate = new DateTime(2018, 1, 1)
                };
                Wine w6 = new Wine
                {
                    Name = "Chateau Dudon",
                    Place = "Bordeaux",
                    Region = "Bordeaux",
                    Country = "Italy",
                    Price = 469,
                    WineType = WineType.Red,
                    Year = 2010,
                    CanDrinkFromDate = new DateTime(2024, 10, 20),
                    CanDrinkToDate = new DateTime(2025, 10, 20),
                    StoredDate = new DateTime(2021, 10, 1)
                };
                Wine w7 = new Wine
                {
                    Name = "Chateau Musar",
                    Place = "Bekaay Valley",
                    Region = "Bekaay Valley",
                    Country = "Libanon",
                    Price = 169,
                    WineType = WineType.Red,
                    Year = 2016,
                    CanDrinkFromDate = new DateTime(2022, 10, 20),
                    CanDrinkToDate = new DateTime(2024, 10, 20),
                    StoredDate = new DateTime(2022, 1, 1)
                };
                Wine w8 = new Wine
                {
                    Name = "Domaine Colombier Crozes Hermitage",
                    Place = "Rhone",
                    Region = "Crozes Hermitage",
                    Country = "France",
                    Price = 155,
                    WineType = WineType.Red,
                    Year = 2019,
                    CanDrinkFromDate = new DateTime(2022, 10, 20),
                    CanDrinkToDate = new DateTime(2023, 10, 20),
                    StoredDate = new DateTime(2020, 1, 1)
                };
                Wine w9 = new Wine
                {
                    Name = "Faiveley Bourgogne Chardonnay",
                    Place = "Burgund",
                    Region = "Burgund",
                    Country = "France",
                    Price = 260,
                    WineType = WineType.White,
                    Year = 2020,
                    CanDrinkFromDate = new DateTime(2028, 10, 20),
                    CanDrinkToDate = new DateTime(2030, 10, 20),
                    StoredDate = new DateTime(2014, 1, 1)
                };
                Wine w10 = new Wine
                {
                    Name = "Domaine Romanee Conti",
                    Place = "Burgund",
                    Region = "Cote d'Or",
                    Country = "France",
                    Price = 10000,
                    WineType = WineType.Red,
                    Year = 1998,
                    CanDrinkFromDate = new DateTime(2022, 10, 20),
                    CanDrinkToDate = new DateTime(2023, 12, 20),
                    StoredDate = new DateTime(2010, 1, 1)
                };

                wines.Add(w1);
                wines.Add(w2);
                wines.Add(w3);
                wines.Add(w4);
                wines.Add(w5);
                wines.Add(w6);
                wines.Add(w7);
                wines.Add(w8);
                wines.Add(w9);
                wines.Add(w10);
                await SaveWineAsync(wines);

                return wines;
            }

            return null;
        }
    }
}
