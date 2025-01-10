using BookShelf.Core.Entities;
using BookShelf.Core.Interfaces;

namespace BookShelf.Core.UseCases;

public class ViewUserDetails
{
    private readonly IUserRepository _userRepository;

    public ViewUserDetails(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<User>> Execute(int? userId = null, string? name = null, string? contactNumber = null)
    {
        if (userId.HasValue)
        {
            var user = await _userRepository.GetByIdAsync(userId.Value);
            return user == null ? Enumerable.Empty<User>() : new List<User> { user };
        }

        var users = await _userRepository.GetAllAsync();

        if (!string.IsNullOrEmpty(name))
            users = users.Where(u => u.Name.Contains(name, StringComparison.OrdinalIgnoreCase));

        if (!string.IsNullOrEmpty(contactNumber))
            users = users.Where(u => u.PhoneNumber.Contains(contactNumber, StringComparison.OrdinalIgnoreCase));

        return users;
    }
}
