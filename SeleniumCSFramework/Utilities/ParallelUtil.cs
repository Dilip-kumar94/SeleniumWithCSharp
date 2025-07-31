using AventStack.ExtentReports;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumCSFramework.PageObject;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;

namespace SeleniumCSFramework.Utilities
{
    public class ParallelUtil:ReportingUtil
    {
        public static ThreadLocal<IWebDriver> driver = new ThreadLocal<IWebDriver>()!;
        
        [SetUp]
        public void Setup()
        {
            test = extent!.CreateTest(TestContext.CurrentContext.Test.Name);
            TestContext.Progress.WriteLine("Setting up the test environment.");
            driver.Value = GetDriver(ConfigurationManager.AppSettings["browser"] ?? "default");
            driver.Value.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Value.Manage().Window.Maximize();
            driver.Value.Navigate().GoToUrl(ConfigurationManager.AppSettings["baseUrl"] ?? "null");
        }

        public IWebDriver? getDriver()
        {
            return driver.Value;
        }
        private IWebDriver GetDriver(string vdriver)
        {
            switch (vdriver)
            {
                case "chrome":
                    TestContext.Progress.WriteLine("Using Chrome driver");
                    return new ChromeDriver();
                    
                case "edge":
                    TestContext.Progress.WriteLine("Using Edge driver");
                    return new EdgeDriver();
                    
                case "default":
                    TestContext.Progress.WriteLine("Using default driver (Chrome)");
                    return new ChromeDriver();
                    
                default:
                    TestContext.Progress.WriteLine("Invalid driver specified, using Chrome as default.");
                    return  new ChromeDriver();
                
            }
            

        }
        [TearDown]
        public void Teardown()
        {
            var testStatus = TestContext.CurrentContext.Result.Outcome.Status;
            if (testStatus == TestStatus.Failed)
            {
                test.Fail("Test failed: " + TestContext.CurrentContext.Result.Message,ScreenshotUtil.captureScreenshot(driver.Value!,TestContext.CurrentContext.Test.Name));
                test.Log(Status.Fail,TestContext.CurrentContext.Result.StackTrace);
            }
            else
            {
                test.Pass("Test passed.");
            }
            if (driver?.IsValueCreated == true)
            {
                driver?.Value?.Quit();
                driver?.Value?.Dispose();
            }

        }
        
    }
}
