using BookShelf.Core.Entities;
using BookShelf.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookShelf.Infrastructure.Data;

public class TransactionRepository : ITransactionRepository
{
    private readonly ApplicationDbContext _context;

    public TransactionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Transaction> GetByIdAsync(int id)
    {
        return await _context.Transactions
            .Include(t => t.User) 
            .Include(t => t.Book)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<List<Transaction>> GetActiveTransactionsByUserAsync(int userId)
    {
        return await _context.Transactions
            .Where(t => t.UserId == userId && t.ReturnedDate == null)
            .ToListAsync();
    }

    public async Task<List<Transaction>> GetLateTransactionsAsync()
    {
        var today = DateTime.UtcNow;
        return await _context.Transactions
            .Include(t => t.Book)
            .Include(t => t.User)
            .Where(t => t.BorrowedDate.AddDays(14) < today && t.ReturnedDate == null)
            .ToListAsync();
    }

    public async Task AddAsync(Transaction transaction)
    {
        await _context.Transactions.AddAsync(transaction);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Transaction transaction)
    {
        _context.Transactions.Update(transaction);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Transaction>> GetActiveTransactionsByBookAsync(int bookId)
    {
        return await _context.Transactions
            .Where(t => t.BookId == bookId && t.ReturnedDate == null)
            .ToListAsync();
    }

    public async Task<IEnumerable<Transaction>> GetAllAsync()
    {
        return await _context.Transactions
            .Include(t => t.User) 
            .Include(t => t.Book) 
            .ToListAsync();
    }
}
