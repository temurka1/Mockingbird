namespace Mockingbird.ViewModels.ActivityLog
{
    using Common;
    using Prism.Mvvm;

    public class ActivityViewModel: BindableBase
    {
        #region Fields

        private TelemetryEventType telemetryType;
        private string _notificationMessage;
        private string _sender;

        #endregion

        #region Properties

        public TelemetryEventType EventType
        {
            get => telemetryType;
            set => SetProperty(ref telemetryType, value);
        }

        public string Message
        {
            get => _notificationMessage;
            set => SetProperty(ref _notificationMessage, value);
        }

        public string Sender
        {
            get => _sender;
            set => SetProperty(ref _sender, value);
        }

        #endregion

        #region Constructors

        public ActivityViewModel(TelemetryEventType eventType, string message, string sender)
        {
            EventType = eventType;
            Message = message;
            Sender = sender;
        }

        #endregion
    }
}
