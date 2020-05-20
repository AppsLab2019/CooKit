using System;
using System.Threading.Tasks;
using CooKit.Delegates;
using CooKit.Models.Messages;

namespace CooKit.Services.Messages
{
    public interface IMessageBroker
    {
        Task Send(IMessage message);
        Task Send(object sender, string title, object argument = null);

        void Attach(MessageHandler handler);
        void Attach(MessageHandler handler, string title);

        void Attach<TSender>(MessageHandler handler);
        void Attach(MessageHandler handler, Type senderType);

        void Detach(MessageHandler handler);
    }
}
