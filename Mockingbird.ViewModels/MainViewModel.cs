namespace Mockingbird.ViewModels
{
    using System;

    using Prism.Events;
    using Prism.Mvvm;
    using Prism.Commands;
    using Common;
    using BusinessLogic;
    using ActivityLog;

    public class MainViewModel: BindableBase, IDisposable
    {
        #region Fields

        private readonly IEventAggregator _eventAggregator;
        private readonly ITelemetryReceiver telemetryReceiver;

        private bool _isReceivingStarted;

        #endregion

        #region Properties

        public ActivityLogViewModel ActivityLogViewModel { get; }

        public bool IsReceivingStarted
        {
            get => _isReceivingStarted;
            private set => SetProperty(ref _isReceivingStarted, value);
        }

        public DelegateCommand StartReceiveNotificationsCommand { get; }
        public DelegateCommand StopReceiveNotificationsCommand { get; }

        #endregion

        #region Constructors

        public MainViewModel(IEventAggregator eventAggregator, ITelemetryReceiver telemetryReceiver)
        {
            _eventAggregator = eventAggregator ?? throw new NullReferenceException(nameof(eventAggregator));
            this.telemetryReceiver = telemetryReceiver ?? throw new NullReferenceException(nameof(telemetryReceiver));

            ActivityLogViewModel = new ActivityLogViewModel(eventAggregator);

            StartReceiveNotificationsCommand = new DelegateCommand(ExecuteStartReceiveNotificationsCommand);
            StopReceiveNotificationsCommand = new DelegateCommand(ExecuteStopReceiveNotificationsCommand);
        }

        #endregion

        #region Methods

        public void Dispose()
        {
            telemetryReceiver.NotificationReceived -= HandleTelemetryReceived;
            ActivityLogViewModel.Dispose();
        }

        private void ExecuteStartReceiveNotificationsCommand()
        {
            IsReceivingStarted = true;
            telemetryReceiver.NotificationReceived += HandleTelemetryReceived;
        }

        private void ExecuteStopReceiveNotificationsCommand()
        {
            IsReceivingStarted = false;
            telemetryReceiver.NotificationReceived -= HandleTelemetryReceived;
        }

        private void HandleTelemetryReceived(object sender, ReceivedTelemetryEventArgs telemetry)
        {
            // just resend telemetry events to other subscribers
            // this is not GOOD way to do such feature, but it's quite match example's needs
            //
            _eventAggregator.GetEvent<TelemetryUpdateEvent>().Publish(new TelemetryUpdateEventArgs(telemetry.TelemetryType, telemetry.Message, telemetry.Sender));
        }

        #endregion
    }
}
