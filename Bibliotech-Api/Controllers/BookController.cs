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
        return Ok(context.books.OrderBy(id => id));
    }

    [HttpPost]
    public IActionResult Post([FromBody] Book book)
    {
        context.books.Add(book);
        context.SaveChanges();
        
        return Ok(book);
    }

    [HttpPatch]
    [Route("{id:int}")]
    public IActionResult Patch([FromBody] Book book, [FromRoute] int id)
    {
        var bookToPatch = context.books.FirstOrDefault(b => b.Id == id);
        if (bookToPatch == null)
        {
            return NotFound($"{book.Id} not found");
        }

        bookToPatch.Title = book.Title;
        bookToPatch.Author = book.Author;
        bookToPatch.Year = book.Year;

        context.SaveChanges();
        return Ok(bookToPatch);
    }

    [HttpDelete]
    [Route("{id:int}")]
    public IActionResult Delete([FromRoute] int id)
    {
        var bookToDelete = context.books.FirstOrDefault(b => b.Id == id);
        
        if (bookToDelete == null)
        {
            return NotFound("not found" );
        }

        context.books.Remove(bookToDelete);
        context.SaveChanges();
        
        return Ok();
    }
}