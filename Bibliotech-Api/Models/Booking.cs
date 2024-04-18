namespace Bibliotech_Api.Models;

public record Booking
{
    public int booking_id { get; set; }
    public int user_id { get; set; }
    public int book_id { get; set; }
    public DateTime booking_date { get; set; }
    public DateTime return_date { get; set; }
}