namespace Mockingbird.Common
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class NotificationEventToStringConverter: IValueConverter
    {
        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TelemetryEventType notificationEventType)
            {
                switch (notificationEventType)
                {
                    case TelemetryEventType.CriticalSystemFailure:
                        return $"HSTN WE GT PRBLM";

                    case TelemetryEventType.StatusMessage:
                        return "Telemetry";

                    case TelemetryEventType.LaunchSequenceStarted:
                        return "Launch sequence started";

                    case TelemetryEventType.Liftoff:
                        return "Godspeed";

                    default:
                        throw new ArgumentOutOfRangeException(nameof(notificationEventType));
                }
            }

            throw new ArgumentException(nameof(value));
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException($"ConvertBack is not supported for {GetType().FullName}");
        }
    }
}
