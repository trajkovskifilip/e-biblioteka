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
        public void Test2()
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
        public void Test3()
        {
            _driver.Navigate().GoToUrl("https://localhost:44305/");
            _driver.FindElement(By.LinkText("Find something to read")).Click();
            _driver.FindElement(By.Id("search")).Click();
            _driver.FindElement(By.Id("search")).SendKeys("Harry Potter");
            _driver.FindElement(By.Id("search")).SendKeys(Keys.Enter);
            String real_title = _driver.FindElement(By.ClassName("card-title")).Text;
            String title = "Harry Potter and the Sorcerer's Stone";
            Assert.AreEqual(title, real_title);

        }
        [TestMethod]
        public void Test4()
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
        [TestMethod]
        public void Test5()
        {
            _driver.Navigate().GoToUrl("https://localhost:44305/");
            _driver.FindElement(By.Id("loginLink")).Click();
            _driver.FindElement(By.Id("UserName")).SendKeys("admin@library.com");
            _driver.FindElement(By.Id("Password")).SendKeys("Password1!");
            _driver.FindElement(By.CssSelector(".btn")).Click();
            _driver.FindElement(By.LinkText("Books")).Click();
            _driver.FindElement(By.LinkText("Edit")).Click();
            _driver.FindElement(By.Id("Book_InStock")).Clear();
            _driver.FindElement(By.Id("Book_InStock")).SendKeys("3");
            _driver.FindElement(By.CssSelector(".btn-success")).Click();
            _driver.FindElement(By.CssSelector(".card:nth-child(2) .btn-danger")).Click();
            _driver.FindElement(By.CssSelector(".btn-danger")).Click();

        }
        [TestMethod]
        public void Test6()
        {
            _driver.Navigate().GoToUrl("https://localhost:44305/");
            _driver.FindElement(By.Id("loginLink")).Click();
            _driver.FindElement(By.Id("UserName")).SendKeys("admin@library.com");
            _driver.FindElement(By.Id("Password")).SendKeys("Password1!");
            _driver.FindElement(By.CssSelector(".btn")).Click();
            _driver.FindElement(By.LinkText("Forum")).Click();
            _driver.FindElement(By.LinkText("Edit")).Click();
            _driver.FindElement(By.Id("Content")).Clear();
            _driver.FindElement(By.Id("Content")).SendKeys("asdasdadkklj");
            _driver.FindElement(By.CssSelector(".btn")).Click();
        }
        [TestMethod]
        public void Test7()
        {
            _driver.Navigate().GoToUrl("https://localhost:44305/");
            _driver.FindElement(By.Id("loginLink")).Click();
            _driver.FindElement(By.Id("UserName")).Click();
            _driver.FindElement(By.CssSelector(".form-group:nth-child(5) > .col-md-10")).Click();
            _driver.FindElement(By.CssSelector(".btn")).Click();
            String real_password_error = _driver.FindElement(By.Id("Password-error")).Text;
            String real_username_error = _driver.FindElement(By.Id("UserName-error")).Text;
            String password_error = "The Password field is required.";
            String username_error = "The Username field is required.";
            Assert.AreEqual(password_error, real_password_error);
            Assert.AreEqual(username_error, real_username_error);
        }
    }
}
