using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace evolvAutoFramework.Helpers
{
    public static class DriverExtensions
    {
        public static object ExpectedConditions { get; private set; }

        private static WebDriverWait GetWaitWebDriver(this IWebDriver driver)
        {
            return new WebDriverWait(driver, TimeSpan.FromMilliseconds(ConfigurationHelpers.MaxWaitTime));
        }

        public static IWebElement FindClickableElement(this IWebDriver driver, By locator)
        {
            // TODO: Figure out the new way of doing this ExpectedConditions that is not depricated
            return driver.GetWaitWebDriver().Until(ExpectedConditions.ElementToBeClickable(locator));
        }

        public static IWebElement FindVisibleElement(this IWebDriver driver, By locator)
        {
            // TODO: Figure out the new way of doing this ExpectedConditions that is not depricated
            _ = driver.GetWaitWebDriver().Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(locator));
            return driver.FindElement(locator);
        }
    }
}
