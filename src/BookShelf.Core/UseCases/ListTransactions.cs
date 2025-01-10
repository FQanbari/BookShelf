using BookShelf.Core.Entities;
using BookShelf.Core.Interfaces;

namespace BookShelf.Core.UseCases;

public class ListTransactions
{
    private readonly ITransactionRepository _transactionRepository;

    public ListTransactions(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<IEnumerable<Transaction>> Execute(int? userId = null, int? bookId = null, bool? isActive = null)
    {
        var transactions = await _transactionRepository.GetAllAsync();

        if (userId.HasValue)
            transactions = transactions.Where(t => t.UserId == userId);

        if (bookId.HasValue)
            transactions = transactions.Where(t => t.BookId == bookId);

        if (isActive.HasValue)
            transactions = transactions.Where(t => t.ReturnedDate == null == isActive);

        return transactions;
    }
}
