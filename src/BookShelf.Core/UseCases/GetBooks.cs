using BookShelf.Core.Entities;
using BookShelf.Core.Interfaces;

namespace BookShelf.Core.UseCases;

public class GetBooks
{
    private readonly IBookRepository _bookRepository;

    public GetBooks(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<IEnumerable<Book>> Execute(string? title = null, string? author = null, string? genre = null)
    {
        var books = await _bookRepository.GetAllAsync();

        if (!string.IsNullOrEmpty(title))
            books = books.Where(b => b.Title.Contains(title, StringComparison.OrdinalIgnoreCase));

        if (!string.IsNullOrEmpty(author))
            books = books.Where(b => b.Author.Contains(author, StringComparison.OrdinalIgnoreCase));

        if (!string.IsNullOrEmpty(genre))
            books = books.Where(b => b.Genre.Contains(genre, StringComparison.OrdinalIgnoreCase));

        return books;
    }
}