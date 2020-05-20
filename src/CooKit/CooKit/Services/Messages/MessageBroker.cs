using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CooKit.Delegates;
using CooKit.Extensions;
using CooKit.Models.Messages;

namespace CooKit.Services.Messages
{
    public sealed class MessageBroker : IMessageBroker
    {
        private readonly List<HandlerEntry> _handlerEntries;

        public MessageBroker()
        {
            _handlerEntries = new List<HandlerEntry>();
        }

        #region Send Methods

        public Task Send(IMessage message)
        {
            if (message is null)
                throw new ArgumentNullException(nameof(message));

            // pre-enumeration using ToArray() was used to avoid deleting entries
            // while iterating over the list (caused by deferred execution of Select())
            var validEntries = _handlerEntries
                .Where(entry => entry.Checker(message))
                .ToArray();

            var tasks = new List<Task>(validEntries.Length);

            foreach (var entry in validEntries)
            {
                var handler = entry.Handler;

                if (handler is null)
                {
                    _handlerEntries.Remove(entry);
                    continue;
                }

                var task = handler(message);
                tasks.Add(task);
            }

            return Task.WhenAll(tasks);
        }

        public Task Send(object sender, string title, object argument = null)
        {
            return Send(new Message(sender, title, argument));
        }

        #endregion

        #region Attach Methods

        public void Attach(MessageHandler handler)
        {
            InternalAttach(handler, _ => true);
        }

        public void Attach(MessageHandler handler, string title)
        {
            InternalAttach(handler, message => message.Title == title);
        }

        public void Attach<TSender>(MessageHandler handler)
        {
            Attach(handler, typeof(TSender));
        }

        public void Attach(MessageHandler handler, Type senderType)
        {
            bool Checker(IMessage message)
            {
                var type = message.Sender.GetType();
                return type.IsAssignableFrom(senderType);
            }

            InternalAttach(handler, Checker);
        }

        private void InternalAttach(MessageHandler handler, ShouldSendChecker checker)
        {
            if (handler is null)
                throw new ArgumentNullException(nameof(handler));

            var entry = new HandlerEntry(handler, checker);
            _handlerEntries.Add(entry);
        }

        #endregion

        public void Detach(MessageHandler handler)
        {
            if (handler is null)
                throw new ArgumentNullException(nameof(handler));

            _handlerEntries.RemoveAll(entry =>
            {
                var checkingHandler = entry.Handler;

                if (checkingHandler is null)
                    return true;

                return checkingHandler == handler;
            });
        }

        #region Helper Class / Delegate

        internal delegate bool ShouldSendChecker(IMessage message);

        internal sealed class HandlerEntry
        {
            internal MessageHandler Handler => _weakHandler.GetValueOrNull();
            internal ShouldSendChecker Checker { get; }

            private readonly WeakReference<MessageHandler> _weakHandler;

            public HandlerEntry(MessageHandler handler, ShouldSendChecker checker)
            {
                _weakHandler = new WeakReference<MessageHandler>(handler);
                Checker = checker;
            }
        }

        #endregion
    }
}
