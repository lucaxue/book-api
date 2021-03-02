﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;


[ApiController]
[Route("Books")]
public class BookController : ControllerBase
{
    private readonly IRepository<Book> _bookRepository;

    public BookController(IRepository<Book> bookRepository)
    {
        _bookRepository = bookRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(string search = " ", int limit = -1, int page = -1)
    {
        try
        {
            if (search != " ")
            {
                var searchBooks = await _bookRepository.Search(search.ToLower());
                return Ok(searchBooks);
            }
            else if(page >= 0 && limit >=0)
            {
                var limitedPagedBooks = await _bookRepository.Limit(limit, (page-1) *limit);
                return Ok(limitedPagedBooks);
            }
            else if (limit >= 0)
            {
                var limitedBooks = await _bookRepository.Limit(limit, 0);
                return Ok(limitedBooks);
            }
            else
            {
                var allBooks = await _bookRepository.GetAll();
                return Ok(allBooks);
            }

        }
        catch (Exception)
        {
            return NotFound("There are no books.");
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
            return StatusCode(418);
            //I'm a teapot error
        }
    }

    [HttpDelete("{id}")]
    public void Delete(long id)
    {
        _bookRepository.Delete(id);

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
            return BadRequest($"Can't update a book that doesn't exist, Book of ID {id}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] Book book)
    {
        try
        {
            var insertedBook = await _bookRepository.Insert(book);
            return Created($"/books/{insertedBook.Id}", insertedBook);
        }
        catch (Exception)
        {
            return BadRequest($"Sorry can't insert new book.");
        }
    }
}

