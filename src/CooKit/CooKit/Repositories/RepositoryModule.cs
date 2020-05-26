using Autofac;
using CooKit.Repositories.Ingredients;
using CooKit.Repositories.Pictograms;
using CooKit.Repositories.Recipes;

namespace CooKit.Repositories
{
    public sealed class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SQLiteIngredientTemplateRepository>().As<IIngredientTemplateRepository>().SingleInstance();

            builder.RegisterType<SQLitePictogramRepository>().As<IPictogramRepository>().SingleInstance();

            builder.RegisterType<SQLiteRawRecipeDtoRepository>().As<ISQLiteRawRecipeDtoRepository>().SingleInstance();
            builder.RegisterType<SQLiteRecipeRepository>().As<IRecipeRepository>().SingleInstance();
        }
    }
}
