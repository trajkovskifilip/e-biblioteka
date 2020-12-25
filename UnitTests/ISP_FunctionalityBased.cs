using System;
using System.Web.Mvc;
using E_biblioteka.Controllers;
using E_biblioteka.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class ISP_FunctionalityBased
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
        public void TestBaseCase()
        {
            ViewResult result = controller.Index(1, null, "Girl", null) as ViewResult;
            BooksViewModel model = result.Model as BooksViewModel;
            Assert.AreEqual(3, model.Books.Count);
            Assert.AreEqual("Gone Girl", model.Books[0].Name);
            Assert.AreEqual("The Girl on the Train", model.Books[1].Name);
            Assert.AreEqual("Girl with a Pearl Earring", model.Books[2].Name);
        }

        [TestMethod]
        public void TestA1()
        {
            ViewResult result = controller.Index(1, null, null, "Drama") as ViewResult;
            BooksViewModel model = result.Model as BooksViewModel;
            Assert.AreEqual(0, model.Books.Count);
        }

        [TestMethod]
        public void TestA2()
        {
            ViewResult result = controller.Index(1, null, null, "Historical Fiction") as ViewResult;
            BooksViewModel model = result.Model as BooksViewModel;
            Assert.AreEqual(1, model.Books.Count);
            Assert.AreEqual("Girl with a Pearl Earring", model.Books[0].Name);
        }

        [TestMethod]
        public void TestB1()
        {
            ViewResult result = controller.Index(1, "year-descending", "Girl", "Thriller") as ViewResult;
            BooksViewModel model = result.Model as BooksViewModel;
            Assert.AreEqual(2, model.Books.Count);
            Assert.AreEqual("The Girl on the Train", model.Books[0].Name);
            Assert.AreEqual(2015, model.Books[0].Year);
            Assert.AreEqual("Gone Girl", model.Books[1].Name);
            Assert.AreEqual(2014, model.Books[1].Year);
        }
    }
}
