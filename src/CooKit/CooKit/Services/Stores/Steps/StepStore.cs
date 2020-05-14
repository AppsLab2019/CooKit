using CooKit.Models.Steps;
using CooKit.Repositories.Steps;

namespace CooKit.Services.Stores.Steps
{
    public sealed class StepStore : RepositoryStore<IStep>, IStepStore
    {
        public StepStore(IStepRepository repository) : base(repository)
        {
        }
    }
}
