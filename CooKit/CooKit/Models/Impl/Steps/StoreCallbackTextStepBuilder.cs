using System;
using System.Threading.Tasks;
using CooKit.Models.Steps;
using CooKit.Services;

namespace CooKit.Models.Impl.Steps
{
    public sealed class StoreCallbackTextStepBuilder : ITextStepBuilder
    {
        public IBuilderProperty<IStepBuilder, Guid> Id { get; }
        public IBuilderProperty<ITextStepBuilder, string> Text { get; }

        private readonly IStepStore _store;

        public StoreCallbackTextStepBuilder(IStepStore store, IStepBuilder baseBuilder)
        {
            _store = store;

            Id = new BuilderPropertyImpl<IStepBuilder, Guid>(this, baseBuilder.Id.Value);
            Text = new BuilderPropertyImpl<ITextStepBuilder, string>(this);
        }

        public ITextStepBuilder ToTextBuilder() =>
            throw new NotSupportedException();

        public IImageStepBuilder ToBigImageBuilder() => 
            throw new NotSupportedException();

        public async Task<IStep> BuildAsync()
        {
            await _store.AddAsync(this);
            return await _store.LoadAsync(Id.Value);
        }
    }
}
