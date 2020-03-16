using System;
using System.Threading.Tasks;
using CooKit.Models.Steps;
using CooKit.Services;

namespace CooKit.Models.Impl.Steps
{
    public sealed class StoreCallbackTextRecipeStepBuilder : ITextRecipeStepBuilder
    {
        public IBuilderProperty<IRecipeStepBuilder, Guid> Id { get; }
        public IBuilderProperty<ITextRecipeStepBuilder, string> Text { get; }

        private readonly IRecipeStepStore _store;

        public StoreCallbackTextRecipeStepBuilder(IRecipeStepStore store, IRecipeStepBuilder baseBuilder)
        {
            _store = store;

            Id = new BuilderPropertyImpl<IRecipeStepBuilder, Guid>(this, baseBuilder.Id.Value);
            Text = new BuilderPropertyImpl<ITextRecipeStepBuilder, string>(this);
        }

        public ITextRecipeStepBuilder ToTextBuilder() =>
            throw new NotSupportedException();

        public async Task<IRecipeStep> BuildAsync()
        {
            await _store.AddAsync(this);
            return await _store.LoadAsync(Id.Value);
        }
    }
}
