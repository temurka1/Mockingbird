namespace Mockingbird.Common
{
    /// <summary>
    /// Type of telemetry message
    /// </summary>
    public enum TelemetryEventType
    {
        /// <summary>
        /// Space shuttle critical failure. Its probably too late to say "Houston, we have a problem"
        /// </summary>
        CriticalSystemFailure,

        /// <summary>
        /// Simple telemetry status message - some data about speed, altitude, etc
        /// </summary>
        StatusMessage,

        /// <summary>
        /// Signals that launch sequence was initiated
        /// </summary>
        LaunchSequenceStarted,

        /// <summary>
        /// T-0, Liftoff
        /// </summary>
        Liftoff,
    }
}
