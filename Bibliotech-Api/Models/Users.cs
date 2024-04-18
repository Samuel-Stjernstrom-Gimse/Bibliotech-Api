using System.ComponentModel.DataAnnotations;

namespace Bibliotech_Api.Models;

public record Users
{
    [Key]
    public int user_id { get; set; }
    public string username { get; set; }
    public string email { get; set; }
    public string password { get; set; }
}
