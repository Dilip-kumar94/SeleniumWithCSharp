using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V136.Network;
using OpenQA.Selenium.DevTools.V137.Storage;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Linq;

namespace SampleTestAutomationCS
{
    internal class Program
    {
        class innerClass
        {
            IList<string> listofString = new List<string>();
            private const string SHOP_BUTTONS_SELECTOR = "return document.querySelector(\"body > shop-app\").shadowRoot.querySelector(\"iron-pages > shop-home\").shadowRoot.querySelectorAll(\"div > shop-button > a\")";
            private const string SHOP_TEXT_SELECTOR = "return document.querySelector(\"body > shop-app\").shadowRoot.querySelector(\"iron-pages > shop-home\").shadowRoot.querySelectorAll(\"div > h2\")";

            private IList<IWebElement> GetElementsByShadowSelector(IJavaScriptExecutor js, string selector)
            {
                return (IList<IWebElement>)js.ExecuteScript(selector)! ?? new List<IWebElement>();
            }

            public void innerMethod()
            {
                ChromeDriverService service = ChromeDriverService.CreateDefaultService();
                service.SuppressInitialDiagnosticInformation = true;
                service.HideCommandPromptWindow = true;
                ChromeOptions options = new ChromeOptions();

                ChromeDriver driver = new(service, options);

                driver.Url = "https://shop.polymer-project.org/";
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                
                var shopButtons = GetElementsByShadowSelector(js, SHOP_BUTTONS_SELECTOR);
                var shopTextElements = GetElementsByShadowSelector(js, SHOP_TEXT_SELECTOR);
                
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                
                var shopTexts = shopTextElements.Select(el => el.Text).ToList();
                               
                foreach (var (button, text) in shopButtons.Zip(shopTexts))
                {
                    Console.WriteLine(text);
                    button.Click();
                    wait.Until(ExpectedConditions.TitleContains(text));
                    Console.WriteLine(driver.Title);
                    driver.Navigate().Back();
                }
                
                driver.Quit();
            }
            public void innerMethod2()
            {
                foreach (string str in listofString)
                {
                    Debug.WriteLine(str);
                    Console.WriteLine(str);
                }
            }
        }

        static void Main(string[] args)
        {
            innerClass inner = new innerClass();
            inner.innerMethod();
        }
    }
}
