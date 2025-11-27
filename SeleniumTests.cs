using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace SeleniumTutorial
{
    public class SeleniumTests
    {
        private static IWebDriver driver;
        public static string gridURL = "@hub.lambdatest.com/wd/hub";
        private static readonly string LT_USERNAME = Environment.GetEnvironmentVariable("LT_USERNAME");
        private static readonly string LT_ACCESS_KEY = Environment.GetEnvironmentVariable("LT_ACCESS_KEY");

        [SetUp]
        public void Setup()
        {
            ChromeOptions capabilities = new ChromeOptions();
            capabilities.BrowserVersion = "126";
            Dictionary<string, object> ltOptions = new Dictionary<string, object>();
            ltOptions.Add("username", LT_USERNAME);
            ltOptions.Add("accessKey", LT_ACCESS_KEY);
            ltOptions.Add("platformName", "Windows 10"); 
            ltOptions.Add("build", "Selenium CSharp");
            ltOptions.Add("project", "SeleniumTutorial");
            ltOptions.Add("w3c", true);
            ltOptions.Add("plugin", "c#-nunit");
            capabilities.AddAdditionalOption("LT:Options", ltOptions);
            driver = new RemoteWebDriver(new Uri($"https://{LT_USERNAME}:{LT_ACCESS_KEY}{gridURL}"), capabilities);
        }


        [Test]
        public void ValidateTheMessageIsDisplayed()
        {
            driver.Navigate().GoToUrl("https://www.lambdatest.com/selenium-playground/simple-form-demo");
            driver.FindElement(By.Id("user-message")).SendKeys("LambdaTest rules");
            driver.FindElement(By.Id("showInput")).Click();
            Assert.IsTrue(driver.FindElement(By.Id("message")).Text.Equals("LambdaTest rules"), "The expected message was not displayed.");
        }


        [TearDown]
        public void TearDown()
        {
            Thread.Sleep(3000);
            driver.Quit();
        }
    }
}