using Autofac;
using CooKit.ViewModels.Recipes;

namespace CooKit.ViewModels
{
    public class ViewModelModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RecipeListViewModel>();
            builder.RegisterType<RecipeViewModel>();
        }
    }
}
