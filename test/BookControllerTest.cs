using System;
using Xunit;
using NSubstitute;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;

namespace BookApi.UnitTests
{
  public class BookControllerTest
  {
    //fields
    readonly BookController _controller;
    readonly List<Book> _books;
    readonly Book _bookUpdate;
    readonly Book _bookToInsert;
    readonly Book _bookInserted;

    //constructor
    public BookControllerTest()
    {
      //arrange
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

      _bookToInsert = new Book
      {
        Title = "Do you like testing",
        Author = "Yes"
      };

      _bookInserted = new Book
      {
        Id = 57,
        Title = "Do you like testing",
        Author = "Yes"
      };

      var bookRepository = Substitute.For<IRepository<Book>>();

      bookRepository.GetAll().Returns(x => _books);
      bookRepository.Get(56).Returns(x => _books[2]);
      bookRepository.Search("", 100, 1).Returns(x => _books);
      bookRepository.Search("", 2, 1).Returns(x => new List<Book>() { _books[0], _books[1] });
      bookRepository.Search("", 2, 2).Returns(x => new List<Book>() { _books[2], _books[3] });
      bookRepository.Search("test", 100, 1).Returns(x => new List<Book>() { _books[1], _books[2] });
      bookRepository.Update(_bookUpdate).Returns(x => _bookUpdate);
      bookRepository.Insert(_bookToInsert).Returns(x => _bookInserted);

      _controller = new BookController(bookRepository);

      // Same as above but with Moq
      // var bookRepository = new Mock<IRepository<Book>>();
      // bookRepository.Setup(r => r.GetAll().Result).Returns(_books);
      // _controller = new BookController(bookRepository.Object);
    }

    [Fact]
    public async Task GetAll_WhenCalledWithNothingPassedIn_ReturnStatusCode200()
    {
      //act
      var result = await _controller.GetAll();
      var statusCode = ((OkObjectResult)result).StatusCode;
      //assert
      statusCode.Should().Be(200);
    }

    [Fact]
    public async Task GetAll_WhenCalledWithNothingPassedIn_ReturnsAllBooks()
    {
      //act
      var result = await _controller.GetAll();
      var books = ((OkObjectResult)result).Value as List<Book>;
      //assert
      books.Should().BeEquivalentTo(_books);
    }

    [Fact]
    public async Task GetAll_WhenCalledWithSearchQueryPassedIn_ReturnStatusCode200()
    {
      //act
      var result = await _controller.GetAll("test");
      var statusCode = ((OkObjectResult)result).StatusCode;
      //assert
      statusCode.Should().Be(200);
    }

    [Fact]
    public async Task GetAll_WhenCalledWithSearchQueryPassedIn_ReturnsCorrectBooks()
    {
      //act
      var result = await _controller.GetAll("test");
      var books = ((OkObjectResult)result).Value as List<Book>;
      //assert
      books.Should().BeEquivalentTo(new List<Book>() { _books[1], _books[2] });
    }

    [Fact]
    public async Task GetAll_WhenCalledWithLimitPassedIn_ReturnsStatusCode200()
    {
      //act
      var result = await _controller.GetAll("", 2);
      var statusCode = ((OkObjectResult)result).StatusCode;
      //assert
      statusCode.Should().Be(200);
    }

    [Fact]
    public async Task GetAll_WhenCalledWithLimitPassedIn_ReturnsCorrectLimitedBooks()
    {
      //act
      var result = await _controller.GetAll("", 2);
      var books = ((OkObjectResult)result).Value as List<Book>;
      //assert
      books.Should().BeEquivalentTo(new List<Book>() { _books[0], _books[1] });
    }

    [Fact]
    public async Task GetAll_WhenCalledWithLimitAndPagePassedIn_ReturnsStatusCode200()
    {
      //act
      var result = await _controller.GetAll("", 2, 2);
      var statusCode = ((OkObjectResult)result).StatusCode;
      //assert
      statusCode.Should().Be(200);
    }

    [Fact]
    public async Task GetAll_WhenCalledWithLimitAndPagePassedIn_ReturnsCorrectPageOfLimitedBooks()
    {
      //act
      var result = await _controller.GetAll("", 2, 2);
      var books = ((OkObjectResult)result).Value as List<Book>;
      //assert
      books.Should().BeEquivalentTo(new List<Book>() { _books[2], _books[3] });
    }

    [Fact]
    public async Task Get_WhenCalledWithIdPassedIn_ReturnsStatusCode200()
    {
      //act
      var result = await _controller.Get(56);
      var statusCode = ((OkObjectResult)result).StatusCode;
      //assert
      statusCode.Should().Be(200);
    }

    [Fact]
    public async Task Get_WhenCalledWithIdPassedIn_ReturnsCorrectBook()
    {
      //act
      var result = await _controller.Get(56);
      var book = ((OkObjectResult)result).Value as Book;
      //assert
      book.Should().BeEquivalentTo(_books[2]);
    }

    [Fact]
    public void Delete_WhenCalledWithId_ReturnStatusCode200()
    {
      //act
      var statusCode = ((OkResult)_controller.Delete(2)).StatusCode;
      //assert
      statusCode.Should().Be(200);
    }

    [Fact]
    public async Task Update_WhenCalledWithIdAndBookToUpdate_ReturnStatusCode200()
    {
      //act
      var result = await _controller.Update(56, _bookUpdate);
      var statusCode = ((OkObjectResult)result).StatusCode;
      //assert
      statusCode.Should().Be(200);
    }

    [Fact]
    public async Task Update_WhenCalledWithIdAndBookToUpdate_ReturnsUpdatedBook()
    {
      //act
      var result = await _controller.Update(56, _bookUpdate);
      var updatedBook = ((OkObjectResult)result).Value as Book;
      //assert
      updatedBook.Should().BeEquivalentTo(_bookUpdate);
    }

    [Fact]
    public async Task Insert_WhenCalledWithBookToInsert_ReturnStatusCode201()
    {
      //act
      var result = await _controller.Insert(_bookToInsert);
      var statusCode = ((ObjectResult)result).StatusCode;
      //assert
      statusCode.Should().Be(201);
    }

    [Fact]
    public async Task Insert_WhenCalledWithBookToInsert_ReturnsBookInserted()
    {
      //act
      var result = await _controller.Insert(_bookToInsert);
      var insertedBook = ((ObjectResult)result).Value as Book;
      //assert
      insertedBook.Should().BeEquivalentTo(_bookInserted);
    }
  }
}