using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumCSFramework.PageObject
{
    public class LoginPageObj
    {
        private IWebDriver driver;
        private By modal = By.Id("myModal");
        private string expectedTitle = "Login Page";
        private string emptyErrorMessage = "Empty username/password.";
        private string invalidErrorMessage = "Incorrect username/password.";
        [FindsBy(How = How.Id, Using = "username")]
        private IWebElement UsernameField;
        [FindsBy(How.Name, "password")]
        private IWebElement PasswordField;
        [FindsBy(How.XPath, "//input[@name='radio']")]
        private IList<IWebElement> listofRadiobuttons;
        [FindsBy(How.Id, "okayBtn")]
        private IWebElement OkayButton;
        [FindsBy(How.Id, "signInBtn")]
        private IWebElement SignInButton;
        [FindsBy(How.CssSelector, ".alert.alert-danger.col-md-12")]
        private IWebElement ErrorMessage;
        [FindsBy(How.XPath, "//select[@class='form-control']")]
        private IWebElement SelectElement;


        public LoginPageObj(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }
        public IWebElement getUserNameElement()
        {
            return UsernameField;
        }
        public IWebElement getPasswordElement()
        {
            return PasswordField;
        }
        public IList<IWebElement> getRadioButtonElements()
        {
            return listofRadiobuttons;
        }
        public IWebElement getOkayButtonElement()
        {
            return OkayButton;
        }
        public IWebElement getSignInButtonElement()
        {
            return SignInButton;
        }
        public By getByModal()
        {
            return modal;
        }
        public string getExpectedTitle()
        {
            return expectedTitle;
        }
        public string getCurrentTitle()
        {
            return driver.Title;
        }
        public void setUserName(string username)
        {
            UsernameField.SendKeys(username);
        }
        public void setPassword(string password)
        {
            PasswordField.SendKeys(password);
        }
        public void clickOkayButton()
        {
            OkayButton.Click();
        }
        public ProtoComShopPageObj clickSignInButton()
        {
            SignInButton.Click();
            return new ProtoComShopPageObj(driver);
        }
        public IWebElement getErrorMessageElement()
        {
            return ErrorMessage;
        }
        public void setRadioButton(string value)
        {
            foreach (IWebElement element in getRadioButtonElements())
            {
                if (element.GetAttribute("value")?.Equals(value) == true)
                {
                    element.Click();
                    break;
                }
            }
        }
        public string getTextFromElement(IWebElement element)
        {
            return element.Text;
        }
        public IWebElement getSelectElement()
        {
            return SelectElement;
        }
        public void selectByText(String text)
        {
            SelectElement selectElement = new SelectElement(getSelectElement());
            selectElement.SelectByText(text);
        }
        public string getEmptyErrorMessage()
        {
            return emptyErrorMessage;
        }
        public string getInvalidErrorMessage()
        {
            return invalidErrorMessage;
        }
    }
}
