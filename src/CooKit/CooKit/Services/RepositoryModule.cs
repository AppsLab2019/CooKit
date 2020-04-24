using Autofac;
using CooKit.Services.Repositories.Ingredients;
using CooKit.Services.Repositories.Pictograms;
using CooKit.Services.Repositories.Recipes;

namespace CooKit.Services
{
    public sealed class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SQLiteIngredientTemplateRepository>().As<IIngredientTemplateRepository>().SingleInstance();

            builder.RegisterType<SQLiteIngredientDtoRepository>().As<ISQLiteIngredientDtoRepository>().SingleInstance();
            builder.RegisterType<SQLiteIngredientRepository>().As<IIngredientRepository>().SingleInstance();

            builder.RegisterType<SQLitePictogramRepository>().As<IPictogramRepository>().SingleInstance();

            builder.RegisterType<SQLiteRecipeDtoRepository>().As<ISQLiteRecipeDtoRepository>().SingleInstance();
            builder.RegisterType<SQLiteRecipeRepository>().As<IRecipeRepository>().SingleInstance();
        }
    }
}
