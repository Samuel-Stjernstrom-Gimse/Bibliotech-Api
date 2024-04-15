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
    [Route("/users")]
    public IActionResult PostUser([FromBody] Users user)
    {
        context.users.Add(user);
        context.SaveChanges();
        
        return Ok(user);
    }
    
    [HttpPatch]
    [Route("/users/{id:int}")]
    public IActionResult PatchUser([FromBody] Users user, [FromRoute] int id)
    {
        var existingUser = context.users.FirstOrDefault(u => u.id == id);

        if (existingUser == null)
        {
            return NotFound();
        }

        existingUser.username = user.username;
        existingUser.email = user.email;
        existingUser.password = user.password;

        context.SaveChanges();
    
        return Ok(existingUser);
    }

    [HttpDelete]
    [Route("/users/{id:int}")]
    public IActionResult DeleteUser([FromRoute] int id)
    {
        var user = context.users.FirstOrDefault(u => u.id == id);

        if (user == null)
        {
            return NotFound();
        }

        context.users.Remove(user);
        context.SaveChanges();
    
        return Ok();
    }
}

