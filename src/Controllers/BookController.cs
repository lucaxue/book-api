using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookApi.Models;
using BookApi.Data;

namespace BookApi.Controllers
{
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
        public async Task<IActionResult> Index(string search = "", int limit = 100, int page = 1)
        {
            try
            {
                var booksResult = await _bookRepository.Search(search, limit, page);
                return Ok(booksResult);
            }
            catch (Exception)
            {
                if (limit < 0 || page <= 0)
                {
                    return BadRequest(
                        $@"Sorry, the {(page <= 0 ? "page" : "limit")} entered is not valid.
                        Try entering a positive number."
                    );
                }
                return NotFound(
                    @"Sorry, could not get any books from the repository.
                    Please try another request."
                );
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Show(long id)
        {
            try
            {
                var returnedBook = await _bookRepository.Find(id);
                return Ok(returnedBook);
            }
            catch (Exception)
            {
                return NotFound(
                    $@"Sorry, book of id {id} cannot be fetched, since it does not exist.
                    Are you sure the id is correct?"
                );
            }
        }

        [HttpPost]
        public async Task<IActionResult> Store([FromBody] Book book)
        {
            try
            {
                var insertedBook = await _bookRepository.Create(book);
                return Created(
                    $"/books/{insertedBook.Id}",
                    insertedBook
                );
            }
            catch (Exception)
            {
                return BadRequest(
                    $@"Sorry, cannot insert new book.
                    Are you sure the book is valid?"
                );
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] Book book)
        {
            try
            {
                book.Id = id;
                var updatedBook = await _bookRepository.Update(book);
                return Ok(updatedBook);
            }
            catch (Exception)
            {
                return BadRequest(
                    $@"Sorry, book of id {id} cannot be updated, since it does not exist.
                    Are you sure the id is correct?"
                );
            }
        }


        [HttpDelete("{id}")]
        public IActionResult Destroy(long id)
        {
            try
            {
                _bookRepository.Delete(id);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest(
                    $@"Sorry, book of id {id} cannot be deleted, since it does not exit.
                    Are you sure the id is correct?"
                );
            }
        }
    }
}