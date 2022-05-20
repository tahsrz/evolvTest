using OpenQA.Selenium;
using System;
using System.Collections.ObjectModel;

namespace evolvAutoFramework.Helpers
{
    public static class ByExtensions
    {
        public static void Click(this By locator, IWebDriver driver)
        {
            Utilities.AutoRetry(() => driver.FindClickableElement(locator).Click());
        }

        public static void Submit(this By locator, IWebDriver driver)
        {
            Utility.AutoRetry(() => driver.FindClickableElement(locator).Submit());
        }

        public static void SendKeys(this By locator, IWebDriver driver, string text)
        {
            Utility.AutoRetry(() => driver.FindClickableElement(locator).SendKeys(text));
        }

        public static void Clear(this By locator, IWebDriver driver)
        {
            Utility.AutoRetry(() => driver.FindClickableElement(locator).Clear());
        }

        public static void ManualClear(this By locator, IWebDriver driver)
        {
            Utility.AutoRetry(() =>
            {
                IWebElement element = driver.FindClickableElement(locator);
                element.Click();
                element.SendKeys(Keys.Control + "a");
                element.SendKeys(Keys.Backspace);
                Console.WriteLine("Cleared Successfully");
            });
        }

        public static void SetTextBoxValueAndVerify(this By locator, IWebDriver driver, string text)
        {
            Utility.AutoRetry(() => locator.SetTextBoxValue(driver, text));
        }

        private static void SetTextBoxValue(this By locator, IWebDriver driver, string text)
        {
            driver.FindClickableElement(locator).Clear();
            driver.FindClickableElement(locator).SendKeys(text);
            string result = PollingUtility.PollingWait(() => locator.GetTextBoxValue(driver), text);
            if (result != text)
                throw new Exception($"Set Text Box Value failed. Expected: {text}, Actual: {result}");
        }

        public static string GetTextNotNull(this By locator, IWebDriver driver)
        {
            return PollingUtility.PollingWaitNotNullOrEmpty(() => locator.GetText(driver));
        }

        public static string GetTextBoxValue(this By locator, IWebDriver driver)
        {
            return driver.FindClickableElement(locator).GetAttribute("value");
        }

        public static string GetText(this By locator, IWebDriver driver)
        {
            return driver.FindElement(locator).Text;
        }

        public static string GetAttribute(this By locator, IWebDriver driver, string attribute)
        {
            return driver.FindElement(locator).GetAttribute(attribute);
        }

        public static string GetCssValue(this By locator, IWebDriver driver, string value)
        {
            return driver.FindElement(locator).GetCssValue(value);
        }

        public static IWebElement GetElement(this By locator, IWebDriver driver)
        {
            return driver.FindElement(locator);
        }

        public static ReadOnlyCollection<IWebElement> GetElements(this By locator, IWebDriver driver)
        {
            return driver.FindElements(locator);

        }

        public static IWebElement GetElementRetry(this By locator, IWebDriver driver)
        {
            Utility.AutoRetry(() => driver.FindElement(locator));
            return driver.FindElement(locator);
        }
    }
}