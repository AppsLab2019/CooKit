using System;
using System.Threading.Tasks;
using CooKit.Services;

namespace CooKit.Models.Impl
{
    public sealed class StoreCallbackPictogramBuilder : IPictogramBuilder
    {
        public IBuilderProperty<IPictogramBuilder, Guid> Id { get; }
        public IBuilderProperty<IPictogramBuilder, string> Name { get; }
        public IBuilderProperty<IPictogramBuilder, string> Description { get; }

        public IBuilderProperty<IPictogramBuilder, string> ImageLoader { get; }
        public IBuilderProperty<IPictogramBuilder, string> ImageSource { get; }

        private readonly IPictogramStore _pictogramStore;

        public StoreCallbackPictogramBuilder(IPictogramStore pictogramStore)
        {
            _pictogramStore = pictogramStore;

            Id = new BuilderPropertyImpl<IPictogramBuilder, Guid>(this, Guid.NewGuid());
            Name = new BuilderPropertyImpl<IPictogramBuilder, string>(this);
            Description = new BuilderPropertyImpl<IPictogramBuilder, string>(this);

            ImageLoader = new BuilderPropertyImpl<IPictogramBuilder, string>(this);
            ImageSource = new BuilderPropertyImpl<IPictogramBuilder, string>(this);
        }

        public async Task<IPictogram> BuildAsync()
        {
            await _pictogramStore.AddAsync(this);
            return await _pictogramStore.LoadAsync(Id.Value);
        }
    }
}
