using CooKit.Models.Pictograms;
using SQLite;

namespace CooKit.Repositories.Pictograms
{
    public sealed class SQLitePictogramRepository : SQLiteRepository<IPictogram, Pictogram>, IPictogramRepository
    {
        public SQLitePictogramRepository(SQLiteAsyncConnection connection) : base(connection)
        {
        }
    }
}
