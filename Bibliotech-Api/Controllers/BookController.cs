using System.Text.Json;
using Bibliotech_Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bibliotech_Api.Controllers;

[Route("book")]
public class BookController : Controller
{
    private const string Database = "database.json";
    private readonly List<Books> _books = LoadBooks();

    private static List<Books> LoadBooks()
    {
        if (!System.IO.File.Exists(Database)) return [];
        var databaseJson = System.IO.File.ReadAllText(Database);
        return JsonSerializer.Deserialize<List<Books>>(databaseJson) ?? [];
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
    public IActionResult Post([FromBody] Books books)
    {
        var newBook = new Books(books.title, books.author, books.year);
        
        _books.Add(newBook);
        SaveBooks();
        
        return Ok(newBook);
    }

    [HttpPatch]
    [Route("{id:guid}")]
    public IActionResult Patch([FromBody] Books books, [FromRoute] Guid id)
    {
        var bookToPatch = _books.FirstOrDefault(b => b.id == id);
        if (bookToPatch == null)
        {
            return NotFound($"{books.id} not found");
        }

        bookToPatch.title = books.title;
        bookToPatch.author = books.author;
        bookToPatch.year = books.year;
        
        SaveBooks();
        return Ok(bookToPatch);
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public IActionResult Delete([FromRoute] Guid id)
    {
        var bookToDelete = _books.FirstOrDefault(b => b.id == id);
        
        if (bookToDelete == null)
        {
            return NotFound("not found");
        }

        _books.Remove(bookToDelete);
        SaveBooks();
        
        return Ok();
    }
}