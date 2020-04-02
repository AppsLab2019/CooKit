using System;

namespace CooKit.Models.Steps
{
    public interface IStepBuilder : IAsyncBuilder<IStep>
    {
        IBuilderProperty<IStepBuilder, Guid> Id { get; }

        ITextStepBuilder ToTextBuilder();
        IImageStepBuilder ToBigImageBuilder();
    }
}
