using System;
using System.Threading.Tasks;
using CooKit.Models.Steps;
using CooKit.Services;

namespace CooKit.Models.Impl.Steps
{
    public sealed class StoreCallbackStepBuilder : IStepBuilder
    {
        public IBuilderProperty<IStepBuilder, Guid> Id { get; }
        private readonly IStepStore _store;

        public StoreCallbackStepBuilder(IStepStore store)
        {
            _store = store;
            Id = new BuilderPropertyImpl<IStepBuilder, Guid>(this, Guid.NewGuid());
        }

        public ITextStepBuilder ToTextBuilder() =>
            new StoreCallbackTextStepBuilder(_store, this);

        public IImageStepBuilder ToBigImageBuilder() => 
            new StoreCallbackImageStepBuilder(_store, this);

        public Task<IStep> BuildAsync() =>
            throw new NotSupportedException();
    }
}
