using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Selenium___MsTest
{
    [TestClass]
    public class ToggleButtonTests
    {
        [TestMethod]
        public void ClickButton()
        {
            IWebDriver driver = new ChromeDriver();

            try
            {
                driver.Navigate().GoToUrl("http://127.0.0.1:5500/index.html");

                // Finder button
                var buttonElement = driver.FindElement(By.Id("toggle-records"));
                Assert.AreEqual("Show Records", buttonElement.Text);

                // Klik på button
                buttonElement.Click();

                Assert.AreEqual("Hide Records", buttonElement.Text);
            }
            finally
            {
                driver.Quit();
            }


        }

        [TestMethod]
        public void SearchForThriller_ShowsOnlyThriller()
        {
            IWebDriver driver = new ChromeDriver();

            try
            {
                driver.Navigate().GoToUrl("http://127.0.0.1:5500/index.html");

                // Skriv Thriller i titel-feltet
                var titleInput = driver.FindElement(By.Id("search-title"));
                titleInput.SendKeys("Thriller");

                // Klik Search
                driver.FindElement(By.Id("search-button")).Click();

                // Klik Show Records, så listen kommer frem
                driver.FindElement(By.Id("toggle-records")).Click();

                // Tjekker om der kun er 1 element i listen
                var items = driver.FindElements(By.TagName("li"));

                Assert.AreEqual(1, items.Count);
            }
            finally
            {
                driver.Quit();
            }
        }
    }
}
