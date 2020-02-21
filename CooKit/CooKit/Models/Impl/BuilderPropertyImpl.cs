namespace CooKit.Models.Impl
{
    public sealed class BuilderPropertyImpl<TReturn, T> : IBuilderProperty<TReturn, T>
    {
        public T Value { get; set; }

        private readonly TReturn _builder;

        public BuilderPropertyImpl(TReturn builder, T value = default)
        {
            _builder = builder;
            Value = value;
        }

        public T Get() =>
            Value;

        public TReturn Set(T value)
        {
            Value = value;
            return _builder;
        }
    }
}
