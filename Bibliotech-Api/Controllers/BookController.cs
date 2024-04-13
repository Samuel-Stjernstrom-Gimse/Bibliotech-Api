using System.Text.Json;
using Bibliotech_Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bibliotech_Api.Controllers;

[Route("book")]
public class BookController : Controller
{
    private const string Database = "database.json";
    private readonly List<Book> _books = LoadBooks();

    private static List<Book> LoadBooks()
    {
        if (!System.IO.File.Exists(Database)) return [];
        var databaseJson = System.IO.File.ReadAllText(Database);
        return JsonSerializer.Deserialize<List<Book>>(databaseJson) ?? [];
    }

    private void SaveBooks()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        var json = JsonSerializer.Serialize(_books, options);
        System.IO.File.WriteAllText(Database, json);
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_books);
    }

    [HttpPost]
    public IActionResult Post([FromBody] Book book)
    {
        var newBook = new Book(book.Title, book.Author, book.Year);
        
        _books.Add(newBook);
        SaveBooks();
        
        return Ok(newBook);
    }

    [HttpPatch]
    [Route("{id:guid}")]
    public IActionResult Patch([FromBody] Book book, [FromRoute] Guid id)
    {
        var bookToPatch = _books.FirstOrDefault(b => b.Id == id);
        if (bookToPatch == null)
        {
            return NotFound($"{book.Id} not found");
        }

        bookToPatch.Title = book.Title;
        bookToPatch.Author = book.Author;
        bookToPatch.Year = book.Year;
        
        SaveBooks();
        return Ok(bookToPatch);
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public IActionResult Delete([FromRoute] Guid id)
    {
        var bookToDelete = _books.FirstOrDefault(b => b.Id == id);
        
        if (bookToDelete == null)
        {
            return NotFound("not found");
        }

        _books.Remove(bookToDelete);
        SaveBooks();
        
        return Ok();
    }
}