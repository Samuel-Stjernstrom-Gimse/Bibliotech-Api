namespace Bibliotech_Api.Models;

public record Book( string Title, string Author,int Year)
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = Title;
    public string Author { get; set; } = Author;
    public int Year { get; set; } = Year;
}