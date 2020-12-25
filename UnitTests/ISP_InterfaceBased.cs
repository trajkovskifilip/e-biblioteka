using System;
using System.Web.Mvc;
using E_biblioteka.Controllers;
using E_biblioteka.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class ISP_InterfaceBased
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
            ViewResult result = controller.Index(1, null, null, null) as ViewResult;
            BooksViewModel model = result.Model as BooksViewModel;
            Assert.AreEqual(4, model.Books.Count);
            Assert.AreEqual("The Da Vinci Code", model.Books[0].Name);
            Assert.AreEqual("Gone Girl", model.Books[1].Name);
            Assert.AreEqual("The Girl on the Train", model.Books[2].Name);
            Assert.AreEqual("Girl with a Pearl Earring", model.Books[3].Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "PageNumber cannot be below 1.")]
        public void TestA1()
        {
            ViewResult result = controller.Index(0, null, null, null) as ViewResult;
        }

        [TestMethod]
        public void TestA3()
        {
            ViewResult result = controller.Index(2, null, null, null) as ViewResult;
            BooksViewModel model = result.Model as BooksViewModel;
            Assert.AreEqual(0, model.Books.Count);
        }

        [TestMethod]
        public void TestB2()
        {
            ViewResult result = controller.Index(1, "title-ascending", null, null) as ViewResult;
            BooksViewModel model = result.Model as BooksViewModel;
            Assert.AreEqual(4, model.Books.Count);
            Assert.AreEqual("Girl with a Pearl Earring", model.Books[0].Name);
            Assert.AreEqual("Gone Girl", model.Books[1].Name);
            Assert.AreEqual("The Da Vinci Code", model.Books[2].Name);
            Assert.AreEqual("The Girl on the Train", model.Books[3].Name);
        }

        [TestMethod]
        public void TestB3()
        {
            ViewResult result = controller.Index(1, "title-descending", null, null) as ViewResult;
            BooksViewModel model = result.Model as BooksViewModel;
            Assert.AreEqual(4, model.Books.Count);
            Assert.AreEqual("The Girl on the Train", model.Books[0].Name);
            Assert.AreEqual("The Da Vinci Code", model.Books[1].Name);
            Assert.AreEqual("Gone Girl", model.Books[2].Name);
            Assert.AreEqual("Girl with a Pearl Earring", model.Books[3].Name);
        }

        [TestMethod]
        public void TestB4()
        {
            ViewResult result = controller.Index(1, "year-ascending", null, null) as ViewResult;
            BooksViewModel model = result.Model as BooksViewModel;
            Assert.AreEqual(4, model.Books.Count);
            Assert.AreEqual(2, model.Books[0].BookId);
            Assert.AreEqual(1999, model.Books[0].Year);
            Assert.AreEqual(3, model.Books[1].BookId);
            Assert.AreEqual(2003, model.Books[1].Year);
            Assert.AreEqual(1, model.Books[2].BookId);
            Assert.AreEqual(2014, model.Books[2].Year);
            Assert.AreEqual(4, model.Books[3].BookId);
            Assert.AreEqual(2015, model.Books[3].Year);
        }

        [TestMethod]
        public void TestB5()
        {
            ViewResult result = controller.Index(1, "year-descending", null, null) as ViewResult;
            BooksViewModel model = result.Model as BooksViewModel;
            Assert.AreEqual(4, model.Books.Count);
            Assert.AreEqual(4, model.Books[0].BookId);
            Assert.AreEqual(2015, model.Books[0].Year);
            Assert.AreEqual(1, model.Books[1].BookId);
            Assert.AreEqual(2014, model.Books[1].Year);
            Assert.AreEqual(3, model.Books[2].BookId);
            Assert.AreEqual(2003, model.Books[2].Year);
            Assert.AreEqual(2, model.Books[3].BookId);
            Assert.AreEqual(1999, model.Books[3].Year);
        }

        [TestMethod]
        public void TestB6()
        {
            ViewResult result = controller.Index(1, "rating-descending", null, null) as ViewResult;
            BooksViewModel model = result.Model as BooksViewModel;
            Assert.AreEqual(4, model.Books.Count);
            Assert.AreEqual(3, model.Books[0].BookId);
            Assert.AreEqual(4.5, model.Books[0].Rating);
            Assert.AreEqual(1, model.Books[1].BookId);
            Assert.AreEqual(4.2, model.Books[1].Rating);
            Assert.AreEqual(4, model.Books[2].BookId);
            Assert.AreEqual(3.9, model.Books[2].Rating);
            Assert.AreEqual(2, model.Books[3].BookId);
            Assert.AreEqual(3.6, model.Books[3].Rating);
        }

        [TestMethod]
        public void TestC2()
        {
            ViewResult result = controller.Index(1, null, "Gone", null) as ViewResult;
            BooksViewModel model = result.Model as BooksViewModel;
            Assert.AreEqual(1, model.Books.Count);
            Assert.AreEqual("Gone Girl", model.Books[0].Name);
        }

        [TestMethod]
        public void TestD1()
        {
            ViewResult result = controller.Index(1, null, null, "Thriller") as ViewResult;
            BooksViewModel model = result.Model as BooksViewModel;
            Assert.AreEqual(3, model.Books.Count);
            Assert.AreEqual("The Da Vinci Code", model.Books[0].Name);
            Assert.AreEqual("Gone Girl", model.Books[1].Name);
            Assert.AreEqual("The Girl on the Train", model.Books[2].Name);
        }
    }
}
