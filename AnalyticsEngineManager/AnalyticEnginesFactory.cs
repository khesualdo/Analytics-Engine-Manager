using System.Collections.Generic;

public interface IAnalyticsEnginesFactory
{
    /// <summary>
    /// Creates a list of all Analytics Engines.
    /// </summary>
    /// <param name="analyticsConfigurationManager">Configuration Manager</param>
    /// <returns>List of all Analytics Engines</returns>
    List<IAnalyticsEngine> CreateAnalyticsEngines(ICustomConfigurationManager analyticsConfigurationManager);
}

public class AnalyticsEnginesFactory : IAnalyticsEnginesFactory
{
    public List<IAnalyticsEngine> CreateAnalyticsEngines(ICustomConfigurationManager analyticsConfigurationManager)
    {
        List<IAnalyticsEngine> analyticsEngines = new List<IAnalyticsEngine>();
        analyticsEngines.Add(new ApplicationInsightsAnalyticsEngine(analyticsConfigurationManager));
        return analyticsEngines;
    }
}

