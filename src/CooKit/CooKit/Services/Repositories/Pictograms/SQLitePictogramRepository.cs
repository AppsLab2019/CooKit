using CooKit.Models;
using SQLite;

namespace CooKit.Services.Repositories.Pictograms
{
    public sealed class SQLitePictogramRepository : GenericSQLiteRepository<Pictogram>, IPictogramRepository
    {
        public SQLitePictogramRepository(SQLiteAsyncConnection connection) : base(connection)
        {
        }
    }
}
