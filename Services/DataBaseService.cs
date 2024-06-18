using SQLite;
using MauiAppSQLite1.Models;



namespace MauiAppSQLite1.Services
{
    public class DatabaseService
    {
        private static SQLiteAsyncConnection _database;
        public DatabaseService()
        {
            SetUpDb();
        }
        private async void SetUpDb()
        {
            try
            {
                if (_database == null)
                {
                    string? dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "VedatSQLite.db3");
                    _database = new SQLiteAsyncConnection(dbPath);
                    await _database.CreateTableAsync<Item>();
                }
            }
            catch (Exception e)
            {
                string x = e.Message;
            }
        }

        public Task<List<Item>> GetItemsAsync()
        {
            var xx = _database.Table<Item>().ToListAsync();
            int i = xx.Result.Count;

            return _database.Table<Item>().ToListAsync();
        }

        public Task<Item> GetItemAsync(int id)
        {
            return _database.Table<Item>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(Item item)
        {
            if (item.Id != 0)
            {
                return _database.UpdateAsync(item);
            }
            else
            {
                return _database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(Item item)
        {
            return _database.DeleteAsync(item);
        }
    }
}
