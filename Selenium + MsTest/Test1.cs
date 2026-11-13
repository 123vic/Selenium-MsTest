using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Selenium___MsTest
{
    [TestClass]
    public class ToggleButtonTests
    {
        [TestMethod]
        public void ClickToggleButton_ChangesTextToHideRecords()
        {
            IWebDriver driver = new ChromeDriver();

            try
            {
                driver.Navigate().GoToUrl("http://127.0.0.1:5500/index.html");

                var buttonElement = driver.FindElement(By.Id("toggle-records"));
                Assert.AreEqual("Show Records", buttonElement.Text);

                buttonElement.Click();

                Assert.AreEqual("Hide Records", buttonElement.Text);
            }
            finally
            {
                driver.Quit();
            }
        }
    }
}
