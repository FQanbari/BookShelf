using BookShelf.Core.Entities;
using BookShelf.Core.Interfaces;

namespace BookShelf.Core.UseCases;

public class BorrowBook
{
    private readonly IBookRepository _bookRepository;
    private readonly IUserRepository _userRepository;
    private readonly ITransactionRepository _transactionRepository;

    public BorrowBook(
        IBookRepository bookRepository,
        IUserRepository userRepository,
        ITransactionRepository transactionRepository)
    {
        _bookRepository = bookRepository;
        _userRepository = userRepository;
        _transactionRepository = transactionRepository;
    }

    public async Task Execute(int bookId, int userId)
    {
        var book = await _bookRepository.GetByIdAsync(bookId);
        if (book == null)
            throw new ArgumentException("Book not found");

        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
            throw new ArgumentException("User not found");

        var activeTransactions = await _transactionRepository.GetActiveTransactionsByUserAsync(userId);
        if (activeTransactions.Count >= 3)
            throw new InvalidOperationException("User has reached the maximum allowed active borrowings");

        if (activeTransactions.Any(x => x.BookId == bookId))
            throw new InvalidOperationException("User borrowed this book");

        var transaction = new Transaction
        {
            BookId = bookId,
            UserId = userId,
            BorrowedDate = DateTime.Now
        };

        await _transactionRepository.AddAsync(transaction);
    }
}