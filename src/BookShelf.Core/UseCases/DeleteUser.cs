using BookShelf.Core.Interfaces;

namespace BookShelf.Core.UseCases;

public class DeleteUser
{
    private readonly IUserRepository _userRepository;
    private readonly ITransactionRepository _transactionRepository;

    public DeleteUser(IUserRepository userRepository, ITransactionRepository transactionRepository)
    {
        _userRepository = userRepository;
        _transactionRepository = transactionRepository;
    }

    public async Task Execute(int userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
            throw new ArgumentException("User not found");

        var activeTransactions = await _transactionRepository.GetActiveTransactionsByUserAsync(userId);
        if (activeTransactions.Any())
            throw new InvalidOperationException("Cannot delete a user with active transactions");

        await _userRepository.DeleteAsync(userId);
    }
}
