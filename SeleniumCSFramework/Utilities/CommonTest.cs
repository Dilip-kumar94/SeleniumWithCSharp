using AventStack.ExtentReports;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumCSFramework.PageObject;
using SeleniumCSFramework.Tests;
using System.Configuration;

namespace SeleniumCSFramework.Utilities
{
    public class CommonTest:ReportingUtil
    {
        protected IWebDriver driver;
        protected WebDriverWait wait;
        protected Actions actions;
        protected LoginPageObj login;
        protected ProtoComShopPageObj protoComShop;

        public IWebDriver getDriver()
        {
            return driver;
        }

        [SetUp]
        public void Setup()
        {
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name).Info("Starting test: " + TestContext.CurrentContext.Test.Name);
            extent.AddSystemInfo("Tests:", TestContext.CurrentContext.Test.Name);
            TestContext.Progress.WriteLine("Setting up the test environment.");
            driver = GetDriver(TestContext.Parameters["browserName"] == "" ? (ConfigurationManager.AppSettings["browser"] ?? "default") : TestContext.Parameters["browserName"]!);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            actions = new Actions(driver);
            login = new LoginPageObj(driver);
            protoComShop = new ProtoComShopPageObj(driver);
        }

        public IWebDriver GetDriver(string driverName)
        {
            switch (driverName)
            {
                case "chrome":
                    TestContext.Progress.WriteLine("Using Chrome driver");
                    driver = new ChromeDriver();
                    break;
                case "edge":
                    TestContext.Progress.WriteLine("Using Edge driver");
                    driver = new EdgeDriver();
                    break;
                case "default":
                    TestContext.Progress.WriteLine("Using default driver (Chrome)");
                    driver = new ChromeDriver();
                    break;
                default:
                    TestContext.Progress.WriteLine("Invalid driver specified, using Chrome as default.");
                    driver = new ChromeDriver();
                    break;
            }
            return driver;
        }

        

        [TearDown]
        public void TearDown()
        {
            
            if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                test?.Log(Status.Fail, "Test failed: " + TestContext.CurrentContext.Result.Message,ScreenshotUtil.captureScreenshot(driver,"Screenshot_"+DateTime.Now.ToString("dd_MM_yyyy_t")));
                
            }
            else
            {
                test?.Log(Status.Info, "Test completed: " + TestContext.CurrentContext.Test.Name);
            }
            TestContext.Progress.WriteLine("Teardown to clear the objects");
            driver?.Dispose();
        }
    }
}