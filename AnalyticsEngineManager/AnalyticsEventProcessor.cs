public interface IAnalyticsEventProcessor
{
    /// <summary>
    /// Used to log event to all services registered with the Analytics Engine
    /// </summary>
    /// <param name="analyticsEvent">Event that needs to be logged</param>
    void LogEvent(IAnalyticsEvent analyticsEvent);
}

/// <summary>
/// Propogates events to Analytics Engines.
/// </summary>
public class AnalyticsEventProcessor : IAnalyticsEventProcessor
{
    private ICustomConfigurationManager _customConfigurationManager;
    private IAnalyticsEnginesFactory _analyticsEnginesFactory;

    public AnalyticsEventProcessor(ICustomConfigurationManager analyticsConfigurationManager, IAnalyticsEnginesFactory analyticsEnginesFactory)
    {
        _customConfigurationManager = analyticsConfigurationManager;
        _analyticsEnginesFactory = analyticsEnginesFactory;
    }

    public void LogEvent(IAnalyticsEvent analyticsEvent)
    {
        foreach (ICustomAnalyticsEngine customAnalyticsEngine in _analyticsEnginesFactory.CreateAnalyticsEngines(_customConfigurationManager))
        {
            string analyticsEngineName = customAnalyticsEngine.AnalyticsEngineName;
            bool flag = _customConfigurationManager.GetFlag(analyticsEngineName);

            if (flag)
            {
                customAnalyticsEngine.SendEvent(analyticsEvent);
            }
        }
    }
}
