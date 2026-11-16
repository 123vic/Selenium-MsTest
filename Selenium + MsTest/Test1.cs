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
        public void SearchForThriller()
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

        [TestMethod]
        public void Login()
        {
            IWebDriver driver = new ChromeDriver();

            try
            {
                driver.Navigate().GoToUrl("http://127.0.0.1:5500/index.html");

                // Skriv brugernavn og password
                driver.FindElement(By.Id("login-username")).SendKeys("admin");
                driver.FindElement(By.Id("login-password")).SendKeys("Admin123");

                // Klik Login
                driver.FindElement(By.Id("login-button")).Click();

                Thread.Sleep(1000);

                // Tjek at Logout-knappen er kommet frem
                var logoutButton = driver.FindElement(By.Id("logout-button"));
                Assert.IsTrue(logoutButton.Displayed);
            }
            finally
            {
                driver.Quit();
            }
        }

        [TestMethod]
        public void Login_WithWrongPassword()
        {
            IWebDriver driver = new ChromeDriver();

            try
            {
                driver.Navigate().GoToUrl("http://127.0.0.1:5500/index.html");

                driver.FindElement(By.Id("login-username")).SendKeys("admin");
                driver.FindElement(By.Id("login-password")).SendKeys("forkert");
                driver.FindElement(By.Id("login-button")).Click();

                Thread.Sleep(1000);

                // Fejlbesked vises, og man er ikke logget ind
                StringAssert.Contains(driver.PageSource, "Authentication failed");
                Assert.AreEqual(0, driver.FindElements(By.Id("logout-button")).Count);
            }
            finally
            {
                driver.Quit();
            }
        }

        [TestMethod]
        public void Logout()
        {
            IWebDriver driver = new ChromeDriver();

            try
            {
                driver.Navigate().GoToUrl("http://127.0.0.1:5500/index.html");

                driver.FindElement(By.Id("login-username")).SendKeys("admin");
                driver.FindElement(By.Id("login-password")).SendKeys("Admin123");
                driver.FindElement(By.Id("login-button")).Click();

                Thread.Sleep(1000);

                // Klik Logout
                driver.FindElement(By.Id("logout-button")).Click();

                // Login-formen er tilbage
                Assert.AreEqual(1, driver.FindElements(By.Id("login-button")).Count);
                Assert.AreEqual(0, driver.FindElements(By.Id("logout-button")).Count);
            }
            finally
            {
                driver.Quit();
            }
        }
        [TestMethod]
        public void AddRecord()
        {
            IWebDriver driver = new ChromeDriver();

            try
            {
                driver.Navigate().GoToUrl("http://127.0.0.1:5500/index.html");

                // Log ind som admin
                driver.FindElement(By.Id("login-username")).SendKeys("admin");
                driver.FindElement(By.Id("login-password")).SendKeys("Admin123");
                driver.FindElement(By.Id("login-button")).Click();
                Thread.Sleep(1000);

                // Udfyld Add-formularen
                driver.FindElement(By.Id("add-title")).SendKeys("Selenium Test Song");
                driver.FindElement(By.Id("add-artist")).SendKeys("Selenium Band");
                driver.FindElement(By.Id("add-duration")).SendKeys("200");
                driver.FindElement(By.Id("add-year")).SendKeys("2024");
                driver.FindElement(By.Id("add-button")).Click();
                Thread.Sleep(1000);

                // Vis listen og tjek at recorden er der
                driver.FindElement(By.Id("toggle-records")).Click();
                StringAssert.Contains(driver.PageSource, "Selenium Test Song");

                // Ryd op: slet den sidste record
                var deleteButtons = driver.FindElements(By.CssSelector("button[id^='delete-']"));
                deleteButtons[deleteButtons.Count - 1].Click();
                Thread.Sleep(1000);
            }
            finally
            {
                driver.Quit();
            }
        }

        [TestMethod]
        public void DeleteRecord()
        {
            IWebDriver driver = new ChromeDriver();

            try
            {
                driver.Navigate().GoToUrl("http://127.0.0.1:5500/index.html");

                // Log ind som admin
                driver.FindElement(By.Id("login-username")).SendKeys("admin");
                driver.FindElement(By.Id("login-password")).SendKeys("Admin123");
                driver.FindElement(By.Id("login-button")).Click();
                Thread.Sleep(1000);

                // Opret en record, som vi kan slette
                driver.FindElement(By.Id("add-title")).SendKeys("Delete Me");
                driver.FindElement(By.Id("add-artist")).SendKeys("Temp Band");
                driver.FindElement(By.Id("add-duration")).SendKeys("100");
                driver.FindElement(By.Id("add-year")).SendKeys("2024");
                driver.FindElement(By.Id("add-button")).Click();
                Thread.Sleep(1000);

                driver.FindElement(By.Id("toggle-records")).Click();
                StringAssert.Contains(driver.PageSource, "Delete Me");

                // Slet den sidste record
                var deleteButtons = driver.FindElements(By.CssSelector("button[id^='delete-']"));
                int before = deleteButtons.Count;
                deleteButtons[deleteButtons.Count - 1].Click();
                Thread.Sleep(1000);

                // Tjek at den er væk
                var after = driver.FindElements(By.CssSelector("button[id^='delete-']"));
                Assert.AreEqual(before - 1, after.Count);
                Assert.IsFalse(driver.PageSource.Contains("Delete Me"));
            }
            finally
            {
                driver.Quit();
            }
        }

        [TestMethod]
        public void UpdateRecord()
        {
            IWebDriver driver = new ChromeDriver();

            try
            {
                driver.Navigate().GoToUrl("http://127.0.0.1:5500/index.html");

                // Log ind som admin
                driver.FindElement(By.Id("login-username")).SendKeys("admin");
                driver.FindElement(By.Id("login-password")).SendKeys("Admin123");
                driver.FindElement(By.Id("login-button")).Click();
                Thread.Sleep(1000);

                // Opret en record, som vi kan rette
                driver.FindElement(By.Id("add-title")).SendKeys("Before Update");
                driver.FindElement(By.Id("add-artist")).SendKeys("Temp Band");
                driver.FindElement(By.Id("add-duration")).SendKeys("100");
                driver.FindElement(By.Id("add-year")).SendKeys("2024");
                driver.FindElement(By.Id("add-button")).Click();
                Thread.Sleep(1000);

                driver.FindElement(By.Id("toggle-records")).Click();

                // Klik Edit på den sidste record
                var editButtons = driver.FindElements(By.CssSelector("button[id^='edit-']"));
                editButtons[editButtons.Count - 1].Click();

                // Ret titlen
                var titleInput = driver.FindElement(By.Id("update-title"));
                titleInput.Clear();
                titleInput.SendKeys("After Update");
                driver.FindElement(By.Id("update-button")).Click();
                Thread.Sleep(1000);

                StringAssert.Contains(driver.PageSource, "After Update");
                Assert.IsFalse(driver.PageSource.Contains("Before Update"));

                // Ryd op: slet den sidste record
                var deleteButtons = driver.FindElements(By.CssSelector("button[id^='delete-']"));
                deleteButtons[deleteButtons.Count - 1].Click();
                Thread.Sleep(1000);
            }
            finally
            {
                driver.Quit();
            }
        }

    }
}
