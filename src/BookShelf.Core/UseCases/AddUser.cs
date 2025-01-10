using BookShelf.Core.Entities;
using BookShelf.Core.Interfaces;

namespace BookShelf.Core.UseCases;

public class AddUser
{
    private readonly IUserRepository _userRepository;

    public AddUser(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Execute(User user)
    {
        if (string.IsNullOrEmpty(user.Name))
            throw new ArgumentException("Name is required");

        if (string.IsNullOrEmpty(user.Email))
            throw new ArgumentException("Email is required");

        if (string.IsNullOrEmpty(user.PhoneNumber))
            throw new ArgumentException("Phone is required");

        await _userRepository.AddAsync(user);
    }
}