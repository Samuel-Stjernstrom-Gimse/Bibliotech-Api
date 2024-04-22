using System.Text;
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
    public IActionResult PostUser([FromBody] User user)
    {
        var password = user.Password;
        var bytes = Encoding.UTF8.GetBytes(password);
        var hashedPassword = Convert.ToBase64String(bytes);
        user.Password = hashedPassword;
        
        context.users.Add(user);
        context.SaveChanges();
        
        return Ok(user);
    }

    [HttpPatch]
    [Route("{id:int}")]
    public IActionResult PatchUser([FromBody] User user, [FromRoute] int id)
    {
        var existingUser = context.users.FirstOrDefault(u => u.Id == id);

        if (existingUser == null)
        {
            return NotFound();
        }

        existingUser.Username = user.Username;
        existingUser.Email = user.Email;
        existingUser.Password = user.Password;

        context.SaveChanges();

        return Ok(existingUser);
    }
    
    [HttpPatch]
    [Route("{id:int}/password")]
    public IActionResult PatchPassword([FromBody] string password, [FromRoute] int id)
    {
        var user = context.users.FirstOrDefault(u => u.Id == id);

        if (user == null)
        {
            return NotFound();
        }

        var bytes = Encoding.UTF8.GetBytes(password);
        var hashedPassword = Convert.ToBase64String(bytes);
        user.Password = hashedPassword;

        context.SaveChanges();

        return Ok(user);
    }
    
    [HttpPatch]
    [Route("{id:int}/email")]
    public IActionResult PatchEmail([FromBody] string email, [FromRoute] int id)
    {
        var user = context.users.FirstOrDefault(u => u.Id == id);

        if (user == null)
        {
            return NotFound();
        }

        user.Email = email;

        context.SaveChanges();

        return Ok(user);
    }
    
    [HttpDelete]
    [Route("{id:int}")]
    public IActionResult DeleteUser([FromRoute] int id)
    {
        var user = context.users.FirstOrDefault(u => u.Id == id);

        if (user == null)
        {
            return NotFound();
        }

        context.users.Remove(user);
        context.SaveChanges();

        return Ok();
    }
    
}