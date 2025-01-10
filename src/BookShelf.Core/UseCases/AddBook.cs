using BookShelf.Core.Entities;
using BookShelf.Core.Interfaces;

namespace BookShelf.Core.UseCases;

public class AddBook
{
    private readonly IBookRepository _bookRepository;

    public AddBook(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task Execute(Book book)
    {
        if (string.IsNullOrEmpty(book.Title))
            throw new ArgumentException("Title is required");

        if (string.IsNullOrEmpty(book.Author))
            throw new ArgumentException("Book author is required");

        await _bookRepository.AddAsync(book);
    }
}