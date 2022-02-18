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
                Wine w = new Wine
                {
                    ID = 0,
                    Name = "Domini Veneti Valpolicella Ripasso",
                    Place = "Valpolicella",
                    Region = "Valpolicella",
                    Country = "Italy",
                    Price = 169,
                    WineType = "Red",


                };
                wines.Add(new Wine());
            }

            return wines;
        }
    }
}
