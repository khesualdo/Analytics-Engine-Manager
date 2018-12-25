using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;

/// <summary>
/// Sends data to Azure Application Insights.
/// </summary>
public class ApplicationInsightsAnalyticsEngine : ICustomAnalyticsEngine
{
    private TelemetryClient _telemetryClient;

    public string AnalyticsEngineName { get; private set; }

    public ApplicationInsightsAnalyticsEngine(ICustomConfigurationManager customConfigurationManager)
    {
        _telemetryClient = new TelemetryClient();
        TelemetryConfiguration.Active.InstrumentationKey = customConfigurationManager.GetApplicationInsightsInstrumentationKey();
        AnalyticsEngineName = "ApplicationInsights";
    }

    public void SendEvent(IAnalyticsEvent analyticsEvent)
    {
        _telemetryClient.TrackEvent(
            analyticsEvent.EventType,
            SeverityLevel.Warning,
            analyticsEvent.ToDictionary());
        _telemetryClient.Flush();
    }
}

