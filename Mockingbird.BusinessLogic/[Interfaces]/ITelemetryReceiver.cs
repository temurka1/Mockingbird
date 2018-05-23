namespace Mockingbird.BusinessLogic
{
    using System;
    using Common;

    /// <summary>
    /// Receives messages from NASA telemetry servers and passes it to subscribers
    /// </summary>
    public interface ITelemetryReceiver: IDisposable
    {
        /// <summary>
        /// Subscribers will receive telemetry from NASA (for real, I promise)
        /// </summary>
        event EventHandler<ReceivedTelemetryEventArgs> NotificationReceived;
    }
}
