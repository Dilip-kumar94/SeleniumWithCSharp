using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumCSFramework.PageObject
{
    public class ProtoComShopPageObj
    {
        private readonly IWebDriver driver;
        private readonly string expectedTitle = "ProtoCommerce";
        private IList<string> productNames = new List<string> { };
        [FindsBy(How = How.CssSelector, Using = ".card-title a")]
        private IList<IWebElement> productList;
        [FindsBy(How = How.XPath, Using = "//button[contains(text(),'Add')]")]
        private IList<IWebElement> addToCartList;
        [FindsBy(How = How.PartialLinkText, Using = "Checkout")]
        private IWebElement checkoutButton;

        public ProtoComShopPageObj(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }
        public string getExpectedTitle()
        {
            return expectedTitle;
        }
        public string getCurrentTitle()
        {
            return driver.Title;
        }
        public IList<IWebElement> getProductListElements()
        {
            return productList;
        }
        public IList<IWebElement> getAddToCartElements()
        {
            return addToCartList;
        }
        public void AddProductToCart(string productName)
        {
            foreach (IWebElement product in getProductListElements())
            {
                if (product.Text.Contains(productName))
                {
                    getAddToCartElements().ElementAt(getProductListElements().IndexOf(product)).Click();
                    productNames.Add(productName);
                }
            }
        }
        public IList<string> getProductNames()
        {
            return productNames;
        }
        public IWebElement getCheckoutButton()
        {
            return checkoutButton;
        }
        public CheckoutPageObj ClickCheckoutButton()
        {
            getCheckoutButton().Click();
            return new CheckoutPageObj(driver);
        }
    }
}
