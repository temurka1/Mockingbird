namespace Mockingbird.Tests
{
    using System;
    using System.Linq;

    using NUnit.Framework;
    using Prism.Events;

    using NSubstitute;

    using ViewModels;
    using ViewModels.ActivityLog;
    using Mocks;
    using Common;
    using BusinessLogic;

    [TestFixture]
    public class MainViewModelTests
    {
        #region Constants

        private const TelemetryEventType FAILURE_MESSAGE_TYPE = TelemetryEventType.CriticalSystemFailure;

        private const int RECEIVE_MESSAGES_COUNT = 1;

        private const string FAILURE_MESSAGE = "Booster failure";
        private const string FAILURE_MESSAGE_SENDER = "USS Challenger";
        private const string FAILURE_MESSAGE_RECEIVER = "John F. Kennedy Space Center";

        #endregion

        [Test]
        public void ReceiveTelemetryMessage_ExpectTelemetryMessageStored()
        {
            EventAggregatorMock eventAggregatorMock = new EventAggregatorMock();
            eventAggregatorMock.AddPubSubEvent<TelemetryUpdateEvent, TelemetryUpdateEventArgs>();

            IEventAggregator eventAggregator = eventAggregatorMock;
            ITelemetryReceiver telemetryReceiver = Substitute.For<ITelemetryReceiver>();

            MainViewModel mainViewModel = new MainViewModel(eventAggregator, telemetryReceiver);
            ActivityLogViewModel activityLogViewModel = mainViewModel.ActivityLogViewModel;

            ReceivedTelemetryEventArgs eventArgs = new ReceivedTelemetryEventArgs(
                telemetryType: FAILURE_MESSAGE_TYPE, 
                message: FAILURE_MESSAGE, 
                sender: FAILURE_MESSAGE_SENDER, 
                receiver: FAILURE_MESSAGE_RECEIVER, 
                timestamp: DateTime.Now);

            mainViewModel.StartReceiveNotificationsCommand.Execute();

            telemetryReceiver.NotificationReceived += Raise.EventWith(this, eventArgs);

            Assert.AreEqual(RECEIVE_MESSAGES_COUNT, activityLogViewModel.Activities.Count);
            Assert.AreEqual(FAILURE_MESSAGE_TYPE, activityLogViewModel.Activities.First().EventType);
            Assert.AreEqual(FAILURE_MESSAGE_SENDER, activityLogViewModel.Activities.First().Sender);
            Assert.AreEqual(FAILURE_MESSAGE, activityLogViewModel.Activities.First().Message);
        }
    }
}
