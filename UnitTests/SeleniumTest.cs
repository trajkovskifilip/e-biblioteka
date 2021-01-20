using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;

namespace UnitTests
{
    [TestClass]
    public class SeleniumTest
    {
        private readonly IWebDriver _driver;
        public SeleniumTest()
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
            _driver.Navigate().GoToUrl("https://localhost:44305/");
            String url_title = _driver.Title;
            String expected_title = "Home Page - My ASP.NET Application";
            Assert.AreEqual(expected_title, url_title);
        }
        [TestMethod]
        public void Test2()
        {
            _driver.Navigate().GoToUrl("https://localhost:44305/");
            _driver.FindElement(By.Id("registerLink")).Click();
            _driver.FindElement(By.Id("UserName")).Click();
            _driver.FindElement(By.Id("UserName")).SendKeys("aida");
            _driver.FindElement(By.Id("Email")).SendKeys("aidamasovic9@gmail.com");
            _driver.FindElement(By.Id("Password")).SendKeys("aidam");
            _driver.FindElement(By.CssSelector(".form-group:nth-child(5)")).Click();
            String error = "Passwords must have 8-15 characters and must contain the following: upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)";
            String real_error = _driver.FindElement(By.Id("Password-error")).Text;
            Assert.AreEqual(error, real_error);

        }
        [TestMethod]
        public void Test3()
        {
            _driver.Navigate().GoToUrl("https://localhost:44305/");
            _driver.FindElement(By.Id("registerLink")).Click();
            _driver.FindElement(By.Id("UserName")).Click();
            _driver.FindElement(By.Id("UserName")).SendKeys("aida");
            _driver.FindElement(By.Id("Email")).SendKeys("aidamasovic9@gmail.com");
            _driver.FindElement(By.Id("Password")).SendKeys("AidaMasovic9!");
            _driver.FindElement(By.Id("ConfirmPassword")).SendKeys("AidaMasovic9@");
            _driver.FindElement(By.CssSelector(".form-group:nth-child(5)")).Click();
            String error = "The password and confirmation password do not match.";
            String real_error = _driver.FindElement(By.Id("ConfirmPassword-error")).Text;
            Assert.AreEqual(error, real_error);

        }
        [TestMethod]
        public void Test4()
        {
            _driver.Navigate().GoToUrl("https://localhost:44305/");
            _driver.FindElement(By.LinkText("Find something to read")).Click();
            _driver.FindElement(By.Id("search")).Click();
            _driver.FindElement(By.Id("search")).SendKeys("Harry Potter");
            _driver.FindElement(By.Id("search")).SendKeys(Keys.Enter);
            String real_title = _driver.FindElement(By.ClassName("card-title")).Text;
            String title = "Harry Potter and the Sorcerer's Stone";
            Assert.AreEqual(real_title, title);

        }
        [TestMethod]
        public void Test5()
        {
            _driver.Navigate().GoToUrl("https://localhost:44305/");
            _driver.FindElement(By.LinkText("Find something to read")).Click();
            _driver.FindElement(By.Id("order-list")).Click();
            _driver.FindElement(By.CssSelector(".col-sm-4")).Click();
            _driver.FindElement(By.Id("genres-list")).Click();
            {
                IWebElement dropdown = _driver.FindElement(By.Id("genres-list"));
                dropdown.FindElement(By.XPath("//option[. = 'Comedy']")).Click();
            }
            _driver.FindElement(By.CssSelector(".card:nth-child(1) > .card-img-top")).Click();
            String real_book = _driver.FindElement(By.CssSelector("div#contentHead > h1")).Text;
            String book = "V for Vendetta";
            Assert.AreEqual(book, real_book);
        }
    }
}
