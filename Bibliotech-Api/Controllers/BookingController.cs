using Bibliotech_Api.Access;
using Bibliotech_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bibliotech_Api.Controllers;

[ApiController]
[Route("booking")]
public class BookingController(LocalDbContext context) : Controller
{
    
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(context.bookings
            .Include(booking => booking.Book)
            .Include(booking => booking.User)
        );
    }
    
    [HttpPost]
    public IActionResult Post([FromBody] BookingDto booking)
    {
        var user = context.users.FirstOrDefault(u => u.Id == booking.UserId);
        var book = context.books.FirstOrDefault(b => b.Id == booking.BookId);
        
        if (user == null || book == null)
        {
            return NotFound();
        }
        
        var newBooking = new Booking
        {
            UserId = booking.UserId,
            User = user,
            BookId = booking.BookId,
            Book = book,
            BookingDate = booking.BookingDate,
            ReturnDate = booking.ReturnDate
        };
        
        context.bookings.Add(newBooking);
        context.SaveChanges();
        
        return Ok(newBooking);
    }
    
    [HttpPatch]
    public IActionResult Patch([FromBody] BookingDto booking)
    {
        var existingBooking = context.bookings.FirstOrDefault(b => b.Id == booking.Id);
        
        if (existingBooking == null)
        {
            return NotFound("Booking not found");
        }
        
        existingBooking.BookingDate = booking.BookingDate;
        existingBooking.ReturnDate = booking.ReturnDate;

        context.SaveChanges();
        
        context.Entry(existingBooking)
            .Reference(b => b.Book)
            .Load();

        context.Entry(existingBooking)
            .Reference(b => b.User)
            .Load();

        return Ok(existingBooking);
    } 
}