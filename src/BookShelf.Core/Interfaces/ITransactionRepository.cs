using BookShelf.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelf.Core.Interfaces;

public interface ITransactionRepository
{
    Task<Transaction> GetByIdAsync(int id);
    Task<List<Transaction>> GetActiveTransactionsByUserAsync(int userId);
    Task<List<Transaction>> GetLateTransactionsAsync();
    Task AddAsync(Transaction transaction);
    Task UpdateAsync(Transaction transaction);
    Task<List<Transaction>> GetActiveTransactionsByBookAsync(int bookId);
    Task<IEnumerable<Transaction>> GetAllAsync();
}