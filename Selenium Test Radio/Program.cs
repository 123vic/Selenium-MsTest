using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

IWebDriver driver = new ChromeDriver();

driver.Navigate().GoToUrl("http://127.0.0.1:5500/index.html");

var buttonElement = driver.FindElement(By.Id("toggle-records"));

buttonElement.Click();
