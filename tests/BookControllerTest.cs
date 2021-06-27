using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using NSubstitute;
using FluentAssertions;
using BookApi.Controllers;
using BookApi.Data;
using BookApi.Models;

namespace BookApi.UnitTests
{
    public class BookControllerTest
    {
        readonly BookController _controller;
        IRepository<Book> _repository;
        readonly List<Book> _books;

        public BookControllerTest()
        {
            _books = new List<Book> {
                new Book
                {
                    Id = 1,
                    Title = "Harry Potter",
                    Author = "JK Rowling"
                },
                new Book
                {
                    Id = 2,
                    Title = "Testing is Good",
                    Author = "Somebody"
                },
                new Book
                {
                    Id = 3,
                    Title = "What A Nice Book",
                    Author = "The Nice Author"
                },
                new Book
                {
                    Id = 4,
                    Title = "I Can C# With My Glasses",
                    Author = "C# Developer"
                },
                new Book
                {
                    Id = 56,
                    Title = "Testing is Hard",
                    Author = "Christina"
                }
            };

            _repository = Substitute.For<IRepository<Book>>();
            _controller = new BookController(_repository);
        }

        [Fact]
        public async Task Index_NoArguments_ReturnAllBooks()
        {
            _repository
                .Search("", 100, 1)
                .Returns(x => _books);

            var result = await _controller.Index();

            var statusCode = ((OkObjectResult)result).StatusCode;
            var books = ((OkObjectResult)result).Value as List<Book>;

            statusCode.Should().Be(200);
            books.Should().BeEquivalentTo(_books);
        }

        [Fact]
        public async Task Index_WithSearchQuery_ReturnsCorrectBooks()
        {
            var searchedBooks = new List<Book> { _books[1], _books[2] };
            _repository
                .Search("test", 100, 1)
                .Returns(x => searchedBooks);

            var result = await _controller.Index("test");

            var statusCode = ((OkObjectResult)result).StatusCode;
            var books = ((OkObjectResult)result).Value as List<Book>;

            statusCode.Should().Be(200);
            books.Should().BeEquivalentTo(searchedBooks);
        }

        [Fact]
        public async Task Index_WithPageLimit_ReturnsCorrectLimitedBooks()
        {
            var limitedBooks = new List<Book> { _books[0], _books[1] };
            _repository
                .Search("", 2, 1)
                .Returns(x => limitedBooks);

            var result = await _controller.Index("", 2);

            var statusCode = ((OkObjectResult)result).StatusCode;
            var books = ((OkObjectResult)result).Value as List<Book>;

            statusCode.Should().Be(200);
            books.Should().BeEquivalentTo(limitedBooks);
        }

        [Fact]
        public async Task Index_WithPageLimitAndPageNumber_ReturnsCorrectPageOfLimitedBooks()
        {
            var paginatedBooks = new List<Book> { _books[2], _books[3] };
            _repository
                .Search("", 2, 2)
                .Returns(x => paginatedBooks);

            var result = await _controller.Index("", 2, 2);

            var statusCode = ((OkObjectResult)result).StatusCode;
            var books = ((OkObjectResult)result).Value as List<Book>;

            statusCode.Should().Be(200);
            books.Should().BeEquivalentTo(paginatedBooks);
        }

        [Fact]
        public async Task Show_WithId_ReturnsCorrectBook()
        {
            _repository
                .Find(56)
                .Returns(x => _books[2]);

            var result = await _controller.Show(56);

            var statusCode = ((OkObjectResult)result).StatusCode;
            var book = ((OkObjectResult)result).Value as Book;

            statusCode.Should().Be(200);
            book.Should().BeEquivalentTo(_books[2]);
        }

        [Fact]
        public async Task Store_WithBookToStore_ReturnsBookStored()
        {
            var bookToStore = new Book
            {
                Title = "Do you like testing",
                Author = "Yes"
            };

            var bookStored = new Book
            {
                Id = 57,
                Title = "Do you like testing",
                Author = "Yes"
            };

            _repository
                .Create(bookToStore)
                .Returns(x => bookStored);

            var result = await _controller.Store(bookToStore);

            var statusCode = ((ObjectResult)result).StatusCode;
            var storedBook = ((ObjectResult)result).Value as Book;

            statusCode.Should().Be(201);
            storedBook.Should().BeEquivalentTo(bookStored);
        }

        [Fact]
        public async Task Update_WithIdAndBookToUpdate_ReturnsUpdatedBook()
        {
            var bookUpdate = new Book
            {
                Id = 56,
                Title = "Testing is Essential",
                Author = "Luca"
            };

            _repository
                .Update(bookUpdate)
                .Returns(x => bookUpdate);

            var result = await _controller.Update(56, bookUpdate);

            var statusCode = ((OkObjectResult)result).StatusCode;
            var updatedBook = ((OkObjectResult)result).Value as Book;

            statusCode.Should().Be(200);
            updatedBook.Should().BeEquivalentTo(bookUpdate);
        }


        [Fact]
        public void Destroy_WithId_ReturnStatusCode200()
        {
            _repository.Delete(2);

            var statusCode = ((OkResult)_controller.Destroy(2)).StatusCode;

            statusCode.Should().Be(200);
        }
    }
}
