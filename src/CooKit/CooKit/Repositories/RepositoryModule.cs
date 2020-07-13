using Autofac;
using CooKit.Models.Ingredients;
using CooKit.Models.Pictograms;
using CooKit.Models.Recipes;
using CooKit.Repositories.Recipes;

namespace CooKit.Repositories
{
    public sealed class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType(typeof(SQLiteConcreteRepository<>)).As(typeof(IRepository<>));

            builder.RegisterType<SQLiteRepository<IIngredientTemplate, IngredientTemplate>>()
                .As<IRepository<IIngredientTemplate>>();

            builder.RegisterType<SQLiteRepository<IPictogram, Pictogram>>().As<IRepository<IPictogram>>();
            builder.RegisterType<SQLiteRecipeRepository>().As<IRepository<IRecipe>>();
        }
    }
}
