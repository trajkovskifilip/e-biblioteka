using System;
using System.Web.Mvc;
using E_biblioteka.Controllers;
using E_biblioteka.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class LogicCoverage
    {
        private static InMemoryDatabase db;
        private static BooksController controller;

        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            db = new InMemoryDatabase();
            db.AddSet(TestData.Authors);
            db.AddSet(TestData.Books);
        }

        [TestInitialize]
        public void TestSetup()
        {
            controller = new BooksController(db);
        }

        [TestMethod]
        public void TestMethod1()
        {
            var actionResult = controller.DownVote(null);
            Assert.IsInstanceOfType(actionResult, typeof(HttpStatusCodeResult));
        }

        [TestMethod]
        public void TestMethod2()
        {
            var actionResult = controller.DownVote(123);
            Assert.IsInstanceOfType(actionResult, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void TestMethod3()
        {
            ViewResult result = controller.DownVote(1) as ViewResult;
            Book book = result.Model as Book;
            Assert.AreEqual(4.1, book.Rating, 0.00001);
        }

        [TestMethod]
        public void TestMethod4()
        {
            ViewResult result = controller.DownVote(2) as ViewResult;
            Book book = result.Model as Book;
            Assert.AreEqual(1, book.Rating);
        }
    }
}
