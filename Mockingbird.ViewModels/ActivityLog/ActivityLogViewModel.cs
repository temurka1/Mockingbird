namespace Mockingbird.ViewModels.ActivityLog
{
    using System;
    using System.Collections.ObjectModel;
    using Common;
    using Prism.Events;
    using Prism.Mvvm;

    public class ActivityLogViewModel: BindableBase, IDisposable
    {
        #region Fields

        private readonly IEventAggregator _eventAggregator;

        #endregion

        #region Properties

        public ObservableCollection<ActivityViewModel> Activities { get; } = new ObservableCollection<ActivityViewModel>();

        #endregion

        #region Constructors

        public ActivityLogViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator ?? throw new NullReferenceException(nameof(eventAggregator));
            _eventAggregator.GetEvent<TelemetryUpdateEvent>().Subscribe(HandleNotificationEvent, ThreadOption.UIThread, true);
        }

        #endregion

        #region Methods

        public void Dispose()
        {
            _eventAggregator.GetEvent<TelemetryUpdateEvent>().Unsubscribe(HandleNotificationEvent);
        }

        private void HandleNotificationEvent(TelemetryUpdateEventArgs args)
        {
            Activities.Add(new ActivityViewModel(args.TelemetryType, args.NotificationMessage, args.Sender));
        }

        #endregion
    }
}
