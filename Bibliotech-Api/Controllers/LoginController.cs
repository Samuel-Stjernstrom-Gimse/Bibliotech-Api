using System.Text;
using Bibliotech_Api.Access;
using Bibliotech_Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bibliotech_Api.Controllers;

public class LoginController(LocalDbContext context) : Controller
{
    [HttpPost]
    [Route("login")]
    public IActionResult Login([FromBody] Login user)
    {
        var existingUser = context.users.FirstOrDefault(u => u.Username == user.Username);

        if (existingUser == null)
        {
            return NotFound("User not found");
        }

        var password = user.Password;

        if (password != existingUser.Password)
        {
            return Unauthorized("Invalid password");
        }

        return Ok(existingUser);
    }
}