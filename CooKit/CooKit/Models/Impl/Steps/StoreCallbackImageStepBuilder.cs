using System;
using System.Threading.Tasks;
using CooKit.Models.Steps;
using CooKit.Services;

namespace CooKit.Models.Impl.Steps
{
    public sealed class StoreCallbackImageStepBuilder : IImageStepBuilder
    {
        public IBuilderProperty<IStepBuilder, Guid> Id { get; }
        public IBuilderProperty<IImageStepBuilder, string> ImageLoader { get; }
        public IBuilderProperty<IImageStepBuilder, string> ImageSource { get; }

        private readonly IStepStore _store;

        public StoreCallbackImageStepBuilder(IStepStore store, IStepBuilder builder)
        {
            _store = store;

            Id = new BuilderPropertyImpl<IStepBuilder, Guid>(this, builder.Id.Value);
            ImageLoader = new BuilderPropertyImpl<IImageStepBuilder, string>(this);
            ImageSource = new BuilderPropertyImpl<IImageStepBuilder, string>(this);
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
