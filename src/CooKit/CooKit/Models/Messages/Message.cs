namespace CooKit.Models.Messages
{
    public sealed class Message : IMessage
    {
        public object Sender { get; set; }
        public string Title { get; set; }
        public object Argument { get; set; }

        public Message()
        {
        }

        public Message(object sender, string title, object argument)
        {
            Sender = sender;
            Title = title;
            Argument = argument;
        }
    }
}
