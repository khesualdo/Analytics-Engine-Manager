using System;
using System.Collections.Generic;

public interface IAnalyticsEvent
{
    /// <summary>
    /// Converts object of type IAnalyticsEvent to dictionary of type IDictionary, where the key and value are both of type string.
    /// </summary>
    /// <returns>IDictionary, where the key and value are both of type string</returns>
    IDictionary<string, string> ToDictionary();

    /// <summary>
    /// ID of the event.
    /// </summary>
    Guid EventID { get; set; }

    /// <summary>
    /// Type of the event.
    /// </summary>
    string EventType { get; set; }

    /// <summary>
    /// User ID of the Global Admin user.
    /// </summary>
    string UserID { get; set; }

    /// <summary>
    /// IP address of the Global Admin client.
    /// </summary>
    string SourceIPAddress { get; set; }

    /// <summary>
    /// Date and time (in UTC format) of the event.
    /// </summary>
    DateTime DataTimeStamp { get; set; }
}

/// <summary>
/// General data type for recording base information about events, to be passed to external Analytics Engines.
/// </summary>
public class AnalyticsEvent : IAnalyticsEvent
{
    public Guid EventID { get; set; }
    public string UserID { get; set; }
    public string SourceIPAddress { get; set; }
    public DateTime DataTimeStamp { get; set; }
    public string EventType { get; set; }

    public AnalyticsEvent(string eventType, string userID, string sourceIPAddress)
    {
        EventType = string.IsNullOrEmpty(eventType) ? "NullOrEmptyEventType" : eventType;
        UserID = string.IsNullOrEmpty(userID) ? "NullOrEmptyUserID" : userID;
        SourceIPAddress = string.IsNullOrEmpty(sourceIPAddress) ? "NullOrEmptySourceIPAddress" : sourceIPAddress;
        EventID = Guid.NewGuid();
        DataTimeStamp = DateTime.UtcNow;
    }

    public IDictionary<string, string> ToDictionary()
    {
        return new Dictionary<string, string>()
        {
            {nameof(EventID), EventID.ToString()},
            {nameof(EventType), EventType},
            {nameof(UserID), UserID},
            {nameof(SourceIPAddress), SourceIPAddress},
            {nameof(DataTimeStamp), DataTimeStamp.ToString()}
        };
    }
}