namespace Mockingbird.Common
{
    using System;

    public class ReceivedTelemetryEventArgs: EventArgs
    {
        #region Properties

        /// <summary>
        /// Type of telemetry message
        /// </summary>
        public TelemetryEventType TelemetryType { get; }

        /// <summary>
        /// Actual message
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Message sender
        /// </summary>
        public string Sender { get; }

        /// <summary>
        /// Message receiver
        /// </summary>
        public string Receiver { get; }

        /// <summary>
        /// Message spawn timestamp
        /// </summary>
        public DateTime Timestamp { get; }

        #endregion

        #region Constructors

        public ReceivedTelemetryEventArgs(
            TelemetryEventType telemetryType, 
            string message, 
            string sender, 
            string receiver, 
            DateTime timestamp)
        {
            TelemetryType = telemetryType;
            Message = message;
            Sender = sender;
            Receiver = receiver;
            Timestamp = timestamp;
        }

        #endregion
    }
}
