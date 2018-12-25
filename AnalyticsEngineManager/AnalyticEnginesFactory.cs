using System.Collections.Generic;

public interface IAnalyticsEnginesFactory
{
    /// <summary>
    /// Creates a list of all Analytics Engines.
    /// </summary>
    /// <param name="analyticsConfigurationManager">Configuration Manager</param>
    /// <returns>List of all Analytics Engines</returns>
    List<ICustomAnalyticsEngine> CreateAnalyticsEngines(ICustomConfigurationManager analyticsConfigurationManager);
}

public class AnalyticsEnginesFactory : IAnalyticsEnginesFactory
{
    public List<ICustomAnalyticsEngine> CreateAnalyticsEngines(ICustomConfigurationManager analyticsConfigurationManager)
    {
        List<ICustomAnalyticsEngine> customAnalyticsEngines = new List<ICustomAnalyticsEngine>();
        customAnalyticsEngines.Add(new ApplicationInsightsAnalyticsEngine(analyticsConfigurationManager));
        return customAnalyticsEngines;
    }
}

