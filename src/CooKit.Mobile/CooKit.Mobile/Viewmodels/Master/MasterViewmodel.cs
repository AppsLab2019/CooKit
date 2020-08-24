using System.Threading.Tasks;
using CooKit.Mobile.Services.Root;
using CooKit.Mobile.Viewmodels.Lists;

namespace CooKit.Mobile.Viewmodels.Master
{
    public class MasterViewmodel : ParameterlessBaseViewmodel
    {
        private readonly IRootService _rootService;

        public MasterViewmodel(IRootService rootService)
        {
            _rootService = rootService;
        }

        protected override Task InitializeAsync()
        {
            return _rootService.SetRootAsync(typeof(LocalRecipeListViewmodel));
        }
    }
}
