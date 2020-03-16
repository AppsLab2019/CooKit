using System;
using System.Threading.Tasks;
using CooKit.Models.Steps;
using CooKit.Services;

namespace CooKit.Models.Impl.Steps
{
    public sealed class StoreCallbackRecipeStepBuilder : IRecipeStepBuilder
    {
        public IBuilderProperty<IRecipeStepBuilder, Guid> Id { get; }
        private readonly IRecipeStepStore _store;

        public StoreCallbackRecipeStepBuilder(IRecipeStepStore store)
        {
            _store = store;
            Id = new BuilderPropertyImpl<IRecipeStepBuilder, Guid>(this, Guid.NewGuid());
        }

        public ITextRecipeStepBuilder ToTextBuilder() =>
            new StoreCallbackTextRecipeStepBuilder(_store, this);

        public Task<IRecipeStep> BuildAsync() =>
            throw new NotSupportedException();
    }
}
