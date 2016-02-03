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
        private IWebDriver driver;

        [Given(@"I am using (.*)")]
        public void GivenIAmUsing(string browser)
        {
            switch (browser)
            {
                case "Firefox":
                    driver = new FirefoxDriver();
                    break;
                case "Chrome":
                    driver = new ChromeDriver();
                    break;
                case "IE":
                    driver = new InternetExplorerDriver();
                    break;
                case "PhantomJS":
                    driver = new PhantomJSDriver();
                    break;
                default:
                    throw new ArgumentException("Unknown browser");
            }
        }

        [When(@"I browse to '(.*)'")]
        public void WhenIBrowseTo(string url)
        {
            driver.Navigate().GoToUrl(url);
        }

        [When(@"I search for '(.*)'")]
        public void WhenISearchFor(string term)
        {
            var query = driver.FindElement(By.Name("q"));
            query.SendKeys(term);
            query.Submit();
        }

        [Then(@"the page title should start with '(.*)'")]
        public void ThenThePageTitleShouldStartWith(string title)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(d => d.Title.StartsWith(title, StringComparison.CurrentCultureIgnoreCase));
        }

        [AfterScenario]
        public void Cleanup()
        {
            driver?.Quit();
        }
    }
}
