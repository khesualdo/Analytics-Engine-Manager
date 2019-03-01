public interface IAnalyticsEventProcessor
{
    /// <summary>
    /// Used to log event to all services registered with the Analytics Engine
    /// </summary>
    /// <param name="analyticsEvent">Event that needs to be logged</param>
    void LogEvent(IAnalyticsEvent analyticsEvent);

    /// <summary>
    /// Used to asynchronously log event to all services registered with the Analytics Engine
    /// </summary>
    /// <param name="analyticsEvent">Event that needs to be logged</param>
    Task LogEventAsync(IAnalyticsEvent analyticsEvent);

    /// <summary>
    /// Used to log event to all services registered with the Analytics Engine in parallel
    /// </summary>
    /// <param name="analyticsEvent">Event that needs to be logged</param>
    Task LogEventParallelAsync(IAnalyticsEvent analyticsEvent);
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
        foreach (IAnalyticsEngine customAnalyticsEngine in _analyticsEnginesFactory.CreateAnalyticsEngines(_customConfigurationManager))
        {
            string analyticsEngineName = customAnalyticsEngine.AnalyticsEngineName;
            bool flag = _customConfigurationManager.GetFlag(analyticsEngineName);

            if (flag)
            {
                customAnalyticsEngine.SendEvent(analyticsEvent);
            }
        }
    }

    public async Task LogEventAsync(IAnalyticsEvent analyticsEvent)
    {
        await Task.Run(() => this.LogEvent(analyticsEvent));
    }

    public async Task LogEventParallelAsync(IAnalyticsEvent analyticsEvent)
    {
        List<Task> tasks = new List<Task>();
        foreach (IAnalyticsEngine customAnalyticsEngine in _analyticEnginesFactory.CreateAnalyticsEngines(_customConfigurationManager))
        {
            string analyticsEngineName = customAnalyticsEngine.GetAnalyticsEngineName;
            bool flag = _customConfigurationManager.GetFlag(analyticsEngineName);
            if (flag)
            {
                Task task = customAnalyticsEngine.SendEventAsync(analyticsEvent);
                tasks.Add(task);
            }
        }
        await Task.WhenAll(tasks);
    }
}
