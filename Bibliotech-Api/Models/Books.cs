namespace Bibliotech_Api.Models;

public record Books( string title, string author,int year)
{
    public Guid id { get; set; } = Guid.NewGuid();
    public string title { get; set; } = title;
    public string author { get; set; } = author;
    public int year { get; set; } = year;
}