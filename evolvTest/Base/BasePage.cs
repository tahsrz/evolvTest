using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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
            foreach (var webElement in webElements)
            {
                if (value == webElement.GetAttribute(attribute))
                {
                    return webElement;
                }
            }

            throw new Exception($"IWebelement with Attribute: {attribute} and Value: {value} could not be found.");
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
            Driver.SwitchTo().Window(Driver.WindowHandles[Driver.WindowHandles.Count - 1]);
        }

        public void SwitchToPreviousWindow()
        {
            Driver.SwitchTo().Window(PreviousWindows.Pop());
        }

        public void CloseCurrentWindow()
        {
            Driver.Close();
        }
    }
}