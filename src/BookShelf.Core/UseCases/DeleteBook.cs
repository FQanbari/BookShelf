using BookShelf.Core.Interfaces;

namespace BookShelf.Core.UseCases;

public class DeleteBook
{
    private readonly IBookRepository _bookRepository;
    private readonly ITransactionRepository _transactionRepository;

    public DeleteBook(IBookRepository bookRepository, ITransactionRepository transactionRepository)
    {
        _bookRepository = bookRepository;
        _transactionRepository = transactionRepository;
    }

    public async Task Execute(int bookId)
    {
        var book = await _bookRepository.GetByIdAsync(bookId);
        if (book == null)
            throw new ArgumentException("Book not found");

        var activeBorrowings = await _transactionRepository.GetActiveTransactionsByBookAsync(bookId);
        if (activeBorrowings.Any())
            throw new InvalidOperationException("Cannot delete a book that is currently borrowed");

        await _bookRepository.DeleteAsync(bookId);
    }
}
