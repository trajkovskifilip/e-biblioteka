using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;

namespace UnitTests
{
    [TestClass]
    public class WebDriverFactory
    {
        private readonly IWebDriver _driver;
        public WebDriverFactory()
        {
            _driver = new OpenQA.Selenium.Chrome.ChromeDriver();
        }
        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
            
        }
        [TestMethod]
        public void Test1()
        {
            _driver.Url = "https://localhost:44305/";
            String url_title = _driver.Title;
            String expected_title = "Home Page - My ASP.NET Application";
            Assert.AreEqual(expected_title, url_title);
        }

       
    }
}
