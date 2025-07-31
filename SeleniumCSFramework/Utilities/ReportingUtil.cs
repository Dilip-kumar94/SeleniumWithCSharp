using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumCSFramework.Utilities
{
    public class ReportingUtil
    {
        protected ExtentReports extent;
        protected ExtentTest test;

        [OneTimeSetUp]
        public void SetupReporting()
        {
            
            var htmlReporter = new ExtentHtmlReporter(Environment.CurrentDirectory+"\\index.html");
            
            TestContext.Progress.WriteLine(htmlReporter.ToString());
            htmlReporter.Config.ReportName = "Selenium CS Framework Test Report";
            extent = new();
            extent.AttachReporter(htmlReporter);
            extent.AddSystemInfo("OS", Environment.OSVersion.ToString());
            extent.AddSystemInfo("Browser", (ConfigurationManager.AppSettings["browser"] ?? "chrome"));
            extent.AddSystemInfo("Environment", "Test");
            extent.AddSystemInfo("Test Framework", "NUnit");
            extent.AddSystemInfo("Test Execution Date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            extent.AddSystemInfo("Test Execution Timezone", TimeZoneInfo.Local.StandardName);
            extent.AddSystemInfo("Test Execution User", Environment.UserName);
        }

        [OneTimeTearDown]
        public void TearDownReporting()
        {
            extent?.Flush();
        }
    }
}
