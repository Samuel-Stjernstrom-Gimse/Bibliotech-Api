using Bibliotech_Api.Access;
using Bibliotech_Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bibliotech_Api.Controllers;

[Route("book")]
public class BookController(LocalDbContext context) : Controller
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(context.books);
    }

    [HttpPost]
    public IActionResult Post([FromBody] Books book)
    {
        context.books.Add(book);
        context.SaveChanges();
        
        return Ok(book.title);
    }

    [HttpPatch]
    [Route("{id:int}")]
    public IActionResult Patch([FromBody] Books books, [FromRoute] int id)
    {
        var bookToPatch = context.books.FirstOrDefault(b => b.id == id);
        if (bookToPatch == null)
        {
            return NotFound($"{books.id} not found");
        }

        bookToPatch.title = books.title;
        bookToPatch.author = books.author;
        bookToPatch.year = books.year;

        context.SaveChanges();
        return Ok(bookToPatch);
    }

    [HttpDelete]
    [Route("{id:int}")]
    public IActionResult Delete([FromRoute] int id)
    {
        var bookToDelete = context.books.FirstOrDefault(b => b.id == id);
        
        if (bookToDelete == null)
        {
            return NotFound("not found" );
        }

        context.books.Remove(bookToDelete);
        context.SaveChanges();
        
        return Ok();
    }
}