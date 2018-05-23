namespace Mockingbird.Mocks
{
    using System;
    using System.Collections.Generic;
    using NSubstitute;
    using Prism.Events;

    /// <summary>
    /// Wraps standard <see cref="PubSubEvent{TPayload}"/>, stores and invokes subscribed handles when asked
    /// </summary>
    /// <typeparam name="TEventType">Type of pubsub event</typeparam>
    /// <typeparam name="TPayloadType">Type of pubsub event payload</typeparam>
    internal class PubSubEventWrapper<TEventType, TPayloadType> : IEventWrapper 
        where TEventType : PubSubEvent<TPayloadType>
    {
        private readonly List<Action<TPayloadType>> _handlers = new List<Action<TPayloadType>>();
        private readonly object _handlersSyncRoot = new object();

        public EventBase WrappedEvent { get; }

        public PubSubEventWrapper()
        {
            var @event = Substitute.For<TEventType>();

            @event.When(x => x.Subscribe(Arg.Any<Action<TPayloadType>>(), Arg.Any<ThreadOption>(), Arg.Any<bool>())).Do(
                x =>
                {
                    lock (_handlersSyncRoot)
                    {
                        _handlers.Add(x.Arg<Action<TPayloadType>>());
                    }
                });

            @event.When(x => x.Publish(Arg.Any<TPayloadType>())).Do(
                x =>
                {
                    lock (_handlersSyncRoot)
                    {
                        InvokeHandlers(x.Arg<TPayloadType>());
                    }
                });

            @event.When(x => x.Unsubscribe(Arg.Any<Action<TPayloadType>>())).Do(
                x =>
                {
                    lock (_handlersSyncRoot)
                    {
                        _handlers.Remove(x.Arg<Action<TPayloadType>>());
                    }
                });

            WrappedEvent = @event;
        }

        private void InvokeHandlers(TPayloadType payload)
        {
            lock (_handlersSyncRoot)
            {
                foreach (Action<TPayloadType> handler in _handlers)
                {
                    handler?.Invoke(payload);
                }
            }
        }
    }
}
