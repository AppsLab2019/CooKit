using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Autofac;
using CooKit.Models;
using CooKit.Models.Steps;
using CooKit.Services;
using CooKit.Views.Editor;
using Xamarin.Forms;

namespace CooKit.ViewModels.Editor.Designers
{
    public sealed class RecipeDesignerViewModel : BaseSimpleDesignerViewModel<IRecipe, IRecipeBuilder>
    {
        public ReadOnlyObservableCollection<IIngredient> AvailableIngredients { get; }
        public ObservableCollection<object> SelectedIngredients { get; }

        public ReadOnlyObservableCollection<IPictogram> AvailablePictograms { get; }
        public ObservableCollection<object> SelectedPictograms { get; }

        public ObservableCollection<IRecipeStep> Steps { get; }
        public ICommand EditStepsCommand { get; }

        public RecipeDesignerViewModel() : base(App.Container.Resolve<IRecipeStore>())
        {
            var container = App.Container;

            AvailableIngredients = container.Resolve<IIngredientStore>().LoadedObjects;
            AvailablePictograms = container.Resolve<IPictogramStore>().LoadedObjects;

            SelectedIngredients = new ObservableCollection<object>();
            SelectedPictograms = new ObservableCollection<object>();

            Steps = new ObservableCollection<IRecipeStep>();
            EditStepsCommand = new Command(HandleEditSteps);
        }

        private async void HandleEditSteps() =>
            await PushAsync(new StepManagementView{ BindingContext = new StepManagementViewModel(Steps) });

        protected override Task Build()
        {
            var ingredientIds = SelectedIngredients
                .Cast<IIngredient>()
                .Select(ingredient => ingredient.Id)
                .ToArray();

            var pictogramIds = SelectedPictograms
                .Cast<IPictogram>()
                .Select(pictogram => pictogram.Id)
                .ToArray();

            var stepIds = Steps
                .Select(step => step.Id)
                .ToArray();

            return Builder
                .IngredientIds.Set(ingredientIds)
                .PictogramIds.Set(pictogramIds)
                .StepIds.Set(stepIds)
                .BuildAsync();
        }
    }
}
