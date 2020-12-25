using E_biblioteka.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    class TestData
    {
        private static Author DanBrown = new Author
        {
            AuthorId = 1,
            Name = "Dan Brown"
        };

        private static Author GillianFlynn = new Author
        {
            AuthorId = 2,
            Name = "Gillian Flynn"
        };

        private static Author TracyChevalier = new Author
        {
            AuthorId = 3,
            Name = "Tracy Chevalier"
        };

        private static Author PaulaHawkins = new Author
        {
            AuthorId = 4,
            Name = "Paula Hawkins"
        };

        private static Book GoneGirl = new Book
        {
            BookId = 1,
            Name = "Gone Girl",
            Genre = "Thriller",
            Year = 2014,
            Rating = 4.2,
            Author = GillianFlynn
        };

        private static Book GirlWithAPearlEarring = new Book
        {
            BookId = 2,
            Name = "Girl with a Pearl Earring",
            Genre = "Historical Fiction",
            Year = 1999,
            Rating = 1,
            Author = TracyChevalier
        };

        private static Book TheDaVinciCode = new Book
        {
            BookId = 3,
            Name = "The Da Vinci Code",
            Genre = "Thriller",
            Year = 2003,
            Rating = 4.5,
            Author = DanBrown
        };

        private static Book TheGirlOnTheTrain = new Book
        {
            BookId = 4,
            Name = "The Girl on the Train",
            Genre = "Thriller",
            Year = 2015,
            Rating = 3.9,
            Author = PaulaHawkins
        };

        public static IQueryable<Author> Authors
        {
            get
            {
                var authors = new List<Author>();
                authors.Add(DanBrown);
                authors.Add(GillianFlynn);
                authors.Add(TracyChevalier);
                authors.Add(PaulaHawkins);
                return authors.AsQueryable();
            }
        }

        public static IQueryable<Book> Books
        {
            get
            {
                var books = new List<Book>();
                books.Add(GoneGirl);
                books.Add(GirlWithAPearlEarring);
                books.Add(TheDaVinciCode);
                books.Add(TheGirlOnTheTrain);
                return books.AsQueryable();
            }
        }
    }
}
