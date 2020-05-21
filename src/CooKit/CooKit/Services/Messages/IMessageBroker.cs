using System.Threading.Tasks;
using CooKit.Delegates;

namespace CooKit.Services.Messages
{
    public interface IMessageBroker
    {
        void Subscribe(object receiver, MessageHandler handler);
        void Subscribe(object receiver, MessageHandler handler, string title);

        void Subscribe<TSender>(object receiver, MessageHandlerSend<TSender> handler);
        void Subscribe<TSender>(object receiver, MessageHandlerSend<TSender> handler, string title);

        void Subscribe<TParam>(object receiver, MessageHandlerParam<TParam> handler);
        void Subscribe<TParam>(object receiver, MessageHandlerParam<TParam> handler, string title);

        void Subscribe<TSender, TParam>(object receiver, MessageHandlerSendParam<TSender, TParam> handler);
        void Subscribe<TSender, TParam>(object receiver, MessageHandlerSendParam<TSender, TParam> handler, string title);

        Task Send(object sender, string title, object param = null);
    }
}
