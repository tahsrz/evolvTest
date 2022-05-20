using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Threading.Tasks;

namespace evolvAutoFramework
{
    public class TestInitializeHook
    {
        private static bool _driverinitialized = false;
        private static int _drivercount = 0;

       public async Task<IWebDriver> InitializeSettingsAsync()
        {
            var tempDriver = await OpenBrowserAsync();
            ++_drivercount;
            tempDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(ConfigurationHelpers.MaxWaitTime);
            tempDriver.Manage().Timeouts().PageLoad = TimeSpan.FromMilliseconds(ConfigurationHelpers.MaxPageLoadWait);
            return tempDriver;
        }

        private async Task<IWebDriver> OpenBrowserAsync()
        {
            switch (ConfigurationHelpers.BrowserType)
            {
                case (BrowserType.InternetExplorer):
                    throw new NotImplementedException();
                    new DriverManager().SetUpDriver(new EdgeConfig());
                    var edgeOptions = new EdgeOptions();
                    edgeOptions.AddArgument("--force-renderer-accessibility");
                    edgeOptions.AddArgument("--start-maximized");
                    edgeOptions.AcceptInsecureCertificates = true;
                    // edgeOptions.AddArgument("--headless");
                    return new EdgeDriver(edgeOptions);
                case BrowserType.FireFox:
                    throw new NotImplementedException();
                //new DriverManager().SetUpDriver(new FirefoxConfig());
                //var firefoxOptions = new FirefoxOptions();
                //firefoxOptions.AddArgument("--force-renderer-accessibility");
                //firefoxOptions.AddArgument("--start-maximized");
                //firefoxOptions.AcceptInsecureCertificates = true;
                ////firefoxOptions.AddArgument("--headless");
                //_driver = new FirefoxDriver(firefoxOptions);
                //break;
                default:
                    if (!_driverInitialized)
                    {
                        new DriverManager().SetUpDriver(new ChromeConfig(),
                            await ChromeVersionUtility.GetChromeDriverPartialUrlAsync());
                        _driverInitialized = true;
                    }
                    var chromeOptions = new ChromeOptions();
                    chromeOptions.AddArgument("--force-renderer-accessibility");
                    chromeOptions.AddArgument("--start-maximized");
                    chromeOptions.AcceptInsecureCertificates = true;
                    //chromeOptions.AddArgument("--headless");
                    return new ChromeDriver(chromeOptions);
            }
        
        }

        public void CloseBrowser(IWebDriver driver)
        {
            --_driverCount;
            driver.Close();

            if (_driverCount == 0)
            {
                switch (ConfigurationHelpers.BrowserType)
                {
                    case BrowserType.InternetExplorer:
                        throw new NotImplementedException();
                    case BrowserType.MicrosoftEdge:
                        ProcessKillUtility.KillEdgeDriver();
                        break;
                    case BrowserType.FireFox:
                        throw new NotImplementedException();
                    default:
                        ProcessKillUtility.KillChromeDriver();
                        break;
                }
            }
        }

        public void NavigateToHome(IWebDriver driver)
        {
            Utility.AutoRetry(() => driver.Navigate().GoToUrl(ConfigurationHelpers.RDWSBaseURI));
        }
    }
}

    }
}