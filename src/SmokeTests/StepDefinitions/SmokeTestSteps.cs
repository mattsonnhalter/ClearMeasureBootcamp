using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;

namespace SmokeTests.StepDefinitions
{
    [Binding]
    public class SmokeTestSteps
    {
        private IWebDriver _driver;

        //Given
        [Given(@"I am using (.*)")]
        public void GivenIAmUsing(string browser)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory.Replace("bin\\Debug", "Drivers");
            switch (browser)
            {
                case "Firefox":
                    _driver = new FirefoxDriver();
                    break;
                case "Chrome":
                    _driver = new ChromeDriver(path);
                    break;
                case "IE":
                    _driver = new InternetExplorerDriver(path);
                    break;
                case "PhantomJS":
                    _driver = new PhantomJSDriver();
                    break;
                default:
                    throw new ArgumentException("Unknown browser");
            }
        }


        //When
        [BeforeStep]
        public void WaitForLoad()
        {
            _driver?.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(60));
        }

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

        [When(@"I am not logged in")]
        public void WhenIAmNotLoggedIn()
        {
            SatisfyLoginCondition(false);
        }

        [When(@"I am logged in")]
        public void WhenIAmLoggedIn()
        {
            SatisfyLoginCondition(true);
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

        //Then
        [Then(@"the page title should start with '(.*)'")]
        public void ThenThePageTitleShouldStartWith(string title)
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(60));
            wait.Until(d => d.Title.StartsWith(title, StringComparison.CurrentCultureIgnoreCase));
        }

        [Then(@"the page url should be exactly '(.*)'")]
        public void ThenThePageUrlShouldBeExactly(string url)
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(60));
            wait.Until(d => d.Url.Equals(url, StringComparison.Ordinal));
        }

        [AfterScenario]
        public void Cleanup()
        {
            _driver?.Quit();
        }
    }
}
