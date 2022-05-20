using NUnit.Framework;
using OpenQA.Selenium;
using evolvAutoFramework.Base;
using evolvAutoFramework.Helpers;
using evolvAutoFramework.Pages;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace evolvAutoFramework
{
    internal class TestBase : TestInitializeHook
    {
        protected HomePage homePage;

        private IWebDriver Driver;

        [OneTimeSetUp]
        public async Task OneTimeSetUpBaseAsync()
        {
            HttpClient.DefaultProxy = new WebProxy() { BypassProxyOnLocal = true };
            HttpClient.DefaultProxy.Credentials = CredentialCache.DefaultCredentials;
            Driver = await InitializeSettingsAsync();
        }

        [SetUp]
        public void SetUpBase()
        {
            NavigateToHome(Driver);
            homePage = new LoginPage(Driver).Login();
        }

        [TearDown]
        public async Task TearDownBaseAsync()
        {
            try
            {
                Utility.AutoRetry(() => homePage.OpenUserMenu().Logout());
            }
            catch
            {
                CloseBrowser(Driver);
                Driver = await InitializeSettingsAsync();
            }
        }

        [OneTimeTearDown]
        public void OneTimeTearDownBase()
        {
            CloseBrowser(Driver);
        }
    }
}