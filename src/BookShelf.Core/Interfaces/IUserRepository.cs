using BookShelf.Core.Entities;

namespace BookShelf.Core.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User> GetByIdAsync(int id);
    Task AddAsync(User book);
    Task UpdateAsync(User book);
    Task DeleteAsync(int id);
}
