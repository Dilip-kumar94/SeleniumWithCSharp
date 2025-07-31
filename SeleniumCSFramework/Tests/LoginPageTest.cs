using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumCSFramework.PageObject;
using SeleniumCSFramework.Utilities;
using System.Configuration;

namespace SeleniumCSFramework.Tests
{    
    public class LoginPageTest:CommonTest
    {
        [Test, Category("Smoke")]
        public void Login_with_valid_credentials_for_student()
        {
            TestContext.Progress.WriteLine("Executing Test 1");
            driver.Url = ConfigurationManager.AppSettings["baseUrl"]??"NULL";
            login.setUserName("rahulshettyacademy");
            login.setPassword("learning1");
            login.setRadioButton("user");
            try
            {
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(login.getByModal()));
                login.clickOkayButton();
            }
            catch (TimeoutException)
            {
                TestContext.Progress.WriteLine("No alert present to accept.");
            }

            login.clickSignInButton();
            protoComShop = new ProtoComShopPageObj(driver);
            try
            {
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TitleContains(protoComShop.getExpectedTitle()));
            }
            catch (WebDriverTimeoutException)
            {
                TestContext.Progress.WriteLine("Timeout while waiting for title to contain expected text.");
            }

            Assert.That(protoComShop.getCurrentTitle(),Is.EqualTo(protoComShop.getExpectedTitle()));
        }
        [Test,Category("Smoke")]
        public void Login_with_Invalid_credentials_for_student()
        {
            TestContext.Progress.WriteLine("Executing Test 2");
            driver.Url = ConfigurationManager.AppSettings["baseUrl"]??"NULL";
            login.setUserName("rahulshettyacademy");
            login.setPassword("learning1");
            login.clickSignInButton();
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                .TextToBePresentInElementValue(login.getSignInButtonElement(),"Sign In"));
            Assert.That(login.getTextFromElement(login.getErrorMessageElement()), Is.EqualTo(login.getInvalidErrorMessage()));
        }
        [Test, Category("Smoke")]
        public void Login_with_Empty_credentials_for_student()
        {
            TestContext.Progress.WriteLine("Executing Test 3");
            driver.Url = ConfigurationManager.AppSettings["baseUrl"]??"NULL";
            login.setUserName("");
            login.setPassword("");
            login.clickSignInButton();
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                .TextToBePresentInElementValue(login.getSignInButtonElement(), "Sign In"));
            Assert.That(login.getTextFromElement(login.getErrorMessageElement()), Is.EqualTo(login.getEmptyErrorMessage()));
        }
        [Test, Category("Smoke")]
        public void Login_with_valid_credentials_for_teachers()
        {
            TestContext.Progress.WriteLine("Executing Test 4");
            driver.Url = ConfigurationManager.AppSettings["baseUrl"]??"NULL";;
            login.setUserName("rahulshettyacademy");
            login.setPassword("learning");
            login.selectByText("Teacher");
            login.clickSignInButton();
            protoComShop = new ProtoComShopPageObj(driver);
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TitleContains(protoComShop.getExpectedTitle()));
            Assert.That(protoComShop.getCurrentTitle(), Is.EqualTo(protoComShop.getExpectedTitle()));
        }
        [Test, Category("Smoke")]
        public void Login_with_Invalid_credentials_for_teachers()
        {
            TestContext.Progress.WriteLine("Executing Test 5");
            driver.Url = ConfigurationManager.AppSettings["baseUrl"]??"NULL";
            login.setUserName("rahulshettyacademy");
            login.setPassword("learning1");
            login.selectByText("Teacher");
            login.clickSignInButton();
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                .TextToBePresentInElementValue(login.getSignInButtonElement(), "Sign In"));
            Assert.That(login.getTextFromElement(login.getErrorMessageElement()), Is.EqualTo(login.getInvalidErrorMessage()));
        }
        [Test, Category("Smoke")]
        public void Login_with_Empty_credentials_for_Teachers()
        {
            TestContext.Progress.WriteLine("Executing Test 6");
            driver.Url = ConfigurationManager.AppSettings["baseUrl"]??"NULL";;
            login.setUserName("");
            login.setPassword("");
            login.selectByText("Teacher");
            login.clickSignInButton();
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                .TextToBePresentInElementValue(login.getSignInButtonElement(), "Sign In"));
            Assert.That(login.getTextFromElement(login.getErrorMessageElement()), Is.EqualTo(login.getEmptyErrorMessage()));
        }        
    }
}
