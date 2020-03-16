using System;

namespace CooKit.Models
{
    public interface IPictogramBuilder : IAsyncBuilder<IPictogram>
    {
        IBuilderProperty<IPictogramBuilder, Guid> Id { get; }
        IBuilderProperty<IPictogramBuilder, string> Name { get; }
        IBuilderProperty<IPictogramBuilder, string> Description { get; }

        IBuilderProperty<IPictogramBuilder, string> ImageLoader { get; }
        IBuilderProperty<IPictogramBuilder, string> ImageSource { get; }
    }
}
