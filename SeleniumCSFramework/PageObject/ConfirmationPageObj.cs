using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumCSFramework.PageObject
{
    public class ConfirmationPageObj
    {
        private IWebDriver driver;
        private By suggestion = By.XPath("//div[@class='suggestions']/ul/li/a");
        [FindsBy(How = How.Id, Using = "country")]
        private IWebElement countryInput;
        [FindsBy(How = How.XPath, Using = "//div[@class='suggestions']/ul/li/a")]
        private IList<IWebElement> listOfSuggestions;
        [FindsBy(How = How.CssSelector, Using = "label[for='checkbox2']")]
        private IWebElement agreeCheckbox;
        [FindsBy(How = How.XPath, Using = "//input[@type='submit']")]
        private IWebElement purchaseButton;
        [FindsBy(How = How.CssSelector, Using = ".alert-success")]
        private IWebElement successMessage;
        public ConfirmationPageObj(IWebDriver driver) 
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }
        public void setCountry(string country)
        {
            countryInput.SendKeys(country);
        }
        public void selectCountryFromSuggestions(string country)
        {
            foreach (IWebElement suggestion in listOfSuggestions)
            {
                if (suggestion.Text.Equals(country, StringComparison.OrdinalIgnoreCase))
                {
                    suggestion.Click();
                    break;
                }
            }
        }
        public void clickAgreeCheckbox()
        {
            if (!agreeCheckbox.Selected)
            {
                agreeCheckbox.Click();
            }
        }
        public void clickPurchaseButton()
        {
            purchaseButton.Click();
        }
        public string getSuccessMessage()
        {
            return successMessage.Text;
        }
        public By getSuggestion()
        {
            return suggestion;   
        }
    }
}
