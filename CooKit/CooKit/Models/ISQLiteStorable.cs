namespace CooKit.Models
{
    public interface ISQLiteStorable<out TInfo> : IStorable
    {
        TInfo InternalInfo { get; }
    }
}
