using BookShelf.Core.Entities;
using BookShelf.Core.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace BookShelf.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly DeleteUser _deleteUser;
    private readonly EditUser _editUser;
    private readonly ViewUserDetails _viewUserDetails;
    private readonly AddUser _addUser;

    public UserController(DeleteUser userService, EditUser editUser, 
        ViewUserDetails viewUserDetails, AddUser addUser)
    {
        _deleteUser = userService;
        _editUser = editUser;
        _viewUserDetails = viewUserDetails;
        _addUser = addUser;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers([FromQuery] int? userId, [FromQuery] string? name, [FromQuery] string? contactNumber)
    {
        var users = await _viewUserDetails.Execute(userId, name, contactNumber);

        if (users == null || !users.Any())
        {
            return NotFound("No users found matching the criteria.");
        }

        return Ok(users);
    }

    //[HttpGet("{id}")]
    //public async Task<IActionResult> GetUser(int id)
    //{
    //    var user = await _userService.GetByIdAsync(id);
    //    if (user == null)
    //    {
    //        return NotFound();
    //    }
    //    return Ok(user);
    //}

    [HttpPost]
    public async Task<IActionResult> AddUser(User user)
    {
        await _addUser.Execute(user);
        return CreatedAtAction(nameof(GetUsers), new { id = user.Id }, user);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> EditUser(int id, User updatedUser)
    {
        if (id != updatedUser.Id)
        {
            return BadRequest("User ID mismatch.");
        }

        await _editUser.Execute(id, updatedUser);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        await _deleteUser.Execute(id);
        return Ok();
    }
}
