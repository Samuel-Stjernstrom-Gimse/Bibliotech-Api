

namespace Bibliotech_Api.Models;

public record Users
{
    public int id { get; set; }
    public string username { get; set; }
    public string email { get; set; }
    public string password { get; set; }
}
