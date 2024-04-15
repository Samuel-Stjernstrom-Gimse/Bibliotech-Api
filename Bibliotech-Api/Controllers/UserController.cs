using Bibliotech_Api.Access;
using Bibliotech_Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bibliotech_Api.Controllers;

[ApiController]
[Route("users")]
public class UserController(LocalDbContext context) : Controller
{
    [HttpGet]
    public IActionResult GetUsers()
    {
        return Ok(context.users);
    }

    [HttpPost]
    public async Task<IActionResult> PostUser([FromBody] Users user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        context.users.Add(user);
        await context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetUsers), new { id = user.id }, user);
    }
}