using BookShelf.Core.Interfaces;

namespace BookShelf.Core.UseCases;

public class NotifyLateReturns
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly INotificationService _notificationService;

    public NotifyLateReturns(
        ITransactionRepository transactionRepository,
        INotificationService notificationService)
    {
        _transactionRepository = transactionRepository;
        _notificationService = notificationService;
    }

    public async Task Execute()
    {
        var lateTransactions = await _transactionRepository.GetLateTransactionsAsync();

        foreach (var transaction in lateTransactions)
        {
            var userMessage = $"Dear user, the book '{transaction.Book.Title}' is overdue. Please return it as soon as possible.";
            await _notificationService.SendAsync(transaction.User.Email, "Overdue Book Notification", userMessage);
        }
    }
}