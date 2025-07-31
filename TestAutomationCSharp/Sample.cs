using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TestAutomationCSharp
{
    public class SampleTests
    {
        //Defining the Driver variable to be used across tests  
        private ChromeDriver driver;
        [SetUp]
        public void Setup()
        {
            TestContext.Progress.WriteLine("Setting up the test environment.");
            driver = new ChromeDriver();
        }

        [Test]
        public void Test1()
        {
            TestContext.Progress.WriteLine("Executing Test 1");
            driver.Url = "https://www.ebay.com/";
            Assert.Pass();
        }
        [Test]
        public void Test2()
        {
            TestContext.Progress.WriteLine("Executing Test 2");
            driver.Url = "https://www.ebay.com/";
            Assert.Pass();
            string pageTiltle = driver.Title;
            if (pageTiltle.Contains("ebay")) {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }      
        }
        [TearDown]
        public void TearDown()
        {
            TestContext.Progress.WriteLine("Cleaning up after the test.");
            driver?.Dispose();
        }
    }
}