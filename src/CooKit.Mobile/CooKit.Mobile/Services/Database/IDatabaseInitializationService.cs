using System.Threading.Tasks;

namespace CooKit.Mobile.Services.Database
{
    public interface IDatabaseInitializationService
    {
        Task InitializeDatabaseAsync();
    }
}
