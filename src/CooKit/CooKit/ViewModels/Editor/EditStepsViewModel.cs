using System.Threading.Tasks;
using CooKit.Models.Editor.CollectionElementPair;
using CooKit.Models.Steps;
using CooKit.ViewModels.Editor.Steps;

namespace CooKit.ViewModels.Editor
{
    public sealed class EditStepsViewModel : ViewModel
    {
        private Task EditTextStep(IStep step)
        {
            var pair = new CollectionElementPair<IStep>(null, step);
            return NavigationService.PushAsync<EditTextStepViewModel>(pair);
        }
    }
}
