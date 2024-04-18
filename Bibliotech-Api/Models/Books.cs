using System.ComponentModel.DataAnnotations;

namespace Bibliotech_Api.Models;

public record Books
{
    [Key]
    public int book_id { get; set; }
    public string title { get; set; }
    public string author { get; set; }
    public int year { get; set; }
}