using Autofac;
using CooKit.Repositories.Ingredients;
using CooKit.Repositories.Pictograms;
using CooKit.Repositories.Recipes;
using CooKit.Repositories.Steps;

namespace CooKit.Repositories
{
    public sealed class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SQLiteIngredientTemplateRepository>().As<IIngredientTemplateRepository>().SingleInstance();

            builder.RegisterType<SQLiteIngredientDtoRepository>().As<IIngredientDtoRepository>().SingleInstance();
            builder.RegisterType<SQLiteIngredientRepository>().As<IIngredientRepository>().SingleInstance();

            builder.RegisterType<SQLitePictogramRepository>().As<IPictogramRepository>().SingleInstance();

            builder.RegisterType<SQLiteRawStepDtoRepository>().As<ISQLiteRawStepDtoRepository>().SingleInstance();
            builder.RegisterType<SQLiteStepRepository>().As<IStepRepository>().SingleInstance();

            builder.RegisterType<SQLiteRawRecipeDtoRepository>().As<ISQLiteRawRecipeDtoRepository>().SingleInstance();
            builder.RegisterType<SQLiteRecipeRepository>().As<IRecipeRepository>().SingleInstance();
        }
    }
}
