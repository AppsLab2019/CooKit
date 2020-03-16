using System;
using System.Threading.Tasks;
using CooKit.Services;

namespace CooKit.Models.Impl
{
    public sealed class StoreCallbackRecipeBuilder : IRecipeBuilder
    {
        public IBuilderProperty<IRecipeBuilder, Guid> Id { get; }
        public IBuilderProperty<IRecipeBuilder, string> Name { get; }
        public IBuilderProperty<IRecipeBuilder, string> Description { get; }
        public IBuilderProperty<IRecipeBuilder, TimeSpan> RequiredTime { get; }

        public IBuilderProperty<IRecipeBuilder, string> ImageLoader { get; }
        public IBuilderProperty<IRecipeBuilder, string> ImageSource { get; }

        public IBuilderProperty<IRecipeBuilder, Guid[]> IngredientIds { get; }
        public IBuilderProperty<IRecipeBuilder, Guid[]> PictogramIds { get; }
        public IBuilderProperty<IRecipeBuilder, Guid[]> StepIds { get; }

        private readonly IRecipeStore _recipeStore;

        public StoreCallbackRecipeBuilder(IRecipeStore recipeStore)
        {
            Id = new BuilderPropertyImpl<IRecipeBuilder, Guid>(this, Guid.NewGuid());
            Name = new BuilderPropertyImpl<IRecipeBuilder, string>(this);
            Description = new BuilderPropertyImpl<IRecipeBuilder, string>(this);
            RequiredTime = new BuilderPropertyImpl<IRecipeBuilder, TimeSpan>(this);

            ImageLoader = new BuilderPropertyImpl<IRecipeBuilder, string>(this);
            ImageSource = new BuilderPropertyImpl<IRecipeBuilder, string>(this);

            IngredientIds = new BuilderPropertyImpl<IRecipeBuilder, Guid[]>(this, new Guid[0]);
            PictogramIds = new BuilderPropertyImpl<IRecipeBuilder, Guid[]>(this, new Guid[0]);
            StepIds = new BuilderPropertyImpl<IRecipeBuilder, Guid[]>(this, new Guid[0]);

            _recipeStore = recipeStore;
        }

        public async Task<IRecipe> BuildAsync()
        {
            await _recipeStore.AddAsync(this);
            return await _recipeStore.LoadAsync(Id.Value);
        }
    }
}
