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
        readonly Book _bookUpdate;
        readonly Book _bookToStore;
        readonly Book _bookStored;

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

            _bookUpdate = new Book
            {
                Id = 56,
                Title = "Testing is Essential",
                Author = "Luca"
            };

            _bookToStore = new Book
            {
                Title = "Do you like testing",
                Author = "Yes"
            };

            _bookStored = new Book
            {
                Id = 57,
                Title = "Do you like testing",
                Author = "Yes"
            };

            _repository = Substitute.For<IRepository<Book>>();
            _controller = new BookController(_repository);
        }

        [Fact]
        public async Task Index_NoArguments_ReturnAllBooks()
        {
            _repository.Search("", 100, 1).Returns(x => _books);

            var result = await _controller.Index();

            var statusCode = ((OkObjectResult)result).StatusCode;
            var books = ((OkObjectResult)result).Value as List<Book>;

            statusCode.Should().Be(200);
            books.Should().BeEquivalentTo(_books);
        }

        [Fact]
        public async Task Index_WithSearchQuery_ReturnsCorrectBooks()
        {
            _repository.Search("test", 100, 1).Returns(x => new List<Book>() { _books[1], _books[2] });

            var result = await _controller.Index("test");

            var statusCode = ((OkObjectResult)result).StatusCode;
            var books = ((OkObjectResult)result).Value as List<Book>;

            statusCode.Should().Be(200);
            books.Should().BeEquivalentTo(new List<Book>() { _books[1], _books[2] });
        }

        [Fact]
        public async Task Index_WithPageLimit_ReturnsCorrectLimitedBooks()
        {
            _repository.Search("", 2, 1).Returns(x => new List<Book>() { _books[0], _books[1] });

            var result = await _controller.Index("", 2);

            var statusCode = ((OkObjectResult)result).StatusCode;
            var books = ((OkObjectResult)result).Value as List<Book>;

            statusCode.Should().Be(200);
            books.Should().BeEquivalentTo(new List<Book>() { _books[0], _books[1] });
        }

        [Fact]
        public async Task Index_WithPageLimitAndPageNumber_ReturnsCorrectPageOfLimitedBooks()
        {
            _repository.Search("", 2, 2).Returns(x => new List<Book>() { _books[2], _books[3] });

            var result = await _controller.Index("", 2, 2);

            var statusCode = ((OkObjectResult)result).StatusCode;
            var books = ((OkObjectResult)result).Value as List<Book>;

            statusCode.Should().Be(200);
            books.Should().BeEquivalentTo(new List<Book>() { _books[2], _books[3] });
        }

        [Fact]
        public async Task Show_WithId_ReturnsCorrectBook()
        {
            _repository.Find(56).Returns(x => _books[2]);

            var result = await _controller.Show(56);

            var statusCode = ((OkObjectResult)result).StatusCode;
            var book = ((OkObjectResult)result).Value as Book;

            statusCode.Should().Be(200);
            book.Should().BeEquivalentTo(_books[2]);
        }

        [Fact]
        public async Task Store_WithBookToStore_ReturnsBookStored()
        {
            _repository.Create(_bookToStore).Returns(x => _bookStored);

            var result = await _controller.Store(_bookToStore);

            var statusCode = ((ObjectResult)result).StatusCode;
            var storedBook = ((ObjectResult)result).Value as Book;

            statusCode.Should().Be(201);
            storedBook.Should().BeEquivalentTo(_bookStored);
        }

        [Fact]
        public async Task Update_WithIdAndBookToUpdate_ReturnsUpdatedBook()
        {
            _repository.Update(_bookUpdate).Returns(x => _bookUpdate);

            var result = await _controller.Update(56, _bookUpdate);

            var statusCode = ((OkObjectResult)result).StatusCode;
            var updatedBook = ((OkObjectResult)result).Value as Book;

            statusCode.Should().Be(200);
            updatedBook.Should().BeEquivalentTo(_bookUpdate);
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
