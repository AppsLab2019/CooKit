using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CooKit.Delegates;

namespace CooKit.Services.Messages
{
    // TODO: unsubscribing, auto-removing old entries
    public sealed class MessageBroker : IMessageBroker
    {
        private readonly IList<Subscription> _subscriptions;

        public MessageBroker()
        {
            _subscriptions = new List<Subscription>();
        }

        #region Subscribe Method Overloads

        public void Subscribe(object receiver, MessageHandler handler)
        {
            InternalSubscribe(receiver, handler, (_, __, ___) => true);
        }

        public void Subscribe(object receiver, MessageHandler handler, string title)
        {
            InternalSubscribe(receiver, handler, (_, messageTitle, __) => title == messageTitle);
        }

        public void Subscribe<TSender>(object receiver, MessageHandlerSend<TSender> handler)
        {
            var senderType = typeof(TSender);
            InternalSubscribe(receiver, handler, (sender, _, __) => senderType.IsInstanceOfType(sender));
        }

        public void Subscribe<TSender>(object receiver, MessageHandlerSend<TSender> handler, string title)
        {
            var senderType = typeof(TSender);

            InternalSubscribe(receiver, handler, (sender, messageTitle, _) => 
                messageTitle == title && senderType.IsInstanceOfType(sender));
        }

        public void Subscribe<TParam>(object receiver, MessageHandlerParam<TParam> handler)
        {
            var paramType = typeof(TParam);
            InternalSubscribe(receiver, handler, (_, __, param) => paramType.IsInstanceOfType(param));
        }

        public void Subscribe<TParam>(object receiver, MessageHandlerParam<TParam> handler, string title)
        {
            var paramType = typeof(TParam);

            InternalSubscribe(receiver, handler, (_, messageTitle, param) =>
                messageTitle == title && paramType.IsInstanceOfType(param));
        }

        public void Subscribe<TSender, TParam>(object receiver, MessageHandlerSendParam<TSender, TParam> handler)
        {
            var senderType = typeof(TSender);
            var paramType = typeof(TParam);

            InternalSubscribe(receiver, handler, (sender, _, param) =>
                senderType.IsInstanceOfType(sender) && paramType.IsInstanceOfType(param));
        }

        public void Subscribe<TSender, TParam>(object receiver, MessageHandlerSendParam<TSender, TParam> handler, string title)
        {
            var senderType = typeof(TSender);
            var paramType = typeof(TParam);

            InternalSubscribe(receiver, handler, (sender, messageTitle, param) =>
                messageTitle == title && senderType.IsInstanceOfType(sender) && paramType.IsInstanceOfType(param));
        }

        #endregion

        private void InternalSubscribe(object receiver, Delegate handler, MessageFilter filter)
        {
            if (receiver is null)
                throw new ArgumentNullException(nameof(receiver));

            if (handler is null)
                throw new ArgumentNullException(nameof(handler));

            var subscription = new Subscription(receiver, handler, filter);
            _subscriptions.Add(subscription);
        }

        // TODO: remove unnecessary copying of the list (use concurrent collection) 
        public Task Send(object sender, string title, object param = null)
        {
            if (sender is null)
                throw new ArgumentNullException(nameof(sender));

            var tasks = _subscriptions
                .ToArray()
                .Where(subscription => subscription.Filter(sender, title, param))
                .Select(subscription => subscription.Invoke(sender, title, param));

            return Task.WhenAll(tasks);
        }

        internal sealed class Subscription
        {
            internal WeakReference Subscriber { get; }
            internal MethodInfo Info { get; }
            internal MessageFilter Filter { get; }

            private readonly object _source;
            private readonly bool _isSourceSubscriber;

            internal Subscription(object subscriber, Delegate handler, MessageFilter filter)
            {
                Subscriber = new WeakReference(subscriber, false);
                Info = handler.Method;
                Filter = filter;

                var source = handler.Target;

                if (source != subscriber)
                {
                    _source = source;
                    _isSourceSubscriber = false;
                }
                else _isSourceSubscriber = true;
            }

            internal Task Invoke(object sender, string title, object param)
            {
                if (Info.IsStatic)
                    return InternalInvoke(null, sender, title, param);

                var source = GetSource();

                if (source is null)
                    return Task.CompletedTask;

                return InternalInvoke(source, sender, title, param);
            }

            private object GetSource()
            {
                return _isSourceSubscriber ? Subscriber.Target : _source;
            }

            private Task InternalInvoke(object source, object sender, string title, object param)
            {
                return (Task) Info.Invoke(source, new[] {sender, title, param});
            }
        }
    }
}
