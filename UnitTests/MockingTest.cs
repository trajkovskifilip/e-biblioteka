using System;
using System.Collections.Generic;
using E_biblioteka.Models;
using E_biblioteka.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTests
{
    [TestClass]
    public class MockingTest
    {
        private static Mock<ICartService> cartServiceMock;

        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            cartServiceMock = new Mock<ICartService>();
        }

        [TestMethod]
        public void TestMethod1()
        {
            List<Book> books = new List<Book>(TestData.Books);
            List<Book> orderedBooks = new List<Book>();
            orderedBooks.Add(books.Find(b => b.BookId == 1));
            orderedBooks.Add(books.Find(b => b.BookId == 3));
            cartServiceMock.Setup(c => c.Books()).Returns(orderedBooks);
            cartServiceMock.Setup(c => c.Total()).Returns(orderedBooks.Count * 10);
            Assert.AreEqual(20, cartServiceMock.Object.Total());
        }

        [TestMethod]
        public void TestMethod2()
        {
            List<Book> orderedBooks = new List<Book>();
            cartServiceMock.Setup(c => c.Books()).Returns(orderedBooks);
            cartServiceMock.Setup(c => c.Total()).Returns(orderedBooks.Count * 10);
            Assert.AreEqual(0, cartServiceMock.Object.Total());
        }
    }
}
