namespace Bibliotech_Api.Models;

public record Books
{
    public Guid id { get; set; }
    public string title { get; set; }
    public string author { get; set; }
    public int year { get; set; }
}