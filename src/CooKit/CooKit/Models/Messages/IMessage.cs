namespace CooKit.Models.Messages
{
    public interface IMessage
    {
        object Sender { get; set; }
        string Title { get; set; }
        object Argument { get; set; }
    }
}
