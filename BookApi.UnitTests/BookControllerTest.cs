using System;
using Xunit;
using NSubstitute;
using Moq;
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
          Id = 56,
          Title = "Testing is Hard",
          Author = "Christina"
        }
      };

      var bookRepository = Substitute.For<IRepository<Book>>();
      bookRepository.GetAll().Returns(x => _books);
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
    public void Delete_WhenCalledWithId_ReturnStatusCode200()
    {
      //act
      var statusCode = ((OkResult)_controller.Delete(2)).StatusCode;
      //assert
      statusCode.Should().Be(200);
    }


  }
}
