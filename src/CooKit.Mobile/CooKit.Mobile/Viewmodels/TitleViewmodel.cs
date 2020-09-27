using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CooKit.Mobile.Extensions;
using CooKit.Mobile.Models;
using CooKit.Mobile.Providers.Categories;
using CooKit.Mobile.Services.Feature;
using Xamarin.Forms;

namespace CooKit.Mobile.Viewmodels
{
    public class TitleViewmodel : ParameterlessBaseViewmodel
    {
        private readonly IFeatureService _featureService;
        private readonly ICategoryProvider _categoryProvider;

        public ICommand OpenCategoryCommand => new Command<TitleCategoryWrapper>(async category => await OpenCategoryAsync(category));
        public ICommand OpenRecipeCommand => new Command<Recipe>(async recipe => await OpenRecipeAsync(recipe));

        public TitleViewmodel(IFeatureService featureService, ICategoryProvider categoryProvider)
        {
            _featureService = featureService;
            _categoryProvider = categoryProvider;
        }

        public Recipe FeaturedRecipe
        {
            get => _featuredRecipe;
            set => OnPropertyChanged(ref _featuredRecipe, value);
        }

        public IList<TitleCategoryWrapper> Categories
        {
            get => _categories;
            set => OnPropertyChanged(ref _categories, value);
        }

        private Recipe _featuredRecipe;
        private IList<TitleCategoryWrapper> _categories;

        protected override async Task InitializeAsync()
        {
            var featuredTask = _featureService.GetFeaturedRecipeAsync();
            var categoryTask = _categoryProvider.GetCategoriesAsync();

            await Task.WhenAll(featuredTask, categoryTask);

            FeaturedRecipe = await featuredTask;
            var categories = await categoryTask;

            var wrappedTasks = categories.Select(async category =>
            {
                var recipes = await category.GetPreviewRecipesAsync();
                return new TitleCategoryWrapper(category, recipes);
            });

            Categories = await Task.WhenAll(wrappedTasks);
        }

        private Task OpenCategoryAsync(TitleCategoryWrapper category)
        {
            return category != null
                ? NavigationService.PushAsync<RecipeListViewmodel>(category.InnerCategory)
                : Task.CompletedTask;
        }

        private Task OpenRecipeAsync(Recipe recipe)
        {
            return recipe != null
                ? NavigationService.PushAsync<RecipeDetailViewmodel>(recipe)
                : Task.CompletedTask;
        }
    }
}
