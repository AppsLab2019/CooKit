using System.Threading.Tasks;
using CooKit.Mobile.Contexts;

namespace CooKit.Mobile.Services.Database
{
    public class EnsuringDatabaseInitializationService : IDatabaseInitializationService
    {
        private readonly RecipeContext _context;

        public EnsuringDatabaseInitializationService(RecipeContext context)
        {
            _context = context;
        }

        public Task InitializeDatabaseAsync()
        {
            return _context.Database.EnsureCreatedAsync();
        }
    }
}
