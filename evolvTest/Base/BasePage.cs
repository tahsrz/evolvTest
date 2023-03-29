using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace evolvAutoFramework.Base
{
    public abstract class BasePage
    {
        public readonly IWebDriver Driver;

        private readonly Stack<string> PreviousWindows = new Stack<string>();

        public BasePage(IWebDriver driver)
        {
            Driver = driver;
        }

        public IWebElement FindElementInCollectionByAttribute(string attribute, string value, ReadOnlyCollection<IWebElement> webElements)
        {
            return webElements.FirstOrDefault(webElement => value == webElement.GetAttribute(attribute))
                ?? throw new Exception($"IWebElement with Attribute: {attribute} and Value: {value} could not be found.");
        }

        public string GetCurrentWindowHandle()
        {
            return Driver.CurrentWindowHandle;
        }

        public string GetTitle()
        {
            return Driver.Title;
        }

        public void SwitchWindowLast()
        {
            PreviousWindows.Push(Driver.CurrentWindowHandle);
            Driver.SwitchTo().Window(Driver.WindowHandles.Last());
        }

        public void SwitchToPreviousWindow()
        {
            {
                if (PreviousWindows.Count == 0)
                {
                    throw new InvalidOperationException("There is no previous window to switch to.");
                }

                Driver.SwitchTo().Window(PreviousWindows.Pop());
            }
        }
    }
}