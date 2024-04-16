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
    [Route("{id:guid}")]
    public IActionResult Patch([FromBody] Books books, [FromRoute] Guid id)
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
    [Route("/book/{id}")]
    public IActionResult Delete([FromRoute] Guid id)
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