/// <summary>
/// Provides common behaviour for all Analytics Engines.
/// </summary>
public interface IAnalyticsEngine
{
    /// <summary>
    /// Gets the name of the Analytics Engine.
    /// </summary>
    string AnalyticsEngineName { get; }

    /// <summary>
    /// Sends and event to an external Analytics Engines.
    /// </summary>
    /// <param name="analyticsEvent">Event to be sent.</param>
    void SendEvent(IAnalyticsEvent analyticsEvent);

    /// <summary>
    /// Asynchronously sends and event to an external Analytics Engines.
    /// </summary>
    /// <param name="analyticsEvent">Event to be sent.</param>
    Task SendEventAsync(IAnalyticsEvent analyticsEvent);
}