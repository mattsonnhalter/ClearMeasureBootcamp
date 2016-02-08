using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;

namespace SmokeTests.StepDefinitions
{
    [Binding]
    public class SmokeTestSteps
    {
        private IWebDriver _driver;
        private static ChromeDriverService _chromeDriverService;
        private static readonly string DriverPath = AppDomain.CurrentDomain.BaseDirectory.Replace("bin\\Debug", "Drivers");
        private static string _homePage = "http://localhost:43507/";

        //Hooks
        [BeforeFeature]
        public static void StartChromeDriverService()
        {
            _chromeDriverService = ChromeDriverService.CreateDefaultService(DriverPath, "chromedriver.exe");
            _chromeDriverService.Start();
        }

        [AfterFeature]
        public static void StopChromeDriverService()
        {
            _chromeDriverService.Dispose();
        }

        [BeforeStep]
        public void WaitForLoad()
        {
            _driver?.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(30));
        }

        //Given
        [Given(@"I am using (.*)")]
        public void GivenIAmUsing(string browser)
        {
            switch (browser)
            {
                case "Firefox":
                    _driver = new FirefoxDriver();
                    break;
                case "Chrome":
                    _driver = new RemoteWebDriver(_chromeDriverService.ServiceUrl, DesiredCapabilities.Chrome());
                    break;
                case "IE":
                    _driver = new InternetExplorerDriver(DriverPath);
                    break;
                case "PhantomJS":
                    _driver = new PhantomJSDriver();
                    break;
                default:
                    throw new ArgumentException("Unknown browser");
            }
        }

        [Given(@"I am not logged in on '(.*)'")]
        public void GivenIAmNotLoggedIn(string url)
        {
            _driver.Navigate().GoToUrl(url);
            SatisfyLoginCondition(false);
        }

        [Given(@"I am logged in on '(.*)'")]
        public void GivenIAmLoggedIn(string url)
        {
            _driver.Navigate().GoToUrl(url);
            SatisfyLoginCondition(true);
        }

        [Given(@"I am on the home page")]
        public void GivenIAmOnHomePage()
        {
            _driver.Navigate().GoToUrl(_homePage);
        }

        private void SatisfyLoginCondition(bool loggedIn)
        {
            var pageTitle = _driver.Title;
            if (!pageTitle.StartsWith("Login", StringComparison.CurrentCulture))
            {
                if (loggedIn) return;
                var logout = _driver.FindElement(By.LinkText("Logout"));
                logout.Click();
            }
            else
            {
                if (!loggedIn) return;
                var login = _driver.FindElement(By.XPath("//button[contains(text(), 'Log In')]"));
                login.Click();
            }
        }

        //When
        [When(@"I browse to '(.*)'")]
        public void WhenIBrowseTo(string url)
        {
            _driver.Navigate().GoToUrl(url);
        }

        [When(@"I search for '(.*)'")]
        public void WhenISearchFor(string term)
        {
            var query = _driver.FindElement(By.Name("q"));
            query.SendKeys(term);
            query.Submit();
        }

        [When(@"I click on the (.*) link")]
        public void WhenIClickOn(string linkText)
        {
            var link = _driver.FindElement(By.LinkText(linkText));
            link.Click();
        }

        //Then
        [Then(@"the page title should start with (.*)")]
        public void ThenThePageTitleShouldStartWith(string title)
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
            wait.Until(d => d.Title.StartsWith(title, StringComparison.CurrentCultureIgnoreCase));
        }

        [Then(@"the page url should be exactly (.*)")]
        public void ThenThePageUrlShouldBeExactly(string url)
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
            wait.Until(d => d.Url.Equals(url, StringComparison.Ordinal));
        }

        [AfterScenario]
        public void Cleanup()
        {
            _driver?.Quit();
        }
    }
}
