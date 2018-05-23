namespace Mockingbird.Mocks
{
    using System;
    using System.Collections.Concurrent;
    using NSubstitute.Exceptions;
    using Prism.Events;

    /// <summary>
    /// Mocks event aggregator. Clients of this class can safely subscribe and publish <see cref="IEventAggregator"/> eventsm
    /// no matter where and with which parameters they were created (i.e with <see cref="ThreadOption.UIThread"/>).
    /// </summary>
    /// <example>
    /// <code>
    /// EventAggregatorMock eventAggregatorMock = new EventAggregatorMock();
    /// eventAggregatorMock.AddPubSubEvent{TelemetryUpdateEvent, TelemetryUpdateEventArgs}();
    /// IEventAggregator eventAggregator = eventAggregatorMock;
    /// </code>
    /// </example>
    public class EventAggregatorMock : IEventAggregator
    {
        private readonly ConcurrentDictionary<Type, IEventWrapper> _eventWrappers = new ConcurrentDictionary<Type, IEventWrapper>();

        /// <summary>
        /// Returns event of approptiate type
        /// </summary>
        /// <typeparam name="TEventType">Type of event</typeparam>
        /// <returns></returns>
        public TEventType GetEvent<TEventType>() where TEventType : EventBase, new()
        {
            if (_eventWrappers.TryGetValue(typeof(TEventType), out IEventWrapper eventWrapper) && eventWrapper.WrappedEvent is TEventType unwrappedEvent)
            {
                return unwrappedEvent;
            }

            // throw exception, this will fail executing test with meaningfull exception text
            throw new AmbiguousArgumentsException($"EventAggregatorMock functionality is not implemented for events of type {typeof(TEventType).FullName}");
        }

        /// <summary>
        /// Nofities EventAggregatorMock about presence of {TEventType} event with {TPayloadType} payload
        /// </summary>
        /// <typeparam name="TEventType">Type of event</typeparam>
        /// <typeparam name="TPayloadType">Type of event payload</typeparam>
        public void AddPubSubEvent<TEventType, TPayloadType>() where TEventType : PubSubEvent<TPayloadType>
        {
            _eventWrappers.TryAdd(typeof(TEventType), new PubSubEventWrapper<TEventType, TPayloadType>());
        }
    }
}
