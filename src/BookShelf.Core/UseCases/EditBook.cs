using BookShelf.Core.Entities;
using BookShelf.Core.Interfaces;

namespace BookShelf.Core.UseCases;

public class EditBook
{
    private readonly IBookRepository _bookRepository;

    public EditBook(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task Execute(int bookId, Book updatedBook)
    {
        var existingBook = await _bookRepository.GetByIdAsync(bookId);
        if (existingBook == null)
            throw new ArgumentException("Book not found");

        if (string.IsNullOrEmpty(updatedBook.Title))
            throw new ArgumentException("Book title is required");

        if (string.IsNullOrEmpty(updatedBook.Author))
            throw new ArgumentException("Book author is required");

        existingBook.Title = updatedBook.Title;
        existingBook.Author = updatedBook.Author;
        existingBook.Genre = updatedBook.Genre;
        existingBook.PublishedDate = updatedBook.PublishedDate;

        await _bookRepository.UpdateAsync(existingBook);
    }
}