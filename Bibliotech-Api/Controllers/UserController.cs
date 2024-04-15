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
}