using CooKit.Models;
using CooKit.Services.Repositories.Pictograms;

namespace CooKit.Services.Stores.Pictograms
{
    public sealed class PictogramStore : RepositoryStore<Pictogram>, IPictogramStore
    {
        public PictogramStore(IPictogramRepository repository) : base(repository)
        {
        }
    }
}
