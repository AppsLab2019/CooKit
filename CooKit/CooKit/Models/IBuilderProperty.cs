namespace CooKit.Models
{
    public interface IBuilderProperty<out TReturn, T>
    {
        T Value { get; set; }

        T Get();
        TReturn Set(T value);
    }
}
