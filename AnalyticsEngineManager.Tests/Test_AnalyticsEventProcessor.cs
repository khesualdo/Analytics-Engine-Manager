using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

[TestClass]
public class Test_AnalyticsEventProcessor
{
    private IAnalyticsEventProcessor _customEventProcessor;
    private IAnalyticsEvent _dummyAnalyticsEvent;
    private Mock<IAnalyticsEnginesFactory> _mockAnalyticsEnginesFactory;
    private Mock<ICustomConfigurationManager> _mockCustomConfigurationManager;
    private Mock<IAnalyticsEngine> _mockApplicationInsightsAnalyticsEngine;

    [TestInitialize]
    public void Initilize()
    {
        _dummyAnalyticsEvent = It.IsAny<IAnalyticsEvent>();

        _mockCustomConfigurationManager = new Mock<ICustomConfigurationManager>();
        _mockAnalyticsEnginesFactory = new Mock<IAnalyticsEnginesFactory>();

        List<IAnalyticsEngine> analyticsEngines = new List<IAnalyticsEngine>();
        _mockApplicationInsightsAnalyticsEngine = new Mock<IAnalyticsEngine>();
        analyticsEngines.Add(_mockApplicationInsightsAnalyticsEngine.Object);

        _mockAnalyticsEnginesFactory.Setup(e => e.CreateAnalyticsEngines(It.IsAny<ICustomConfigurationManager>())).Returns(analyticsEngines);
    }

    /// <summary>
    /// Mock the Configuration Manager to return False.
    /// Verify that the if-statement is never entered and SendEvent is never called.
    /// </summary>
    [TestMethod]
    public void AnalyticsEventProcessor_FlagIsFalse_SendEvent_DoesNotGetCalled()
    {
        _mockCustomConfigurationManager.Setup(e => e.GetFlag(It.IsAny<string>())).Returns(false);
        _customEventProcessor = new AnalyticsEventProcessor(_mockCustomConfigurationManager.Object, _mockAnalyticsEnginesFactory.Object);

        _customEventProcessor.LogEvent(_dummyAnalyticsEvent);

        _mockApplicationInsightsAnalyticsEngine.Verify(m => m.SendEvent(_dummyAnalyticsEvent), Times.Never());
    }

    /// <summary>
    /// Mock the Configuration Manager to return True.
    /// Verify that the if-statement is entered and SendEvent is called exactly once.
    /// </summary>
    [TestMethod]
    public void AnalyticsEventProcessor_FlagIsTrue_SendEvent_IsCalledOnlyOnce()
    {
        _mockCustomConfigurationManager.Setup(e => e.GetFlag(It.IsAny<string>())).Returns(true);
        _customEventProcessor = new AnalyticsEventProcessor(_mockCustomConfigurationManager.Object, _mockAnalyticsEnginesFactory.Object);

        _customEventProcessor.LogEvent(_dummyAnalyticsEvent);

        _mockApplicationInsightsAnalyticsEngine.Verify(m => m.SendEvent(_dummyAnalyticsEvent), Times.Exactly(1));
    }
}
