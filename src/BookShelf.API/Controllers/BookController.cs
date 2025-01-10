using BookShelf.Core.Entities;
using BookShelf.Core.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace BookShelf.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly AddBook _addBook;
    private readonly EditBook _editBook;
    private readonly DeleteBook _deleteBook;
    private readonly GetBooks _getBooks;

    public BookController(AddBook addBook, EditBook editBook, DeleteBook deleteBook, GetBooks getBooks)
    {
        _addBook = addBook;
        _editBook = editBook;
        _deleteBook = deleteBook;
        _getBooks = getBooks;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string? title, string? author, string? genre)
    {
        var books = await _getBooks.Execute(title, author, genre);
        return Ok(books);
    }


    [HttpPost]
    public async Task<IActionResult> Add(Book book)
    {
        await _addBook.Execute(book);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Edit(int id, Book updatedBook)
    {
        await _editBook.Execute(id, updatedBook);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _deleteBook.Execute(id);
        return Ok();
    }

}
