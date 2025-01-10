using BookShelf.Core.Entities;
using BookShelf.Core.Interfaces;

namespace BookShelf.Core.UseCases;

public class GetTransactionDetails
{
    private readonly ITransactionRepository _transactionRepository;

    public GetTransactionDetails(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<Transaction> Execute(int transactionId)
    {
        var transaction = await _transactionRepository.GetByIdAsync(transactionId);
        if (transaction == null)
            throw new ArgumentException("Transaction not found");

        return transaction;
    }
}