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

        public Task<List<Wine>> GetAllWinesAsync()
        {
            return database.Table<Wine>().ToListAsync();
        }

        public Task<Wine> GetWineAsync(int id)
        {
            return database.Table<Wine>()
                .Where(i => i.ID == id).FirstOrDefaultAsync();
        }

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

        public Task<int> DeleteWineAsync(Wine wine)
        {
            return database.DeleteAsync(wine);
        }

        public Task<int> DeleteAllWinesAsync()
        {
            return database.DeleteAllAsync<Wine>();
        }
    }
}
