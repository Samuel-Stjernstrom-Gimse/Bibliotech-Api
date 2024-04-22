namespace Bibliotech_Api.Models;

public record Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public int Year { get; set; }
}

public record User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}

public record Login
{
    public string Username { get; set; }
    public string Password { get; set; }
}

public record Booking
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public int BookId { get; set; }
    public Book Book { get; set; }
    public DateTime BookingDate { get; set; }
    public DateTime ReturnDate { get; set; }
}

public record BookingDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int BookId { get; set; }
    public DateTime BookingDate { get; set; }
    public DateTime ReturnDate { get; set; }
}