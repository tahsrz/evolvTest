📋 Overview
This framework provides a structured foundation for automated browser testing. It handles WebDriver lifecycle management, implements common design patterns, and includes utility extensions to reduce boilerplate code.

Built for test engineers who need a reliable starting point for web application testing without reinventing the wheel.

✨ Key Features
Page Object Model (POM): Clean separation between test logic and page structure.

WebDriver Management: Centralized driver setup, teardown, and browser configuration.

Driver Extensions: Reusable methods for common interactions (waiting, scrolling, element handling).

LINQ Integration: Utility methods to work with collections of elements more expressively.

NUnit Testing: Well-structured test classes with setup/teardown attributes.

🛠️ Tech Stack
Language: C# (.NET)

Test Framework: NUnit

Browser Automation: Selenium WebDriver

Patterns: Page Object Model, Singleton (Driver instance)

🚀 Getting Started
Prerequisites
.NET SDK (version 6.0 or later recommended)

A compatible browser (Chrome, Firefox, Edge) with the corresponding WebDriver

An IDE like Visual Studio, VS Code, or JetBrains Rider

Installation & Setup
Clone the repository:

bash
git clone https://github.com/tahsrz/evolvTest.git
Navigate to the project directory:

bash
cd evolvTest
Restore NuGet packages:

bash
dotnet restore
Build the solution:

bash
dotnet build
Run the tests:

bash
dotnet test
🧱 Project Structure
text
evolvTest/
├── evolvTest/               # Main test project
│   ├── Base/                # Base classes (BasePage, BaseTest)
│   ├── Pages/               # Page Object classes
│   ├── Tests/               # NUnit test classes
│   ├── Utilities/           # Helper classes, extensions, constants
│   └── app.config           # Configuration settings
├── evolvTest.sln            # Solution file
├── .gitignore
└── README.md
📐 Design Approach
BasePage: Contains core WebDriver interactions and wait logic. All page objects inherit from it.

DriverExtensions: Static methods that extend IWebDriver and IWebElement for common operations (e.g., WaitForElement, ScrollToView, GetTextSafe).

BaseTest: Handles test-level setup (driver initialization) and teardown (cleanup).

LINQ in Pages: Selectors and element collections leverage LINQ for more readable filtering and transformations.

🔧 Configuration
Driver settings (browser type, implicit wait timeout, base URL) can be managed in app.config. Modify this file to adjust test behavior without touching code.

Example snippet:

xml
<add key="browser" value="Chrome" />
<add key="baseUrl" value="https://example.com" />
<add key="implicitWaitSeconds" value="10" />
📝 Example: Writing a Test
Create a Page Object:

csharp
public class LoginPage : BasePage
{
    private IWebElement UsernameField => Driver.FindElement(By.Id("username"));
    private IWebElement PasswordField => Driver.FindElement(By.Id("password"));
    private IWebElement LoginButton => Driver.FindElement(By.Id("login"));

    public HomePage Login(string username, string password)
    {
        UsernameField.SendKeys(username);
        PasswordField.SendKeys(password);
        LoginButton.Click();
        return new HomePage(Driver);
    }
}
Write the Test:

csharp
[Test]
public void ValidUser_ShouldLoginSuccessfully()
{
    var loginPage = new LoginPage(Driver);
    var homePage = loginPage.Login("testuser", "correctpassword");
    
    Assert.That(homePage.IsUserLoggedIn(), Is.True);
}
📈 Future Improvements
Add parallel test execution support.

Integrate with a reporting tool (Allure, ExtentReports).

Implement retry logic for flaky tests.

Add Docker support for cross-browser grid execution.

🤝 Contributing
Contributions are welcome. If you find a bug or have an idea for an improvement, open an issue or submit a pull request.

