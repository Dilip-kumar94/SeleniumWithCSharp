using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumCSFramework.PageObject
{
    public class CheckoutPageObj
    {
        private IWebDriver driver;
        private IList<string> productNames = new List<string> { };
        [FindsBy(How = How.CssSelector, Using = "h4 a")]
        private IList<IWebElement> checkoutProducts;
        [FindsBy(How = How.XPath, Using = "//button[contains(text(),'Checkout')]")]
        private IWebElement checkoutButton;
        public CheckoutPageObj(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        public IList<string> getProductNames()
        {
            foreach (IWebElement product in getListOfProductsElement())
            {
                productNames.Add(product.Text);
            }
            return productNames;

        }
        public IList<IWebElement> getListOfProductsElement()
        {
            return checkoutProducts;
        }
        public ConfirmationPageObj clickCheckoutButton()
        {
            checkoutButton.Click();
            return new ConfirmationPageObj(driver);
        }
    }
}
