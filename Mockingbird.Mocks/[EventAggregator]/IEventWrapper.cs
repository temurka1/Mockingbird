namespace Mockingbird.Mocks
{
    using Prism.Events;

    /// <summary>
    /// This thingy wraps <see cref="EventBase"/> to provide flexible way to store events + handlers
    /// </summary>
    internal interface IEventWrapper
    {
        /// <summary>
        /// Wrapped event
        /// </summary>
        EventBase WrappedEvent { get; }
    }
}
