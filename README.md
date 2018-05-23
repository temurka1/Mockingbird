# Mockingbird
This application shows a way how to mock Prism's IEventAggregator and use it in unit tests.

Mock is contained in Mockingbird.Mocks project and can be used as standalone library.

Usage example:
```csharp
EventAggregatorMock eventAggregatorMock = new EventAggregatorMock();

// clients of mock should register events they are interested in
eventAggregatorMock.AddPubSubEvent<TelemetryUpdateEvent, TelemetryUpdateEventArgs>();

IEventAggregator eventAggregator = eventAggregatorMock;
```

The rest of application used just to provide some meaningfull environment to show usage of EventAggregatorMock in production-like conditions.

However it can be used as a good starting point for writing NASA space shuttle's telemetry grabber (no, that would probably cause disaster in interstellar communications).
