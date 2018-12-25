using System;
using System.Configuration;

public interface ICustomConfigurationManager
{
    /// <summary>
    /// Returns True if flag is explicitly set to true.
    /// False otherwise.
    /// </summary>
    /// <param name="analyticsEngineName">Name of the Analytics Engine</param>
    /// <returns>
    /// True if flag is explicitly set to true, False otherwise.
    /// </returns>
    bool GetFlag(string analyticsEngineName);

    /// <summary>
    /// Returns the Application Insights Instrumentation Key.
    /// </summary>
    /// <returns>Application Insights Instrumentation Key</returns>
    string GetApplicationInsightsInstrumentationKey();
}

public class CustomConfigurationManager : ICustomConfigurationManager
{
    public bool GetFlag(string analyticsEngineName)
    {
        try
        {
            return Convert.ToBoolean(ConfigurationManager.AppSettings[analyticsEngineName]);
        }
        catch (ConfigurationErrorsException)
        {
            // analyticsEngineName could not be retrieved
            return false;
        }
    }

    public string GetApplicationInsightsInstrumentationKey()
    {
        try
        {
            return ConfigurationManager.AppSettings["ApplicationInsightsInstrumentationKey"].ToString();
        }
        catch (ConfigurationErrorsException)
        {
            // ApplicationInsightsInstrumentationKey could not be retrieved
            return "";
        }
    }
}
