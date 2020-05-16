using SQLite;

namespace CooKit.Services.Database
{
    public interface ISQLiteConnectionFactory
    {
        SQLiteAsyncConnection CreateConnection();
    }
}
