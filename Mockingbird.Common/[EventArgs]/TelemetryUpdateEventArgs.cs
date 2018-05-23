namespace Mockingbird.Common
{
    public class TelemetryUpdateEventArgs
    {
        public TelemetryEventType TelemetryType { get; }
        public string NotificationMessage { get; }
        public string Sender { get; }

        public TelemetryUpdateEventArgs(TelemetryEventType telemetryType, string notificationMessage, string sender)
        {
            TelemetryType = telemetryType;
            NotificationMessage = notificationMessage;
            Sender = sender;
        }
    }
}
