using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace TestAutomationCSharp
{
     class LoginPage
    {
        private ChromeDriver Driver;
        private WebDriverWait wait;
        /// <summary>
        /// Demonstrated different locators and used implicit and Explicit waits.
        /// Using Simple login page for students and teachers.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            TestContext.Progress.WriteLine("Setting up the test environment.");
            Driver = new ChromeDriver();
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void Login_with_valid_credentials_for_student()
        {
            TestContext.Progress.WriteLine("Executing Test 1");
            Driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";
            Driver.FindElement(By.Id("username")).SendKeys("rahulshettyacademy");
            Driver.FindElement(By.Name("password")).SendKeys("learning");
            List<IWebElement> webElements = Driver.FindElements(By.XPath("//input[@name='radio']")).ToList();
            foreach (IWebElement element in webElements)
            {
                if (element.GetAttribute("value").Equals("user"))
                {
                    element.Click();
                    break;
                }
            }
            try
            {
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("myModal")));
                Driver.FindElement(By.Id("okayBtn")).Click();
            }
            catch (TimeoutException)
            {
                TestContext.Progress.WriteLine("No alert present to accept.");
            }

            Driver.FindElement(By.Id("signInBtn")).Click();
            //WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TitleContains("ProtoCommerce"));
            Assert.That(Driver.Title,Is.EqualTo("ProtoCommerce"));
        }
        [Test]
        public void Login_with_Invalid_credentials_for_student()
        {
            TestContext.Progress.WriteLine("Executing Test 2");
            Driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";
            Driver.FindElement(By.Id("username")).SendKeys("rahulshettyacademy");
            Driver.FindElement(By.Name("password")).SendKeys("learning1");
            Driver.FindElement(By.Id("signInBtn")).Click();
            //WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                .TextToBePresentInElementValue(Driver.FindElement(By.Id("signInBtn")),"Sign In"));
            Assert.That(Driver.FindElement(By.CssSelector(".alert.alert-danger.col-md-12")).Text, Is.EqualTo("Incorrect username/password."));
        }
        [Test]
        public void Login_with_Empty_credentials_for_student()
        {
            TestContext.Progress.WriteLine("Executing Test 3");
            Driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";
            Driver.FindElement(By.Id("username")).SendKeys("");
            Driver.FindElement(By.Name("password")).SendKeys("");
            Driver.FindElement(By.Id("signInBtn")).Click();
            //WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                .TextToBePresentInElementValue(Driver.FindElement(By.Id("signInBtn")), "Sign In"));
            Assert.That(Driver.FindElement(By.CssSelector(".alert.alert-danger.col-md-12")).Text, Is.EqualTo("Empty username/password."));
        }
        [Test]
        public void Login_with_valid_credentials_for_teachers()
        {
            TestContext.Progress.WriteLine("Executing Test 4");
            Driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";
            Driver.FindElement(By.Id("username")).SendKeys("rahulshettyacademy");
            Driver.FindElement(By.Name("password")).SendKeys("learning");
            SelectElement selectElement = new SelectElement(Driver.FindElement(By.XPath("//select[@class='form-control']")));
            selectElement.SelectByText("Teacher");
            Driver.FindElement(By.Id("signInBtn")).Click();
            //WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TitleContains("ProtoCommerce"));
            Assert.That(Driver.Title, Is.EqualTo("ProtoCommerce"));
        }
        [Test]
        public void Login_with_Invalid_credentials_for_teachers()
        {
            TestContext.Progress.WriteLine("Executing Test 5");
            Driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";
            Driver.FindElement(By.Id("username")).SendKeys("rahulshettyacademy");
            Driver.FindElement(By.Name("password")).SendKeys("learning1");
            SelectElement selectElement = new SelectElement(Driver.FindElement(By.XPath("//select[@class='form-control']")));
            selectElement.SelectByText("Teacher");
            Driver.FindElement(By.Id("signInBtn")).Click();
            //WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                .TextToBePresentInElementValue(Driver.FindElement(By.Id("signInBtn")), "Sign In"));
            Assert.That(Driver.FindElement(By.CssSelector(".alert.alert-danger.col-md-12")).Text, Is.EqualTo("Incorrect username/password."));
        }
        [Test]
        public void Login_with_Empty_credentials_for_Teachers()
        {
            TestContext.Progress.WriteLine("Executing Test 6");
            Driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";
            Driver.FindElement(By.Id("username")).SendKeys("");
            Driver.FindElement(By.Name("password")).SendKeys("");
            SelectElement selectElement = new SelectElement(Driver.FindElement(By.XPath("//select[@class='form-control']")));
            selectElement.SelectByText("Teacher");
            Driver.FindElement(By.Id("signInBtn")).Click();
            //WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                .TextToBePresentInElementValue(Driver.FindElement(By.Id("signInBtn")), "Sign In"));
            Assert.That(Driver.FindElement(By.CssSelector(".alert.alert-danger.col-md-12")).Text, Is.EqualTo("Empty username/password."));
        }
        [TearDownAttribute]
        public void TearDownTest()
        {
            TestContext.Progress.WriteLine("Cleaning up after the test.");
            Driver.Dispose();
        }
    }
}
