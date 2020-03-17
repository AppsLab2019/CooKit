using System;
using System.Threading.Tasks;
using CooKit.Models.Steps;
using CooKit.Services;

namespace CooKit.Models.Impl.Steps
{
    public sealed class StoreCallbackBigImageRecipeStepBuilder : IBigImageRecipeStepBuilder
    {
        public IBuilderProperty<IRecipeStepBuilder, Guid> Id { get; }
        public IBuilderProperty<IBigImageRecipeStepBuilder, string> ImageLoader { get; }
        public IBuilderProperty<IBigImageRecipeStepBuilder, string> ImageSource { get; }

        private readonly IRecipeStepStore _store;

        public StoreCallbackBigImageRecipeStepBuilder(IRecipeStepStore store, IRecipeStepBuilder builder)
        {
            _store = store;

            Id = new BuilderPropertyImpl<IRecipeStepBuilder, Guid>(this, builder.Id.Value);
            ImageLoader = new BuilderPropertyImpl<IBigImageRecipeStepBuilder, string>(this);
            ImageSource = new BuilderPropertyImpl<IBigImageRecipeStepBuilder, string>(this);
        }

        public ITextRecipeStepBuilder ToTextBuilder() => 
            throw new NotSupportedException();

        public IBigImageRecipeStepBuilder ToBigImageBuilder() => 
            throw new NotSupportedException();

        public async Task<IRecipeStep> BuildAsync()
        {
            await _store.AddAsync(this);
            return await _store.LoadAsync(Id.Value);
        }
    }
}
