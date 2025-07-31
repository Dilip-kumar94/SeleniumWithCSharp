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
[Parallelizable(ParallelScope.Self)]
public class E2ETest_Parallel:ParallelUtil
{
    protected static JSONReaderUtil? util;
    /// <summary>
    /// We should not keep the page object initialization in the Setup method. In order for the parallel execution to work correctly.
    /// </summary>


    [Test,TestCaseSource(nameof(getTestCaseData)),Parallelizable(ParallelScope.All),Category("E2EParallel")]
    public void Test_End_to_End_2(string userName, string passWord)
    {
        LoginPageObj login = new LoginPageObj(getDriver()!);
        WebDriverWait wait = new WebDriverWait(getDriver()!, TimeSpan.FromSeconds(10));
        login.setUserName(userName);
        login.setPassword(passWord);
        ProtoComShopPageObj protoComShop = login.clickSignInButton();
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TitleContains("ProtoCommerce"));
        protoComShop.AddProductToCart("Blackberry");
        protoComShop.AddProductToCart("Nokia Edge");
        CheckoutPageObj checkoutPage = protoComShop.ClickCheckoutButton();
        IList<string> ExpectedList = protoComShop.getProductNames();
        IList<string> ActualList = checkoutPage.getProductNames();
        ConfirmationPageObj confirmationPage = checkoutPage.clickCheckoutButton();
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
    public static IEnumerable<TestCaseData> getTestCaseData()
    {
        util = new JSONReaderUtil("TestData/TestData.json");
        yield return new TestCaseData(util.GetValue("username"), util.GetValue("password"));
        yield return new TestCaseData(util.GetValue("usernameinvalid"), util.GetValue("passwordinvalid"));
            
    }
}
