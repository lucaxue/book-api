using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;


[ApiController]
[Route("[controller]s")]
public class BookController : ControllerBase
{
  private readonly IRepository<Book> _bookRepository;

  public BookController(IRepository<Book> bookRepository)
  {
    _bookRepository = bookRepository;
  }

  [HttpGet]
  public async Task<IActionResult> GetAll(string search = "", int limit = 100, int page = 1)
  {
    //from controller base, checks if model state is valid
    Console.WriteLine(ModelState.IsValid);
    try
    {
      var booksResult = await _bookRepository.Search(search, limit, page);
      return Ok(booksResult);
      // Console.WriteLine("All books");
      // var allBooks = await _bookRepository.GetAll();
      // return Ok(allBooks);
    }
    catch (Exception)
    {
      if (limit < 0 || page <= 0)
      {
        return BadRequest($"Sorry, the {(page <= 0 ? "page" : "limit")} entered is not valid.\nTry entering a positive number.");
      }
      return NotFound("Sorry, there are no books in the database.\nTry posting some books.");
    }
  }


  [HttpGet("{id}")]
  public async Task<IActionResult> Get(long id)
  {
    try
    {
      var returnedBook = await _bookRepository.Get(id);
      return Ok(returnedBook);
    }
    catch (Exception)
    {
      return NotFound($"Sorry, book of id {id} cannot be fetched, since it does not exist.\nAre you sure the id is correct?");
    }
  }

  [HttpDelete("{id}")]
  public IActionResult Delete(long id)
  {
    try
    {
      _bookRepository.Delete(id);
      return Ok();
    }
    catch (Exception)
    {
      return BadRequest($"Sorry, book of id {id} cannot be deleted, since it does not exit.\nAre you sure the id is correct?");
    }
  }

  [HttpPut("{id}")]
  public async Task<IActionResult> Update(long id, [FromBody] Book book)
  {
    try
    {
      var updatedBook = await _bookRepository.Update(new Book { Id = id, Title = book.Title, Author = book.Author });
      return Ok(updatedBook);
    }
    catch (Exception)
    {
      return BadRequest($"Sorry, book of id {id} cannot be updated, since it does not exist.\nAre you sure the id is correct?");
    }
  }

  [HttpPost]
  public async Task<IActionResult> Insert([FromBody] Book book)
  {
    //from controller base, checks if model state is valid
    Console.WriteLine(ModelState.IsValid);
    try
    {
      var insertedBook = await _bookRepository.Insert(book);
      return Created($"/books/{insertedBook.Id}", insertedBook);
    }
    catch (Exception)
    {
      return BadRequest($"Sorry, cannot insert new book.\nAre you sure the book is valid?");
    }
  }
}
