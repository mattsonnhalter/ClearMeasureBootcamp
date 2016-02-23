using System;
using System.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;

namespace ClearMeasure.Bootcamp.SmokeTests.StepDefinitions
{
    [Binding]
    public class SmokeTestSteps
    {
        private IWebDriver _driver;
        private static ChromeDriverService _chromeDriverService;

        private static readonly string DriversPath = SmokeTestPaths.GetDriversPath();

        private static readonly string HomePage = ConfigurationManager.AppSettings["siteUrl"];

        //Hooks
        [BeforeTestRun]
        public static void StartChromeDriverService()
        {
//            _chromeDriverService = ChromeDriverService.CreateDefaultService(DriversPath, "chromedriver.exe");
//            _chromeDriverService.Start();
        }

        [AfterTestRun]
        public static void StopChromeDriverService()
        {
            _chromeDriverService.Dispose();
        }

        [BeforeScenario]
        public void SelectBrowserFromAppConfig()
        {
            var browser = ConfigurationManager.AppSettings["browser"];
            SelectBrowser(browser);
        }

        private void SelectBrowser(string browser)
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
                    _driver = new InternetExplorerDriver(DriversPath);
                    break;
                case "PhantomJS":
                    var phantomJsPath = SmokeTestPaths.GetPhantomJsPath();
                    var driverService = PhantomJSDriverService.CreateDefaultService(phantomJsPath);
                    _driver = new PhantomJSDriver(driverService);
                    break;
                default:
                    throw new ArgumentException("Unknown browser");
            }
        }

        [BeforeStep]
        public void WaitForLoad()
        {
            _driver?.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
        }

        //Given
        [Given(@"I am on the site")]
        public void GivenIAmOnTheSite()
        {
            _driver.Navigate().GoToUrl(HomePage);
        }

        //When
        [When(@"I log in")]
        public void WhenILogIn()
        {
            SatisfyLoginCondition(true);
        }

        [When(@"I log out")]
        public void WhenILogout()
        {
            SatisfyLoginCondition(false);
        }

        private void SatisfyLoginCondition(bool loggedIn)
        {
            if (!_driver.Title.StartsWith("Login", StringComparison.CurrentCulture))
            {
                if (loggedIn) return;
                var logout = _driver.FindElement(By.LinkText("Logout"));
                logout.Click();
            }
            else
            {
                if (!loggedIn) return;
                var userSelect = new SelectElement(_driver.FindElement(By.Id("UserName")));
                userSelect.SelectByIndex(0);
                var login = _driver.FindElement(By.XPath("//button[contains(text(), 'Log In')]"));
                login.Click();
            }
        }

        [When(@"I browse to '(.*)'")]
        public void WhenIBrowseTo(string url)
        {
            _driver.Navigate().GoToUrl(url);
        }

        [When(@"I browse to the site")]
        public void WhenIBrowseToTheSite()
        {
            _driver.Navigate().GoToUrl(HomePage);
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
        [Then(@"I should be on the (.*) page")]
        public void ThenIShouldBeOnPage(string page)
        {
            var completeUrl = HomePage + SmokeTestPageUrls.PageUrls[page];
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
            wait.Until(d => d.Url.Equals(completeUrl, StringComparison.Ordinal));
        }

        [AfterScenario]
        public void Cleanup()
        {
            _driver?.Quit();
        }
    }
}