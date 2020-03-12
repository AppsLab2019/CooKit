using System;
using CooKit.Services;

namespace CooKit.Models.Impl.SQLite
{
    internal sealed class SQLiteRecipeBuilder : IRecipeBuilder
    {
        public IBuilderProperty<IRecipeBuilder, Guid> Id { get; }
        public IBuilderProperty<IRecipeBuilder, string> Name { get; }
        public IBuilderProperty<IRecipeBuilder, string> Description { get; }

        public IBuilderProperty<IRecipeBuilder, string> ImageLoader { get; }
        public IBuilderProperty<IRecipeBuilder, string> ImageSource { get; }

        public IBuilderProperty<IRecipeBuilder, Guid[]> IngredientIds { get; }
        public IBuilderProperty<IRecipeBuilder, Guid[]> PictogramIds { get; }
        public IBuilderProperty<IRecipeBuilder, Guid[]> StepIds { get; }

        private readonly IRecipeStore _recipeStore;

        public SQLiteRecipeBuilder(IRecipeStore recipeStore)
        {
            Id = new BuilderPropertyImpl<IRecipeBuilder, Guid>(this, Guid.NewGuid());
            Name = new BuilderPropertyImpl<IRecipeBuilder, string>(this);
            Description = new BuilderPropertyImpl<IRecipeBuilder, string>(this);

            ImageLoader = new BuilderPropertyImpl<IRecipeBuilder, string>(this);
            ImageSource = new BuilderPropertyImpl<IRecipeBuilder, string>(this);

            IngredientIds = new BuilderPropertyImpl<IRecipeBuilder, Guid[]>(this);
            PictogramIds = new BuilderPropertyImpl<IRecipeBuilder, Guid[]>(this);
            StepIds = new BuilderPropertyImpl<IRecipeBuilder, Guid[]>(this);

            _recipeStore = recipeStore;
        }

        public IRecipe Build()
        {
            _recipeStore.AddRecipe(this);
            return _recipeStore.LoadRecipe(Id.Value);
        }
    }
}
