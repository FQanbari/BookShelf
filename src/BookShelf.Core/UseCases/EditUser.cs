using BookShelf.Core.Entities;
using BookShelf.Core.Interfaces;

namespace BookShelf.Core.UseCases;

public class EditUser
{
    private readonly IUserRepository _userRepository;

    public EditUser(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Execute(int userId, User updatedUser)
    {
        var existingUser = await _userRepository.GetByIdAsync(userId);
        if (existingUser == null)
            throw new ArgumentException("User not found");

        if (string.IsNullOrEmpty(updatedUser.Name))
            throw new ArgumentException("User name is required");

        if (string.IsNullOrEmpty(updatedUser.PhoneNumber))
            throw new ArgumentException("Contact number is required");

        existingUser.Name = updatedUser.Name;
        existingUser.PhoneNumber = updatedUser.PhoneNumber;
        existingUser.MembershipDate = updatedUser.MembershipDate;

        await _userRepository.UpdateAsync(existingUser);
    }
}
