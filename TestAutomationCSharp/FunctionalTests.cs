using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAutomationCSharp
{
    internal class FunctionalTests
    {
        private ChromeDriver Driver;
        Actions actions;
        [SetUp]
        public void Setup()
        {
            TestContext.Progress.WriteLine("Setting up the test environment.");
            Driver = new ChromeDriver();
            actions = new Actions(Driver);
        }
        [Test]
        public void Title_test()
        {
            TestContext.Progress.WriteLine("Executing Test 1");
            Driver.Url = "https://rahulshettyacademy.com/AutomationPractice/";
            string title = Driver.Title;
            Assert.That(title, Is.EqualTo("Practice Page"));
        }
        [Test]
        public void test_radiobutton() {
            TestContext.Progress.WriteLine("Executing Test 2");
            Driver.Url = "https://rahulshettyacademy.com/AutomationPractice/";
            List<IWebElement> radiobuttonList = Driver.FindElements(By.CssSelector("input[name='radioButton']")).ToList();
            TestContext.Progress.WriteLine("No of Elements:" + radiobuttonList.Count);
            foreach (IWebElement element in radiobuttonList)
            {
                string selectedtext = element.Selected ? element.GetAttribute("value") + " Selected" : element.GetAttribute("value") + " Not Selected";
                TestContext.Progress.WriteLine(selectedtext);
                element.Click();
                selectedtext = element.Selected ? element.GetAttribute("value") + " Selected" : element.GetAttribute("value") + " Not Selected";
                TestContext.Progress.WriteLine(selectedtext);
            }
            Assert.Pass();
        }
        [Test]
        public void test_autoSuggestTextBox()
        {
            TestContext.Progress.WriteLine("Executing Test 3");
            Driver.Url = "https://rahulshettyacademy.com/AutomationPractice/";
            Driver.FindElement(By.Id("autocomplete")).SendKeys("Ind");
            Thread.Sleep(2000); // Wait for suggestions to load
            List<IWebElement> listofSuggestions = Driver.FindElements(By.CssSelector(".ui-menu-item div")).ToList();
            foreach (IWebElement suggestion in listofSuggestions)
            {
                TestContext.Progress.WriteLine("Suggestion: " + suggestion.Text);
                if (suggestion.Text.Equals("India"))
                {
                    suggestion.Click();
                    TestContext.Progress.WriteLine(Driver.FindElement(By.Id("autocomplete")).GetAttribute("value"));
                    break;
                }

            }
        }
        [Test]
        public void test_DropDown()
        {
            TestContext.Progress.WriteLine("Executing Test 4");
            Driver.Url = "https://rahulshettyacademy.com/AutomationPractice/";
            IWebElement dropdown = Driver.FindElement(By.Id("dropdown-class-example"));
            SelectElement elmt = new SelectElement(dropdown);
            foreach (IWebElement option in elmt.Options)
            {
                TestContext.Progress.WriteLine("Option: " + option.Text);
                if (option.Text.Equals("Option3"))
                {
                    option.Click();
                    break;
                }
            }
        }
        [Test]
        public void test_CheckBox()
        {
            TestContext.Progress.WriteLine("Executing Test 5");
            Driver.Url = "https://rahulshettyacademy.com/AutomationPractice/";
            List<IWebElement> listofcheckbox = Driver.FindElements(By.CssSelector("input[type='checkbox']")).ToList();
            foreach (IWebElement checkbox in listofcheckbox)
            {
                TestContext.Progress.WriteLine("Checkbox: " + checkbox.GetAttribute("value"));
                if (checkbox.GetAttribute("value").Equals("option3"))
                {
                    checkbox.Click();
                    TestContext.Progress.WriteLine("Checkbox " + checkbox.GetAttribute("value") + " is now " + (checkbox.Selected ? "Selected" : "Not Selected"));
                }
            }
        }
        [Test]
        public void test_SwitchWindow()
        {
            TestContext.Progress.WriteLine("Executing Test 6");
            Driver.Url = "https://rahulshettyacademy.com/AutomationPractice/";
            string windowHandle = Driver.CurrentWindowHandle;
            Driver.FindElement(By.Id("openwindow")).Click();
            TestContext.Progress.WriteLine("Current window:" + Driver.CurrentWindowHandle.ToString());
            Driver.WindowHandles.ToList().ForEach(handle =>
            {
                TestContext.Progress.WriteLine("Title of the Current window: " + Driver.Title);
                TestContext.Progress.WriteLine("Current Window: " + handle);
                if (handle != windowHandle)
                {
                    Driver.SwitchTo().Window(handle);
                    TestContext.Progress.WriteLine("Switched to new window: " + handle);
                }
                TestContext.Progress.WriteLine("Title of the new window: " + Driver.Title);
                TestContext.Progress.WriteLine("New Window: " + handle);
            });

        }
        [Test]
        public void test_openTab()
        {
            TestContext.Progress.WriteLine("Executing Test 7");
            Driver.Url = "https://rahulshettyacademy.com/AutomationPractice/";
            string currentWindowHandle = Driver.CurrentWindowHandle;
            Driver.FindElement(By.Id("opentab")).Click();
            TestContext.Progress.WriteLine("Current window: " + currentWindowHandle);
            Driver.SwitchTo().Window(Driver.WindowHandles.Last());
            TestContext.Progress.WriteLine("Current window: " + currentWindowHandle);
            Thread.Sleep(5000); // Wait for the new tab to load
        }
        [Test]
        public void test_Alert()
        {
            TestContext.Progress.WriteLine("Executing Test 8");
            Driver.Url = "https://rahulshettyacademy.com/AutomationPractice/";
            Driver.FindElement(By.Id("name")).SendKeys("Dilip");
            Thread.Sleep(8000); // Wait for the user to type
            Driver.FindElement(By.Id("alertbtn")).Click();
            try
            {
                IAlert alert = Driver.SwitchTo().Alert();
                Thread.Sleep(2000); // Wait for the alert to appear
                alert.Accept();
            }
            catch (NoAlertPresentException)
            {
                TestContext.Progress.WriteLine("No alert present to accept.");
            }
            catch (Exception e)
            {
                TestContext.Progress.WriteLine("An error occurred while handling the alert: " + e.Message);
            }

        }
        [Test]
        public void test_confirm_accept()
        {
            TestContext.Progress.WriteLine("Executing Test 9");
            Driver.Url = "https://rahulshettyacademy.com/AutomationPractice/";
            Driver.FindElement(By.Id("name")).SendKeys("Dilip");
            Driver.FindElement(By.Id("confirmbtn")).Click();
            Thread.Sleep(2000); // Wait for the user to type
            bool val=true;
            try
            {
                IAlert alert = Driver.SwitchTo().Alert();
                Thread.Sleep(2000); // Wait for the alert to appear
                val = alert.Text.Contains("Dilip");
                alert.Accept();
            }
            catch (NoAlertPresentException)
            {
                TestContext.Progress.WriteLine("No alert present to accept.");
            }
            catch (Exception e)
            {
                TestContext.Progress.WriteLine("An error occurred while handling the alert: " + e.Message);
            }
            Assert.True(val,"Message doesn't contain the name!!");
        }
        [Test]
        public void test_confirm_dismiss()
        {
            TestContext.Progress.WriteLine("Executing Test 10");
            Driver.Url = "https://rahulshettyacademy.com/AutomationPractice/";
            Driver.FindElement(By.Id("name")).SendKeys("Dilip");
            Driver.FindElement(By.Id("confirmbtn")).Click();
            Thread.Sleep(2000); // Wait for the user to type
            try
            {
                IAlert alert = Driver.SwitchTo().Alert();
                Thread.Sleep(2000); // Wait for the alert to appear
                alert.Dismiss();
            }
            catch (NoAlertPresentException)
            {
                TestContext.Progress.WriteLine("No alert present to dismiss.");
            }
            catch (Exception e)
            {
                TestContext.Progress.WriteLine("An error occurred while handling the alert: " + e.Message);
            }
        }
        [Test]
        public void test_Table()
        {
            List<string> expectedList = new List<string> { };
            List<string> actualList = new List<string> { };
            TestContext.Progress.WriteLine("Executing Test 11");
            Driver.Url = "https://rahulshettyacademy.com/seleniumPractise/#/offers";
            SelectElement pageDD = new SelectElement(Driver.FindElement(By.CssSelector("#page-menu")));
            pageDD.SelectByValue("20");
            assigningArrayList(ref expectedList);
            expectedList.Sort();
            Driver.FindElement(By.XPath("//thead/tr/th[1]")).Click();
            assigningArrayList(ref actualList);
            TestContext.Progress.WriteLine("Expected List: " + string.Join(", ", expectedList));
            TestContext.Progress.WriteLine("Expected List: " + string.Join(", ", actualList));
            Assert.That(actualList, Is.EqualTo(expectedList), "The actual list does not match the expected list.");
        }

        public void assigningArrayList(ref List<string> list)
        {
            List<IWebElement> tableRow = Driver.FindElements(By.XPath("//tbody/tr")).ToList();
            foreach (IWebElement row in tableRow)
            {
                List<IWebElement> columns = row.FindElements(By.TagName("td")).ToList();
                for (int i = 0; i < columns.Count; i++)
                {
                    if (i == 0)
                    {
                        string text = columns[i].Text;
                        list.Add(text);
                    }
                }
            }

        }
        [Test]
        public void test_VisibleElement()
        {
            TestContext.Progress.WriteLine("Executing Test 12");
            Driver.Url = "https://rahulshettyacademy.com/AutomationPractice/";
            actions.ScrollToElement(Driver.FindElement(By.CssSelector("input[placeholder='Hide/Show Example'"))).Perform();
            Driver.FindElement(By.Id("hide-textbox")).Click();
            bool result = Driver.FindElement(By.CssSelector("input[placeholder='Hide/Show Example'")).Displayed;
            Driver.FindElement(By.Id("show-textbox")).Click();
            bool result1 = Driver.FindElement(By.CssSelector("input[placeholder='Hide/Show Example'")).Displayed;
            Assert.Multiple(() =>
            {
                Assert.IsFalse(result, "The element should not be visible after hiding.");
                Assert.IsTrue(result1, "The element should be visible after showing.");
            });
        }
        [Test]
        public void test_tablePagination()
        {
            TestContext.Progress.WriteLine("Executing Test 13");
            Dictionary<int, List<List<IWebElement>>> dictlistofcolumn = new();
            List<List<IWebElement>> listofcolumn=new();
            Driver.Url = "https://rahulshettyacademy.com/AutomationPractice/";
            actions.ScrollToElement(Driver.FindElement(By.XPath("//div[@class='tableFixHead']/table[@id='product']"))).Click().Perform();
            List<IWebElement>listofrow = Driver.FindElements(By.XPath("//div[@class='tableFixHead']/table[@id='product']/tbody/tr")).ToList();
            for(int i=0; i< listofrow.Count;i++)
            {
                List<IWebElement> columns = listofrow[i].FindElements(By.TagName("td")).ToList();             
                listofcolumn.Add(columns);
                if(i == listofrow.Count - 1)
                {
                    dictlistofcolumn.Add(i, listofcolumn);
                }        
            }
            PrintDictionaryofColumns(dictlistofcolumn);

        }

        private void PrintDictionaryofColumns(Dictionary<int, List<List<IWebElement>>> dictlistofcolumn)
        {
            foreach (var kvp in dictlistofcolumn)
            {
                TestContext.Progress.WriteLine($"Page: {kvp.Key + 1}");
                foreach (var row in kvp.Value)
                {
                    StringBuilder rowText = new StringBuilder();
                    foreach (var cell in row)
                    {
                        rowText.Append(cell.Text + " | ");
                    }
                    TestContext.Progress.WriteLine(rowText.ToString().TrimEnd(' ', '|'));
                }
            }
        }
        [Test]
        public void test_MouseHover()
        {
            TestContext.Progress.WriteLine("Executing Test 15");
            Driver.Url = "https://rahulshettyacademy.com/AutomationPractice/";
            Driver.Manage().Window.Maximize();
            actions.ScrollByAmount(0,800).Perform();
            IWebElement mouseHoverElement = Driver.FindElement(By.Id("mousehover"));
            actions.MoveToElement(mouseHoverElement).Perform();
            IWebElement subMenu = Driver.FindElement(By.XPath("//div[@class='mouse-hover-content']/a[1]"));
            actions.MoveToElement(subMenu).Click().Perform();
            Assert.False(subMenu.Displayed);
            Thread.Sleep(8000);
        }

        [Test]
        public void test_DragandDrop()
        {
            TestContext.Progress.WriteLine("Executing Test 14");
            Driver.Url = "https://demoqa.com/droppable/";
            IWebElement source = Driver.FindElement(By.Id("draggable"));
            IWebElement target = Driver.FindElement(By.Id("droppable"));
            actions.ScrollToElement(source).Perform();
            actions.DragAndDrop(source, target).Perform();
            StringAssert.AreEqualIgnoringCase("dropped!", Driver.FindElement(By.XPath("//div[@id='droppable']/p")).Text);
        }

        [TearDown]
        public void TearDown()
        {
            TestContext.Progress.WriteLine("Cleaning up after the test.");
            Driver?.Dispose();
        }
    }
}
