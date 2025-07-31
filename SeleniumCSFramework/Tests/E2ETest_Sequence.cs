using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumCSFramework.PageObject;
using SeleniumCSFramework.Utilities;
using System.Configuration;

namespace SeleniumCSFramework.Tests;

public class E2ETest_Sequence
{
    protected IWebDriver driver;
    protected WebDriverWait wait;
    protected Actions actions;
    protected LoginPageObj login;
    protected ProtoComShopPageObj protoComShop;
    protected ConfirmationPageObj confirmationPage;
    protected CheckoutPageObj checkoutPage;
    protected static JSONReaderUtil util;


    [OneTimeSetUp]
    public void Setup()
    {
        TestContext.Progress.WriteLine("Setting up the test environment.");
        driver = GetDriver(ConfigurationManager.AppSettings["browser"] ?? "default");
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        actions = new Actions(driver);
        login = new LoginPageObj(driver);
        


    }

    private IWebDriver GetDriver(string vdriver)
    {
        switch (vdriver)
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

    [Test,TestCaseSource(nameof(getTestCaseData)),Category("E2ESequence")]
    public void Test_End_to_End_2(string userName, string passWord)
    {
        driver.Url = ConfigurationManager.AppSettings["baseUrl"] ?? "Null";
        login.setUserName(userName);
        login.setPassword(passWord);
        protoComShop = login.clickSignInButton();
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TitleContains("ProtoCommerce"));
        protoComShop.AddProductToCart("Blackberry");
        protoComShop.AddProductToCart("Nokia Edge");
        checkoutPage = protoComShop.ClickCheckoutButton();
        IList<string> ExpectedList = protoComShop.getProductNames();
        IList<string> ActualList = checkoutPage.getProductNames();
        confirmationPage = checkoutPage.clickCheckoutButton();
        confirmationPage.setCountry("ind");
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(confirmationPage.getSuggestion()));
        confirmationPage.selectCountryFromSuggestions("India");
        confirmationPage.clickAgreeCheckbox();
        confirmationPage.clickPurchaseButton();
        Assert.Multiple(() =>
        {
            Assert.That(confirmationPage.getSuccessMessage().Contains("Success! Thank you! Your order will be delivered in next few weeks :-)."));
            Assert.That(ExpectedList.Count, Is.EqualTo(ActualList.Count), "Product count mismatch between expected and actual.");
            for (int i = 0; i < ExpectedList.Count; i++)
            {
                Assert.That(ExpectedList[i], Is.EqualTo(ActualList[i]), $"Product name mismatch at index {i}.");
            }
        });
    }

    [Test,TestCase("rahulshettyacademy", "learning"),Category("E2ESequence")]
    
    public void Test_End_to_End_1(string userName, string passWord)
    {
        driver.Url = ConfigurationManager.AppSettings["baseUrl"] ?? "Null";
        login.setUserName(userName);
        login.setPassword(passWord);
        protoComShop = login.clickSignInButton();
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TitleContains("ProtoCommerce"));
        protoComShop.AddProductToCart("Blackberry");
        protoComShop.AddProductToCart("Nokia Edge");
        checkoutPage = protoComShop.ClickCheckoutButton();
        IList<string> ExpectedList = protoComShop.getProductNames();
        IList<string> ActualList = checkoutPage.getProductNames();
        confirmationPage = checkoutPage.clickCheckoutButton();
        confirmationPage.setCountry("ind");
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(confirmationPage.getSuggestion()));
        confirmationPage.selectCountryFromSuggestions("India");
        confirmationPage.clickAgreeCheckbox();
        confirmationPage.clickPurchaseButton();
        Assert.Multiple(() =>
        {
            Assert.That(confirmationPage.getSuccessMessage().Contains("Success! Thank you! Your order will be delivered in next few weeks :-)."));
            Assert.That(ExpectedList.Count, Is.EqualTo(ActualList.Count), "Product count mismatch between expected and actual.");
            for (int i = 0; i < ExpectedList.Count; i++)
            {
                Assert.That(ExpectedList[i], Is.EqualTo(ActualList[i]), $"Product name mismatch at index {i}.");
            }
        });
    }

    [OneTimeTearDown]
    public void Teardown()
    {
        driver?.Dispose();
    }

    public static IEnumerable<TestCaseData> getTestCaseData()
    {
        util = new JSONReaderUtil("TestData/TestData.json");
        yield return new TestCaseData(util.GetValue("username"), util.GetValue("password"));
        yield return new TestCaseData(util.GetValue("username"), util.GetValue("password"));
            
    }
}
