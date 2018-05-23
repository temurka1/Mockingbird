namespace Mockingbird.BusinessLogic.Telemetry
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    using Common;

    public class TelemetryReceiver: ITelemetryReceiver
    {
        #region Constants

        private const int TELEMETRY_NOTIFICATION_RATE_MS = 1000;

        private const string MESSAGES_SENDER = "USS Challenger";
        private const string MESSAGES_RECEIVER = "John F. Kennedy Space Center";

        private const string BOOSTER_FAILURE = "Booster failure detected";
        private const string LAUNCH_SEQUENCE_STARTED = "T-20 minutes and counting";
        private const string LIFTOFF = "T-0 seconds. SRB ignition.";
        private const string TELEMETRY_STATUS = "Altitude:30000 km, Speed: 10 km/s";

        #endregion

        #region Fields

        private readonly Timer _notificationReceivedTimer;
        private readonly Random _random;

        private readonly Dictionary<TelemetryEventType, string> _perNotificationEventTypeMessages;

        #endregion

        #region Events

        public event EventHandler<ReceivedTelemetryEventArgs> NotificationReceived;

        #endregion

        #region Constructors

        public TelemetryReceiver()
        {
            _perNotificationEventTypeMessages = new Dictionary<TelemetryEventType, string>()
            {
                {TelemetryEventType.CriticalSystemFailure, BOOSTER_FAILURE},
                {TelemetryEventType.LaunchSequenceStarted, LAUNCH_SEQUENCE_STARTED},
                {TelemetryEventType.Liftoff, LIFTOFF},
                {TelemetryEventType.StatusMessage, TELEMETRY_STATUS}
            };

            _random = new Random();
            _notificationReceivedTimer = new Timer(HandleOnNotificationReceivedTimerTick, null, 0, TELEMETRY_NOTIFICATION_RATE_MS);
        }

        private void HandleOnNotificationReceivedTimerTick(object state)
        {
            // there should be actual business logic for receiving data from NASA servers,
            // but that is just testing example application, so just generate messages randomly

            TelemetryEventType argsType = (TelemetryEventType)(_random.Next() % 4);

            var args = new ReceivedTelemetryEventArgs(
                argsType,
                _perNotificationEventTypeMessages[argsType],
                MESSAGES_SENDER,
                MESSAGES_RECEIVER, 
                DateTime.Now);

            NotificationReceived?.Invoke(this, args);
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        public void Dispose()
        {
            _notificationReceivedTimer.Dispose();
        }

        #endregion
    }
}
