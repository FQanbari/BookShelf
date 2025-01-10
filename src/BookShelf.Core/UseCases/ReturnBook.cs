using BookShelf.Core.Interfaces;

namespace BookShelf.Core.UseCases;

public class ReturnBook
{
    private readonly ITransactionRepository _transactionRepository;

    public ReturnBook(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task Execute(int transactionId)
    {
        var transaction = await _transactionRepository.GetByIdAsync(transactionId);
        if (transaction == null)
            throw new ArgumentException("Transaction not found");

        if (transaction.ReturnedDate != null)
            throw new InvalidOperationException("This book has already been returned");

        transaction.ReturnedDate = DateTime.Now;

        // بررسی تأخیر و جریمه
        var dueDate = transaction.BorrowedDate.AddDays(14); // فرض: 14 روز مهلت امانت
        if (transaction.ReturnedDate > dueDate)
        {
            var daysLate = (transaction.ReturnedDate - dueDate).Value.Days;
            transaction.LateFee = daysLate * 1000; // فرض: 1000 واحد جریمه برای هر روز
        }

        await _transactionRepository.UpdateAsync(transaction);
    }
}
