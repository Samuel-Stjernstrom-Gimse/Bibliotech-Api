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
            .Include(bookings => bookings.user)
            .Include(bookings => bookings.book)
        );
    }

    [HttpGet]
    [Route("id:int")]
    public IActionResult GetBookingById(int id)
    {
        return Ok(context.bookings
            .Include(bookings => bookings.user)
            .Include(bookings => bookings.book)
            .FirstOrDefault(bookings => bookings.booking_id == id)
        );
    }

    [HttpPost]
    public IActionResult PostBooking([FromBody] Bookings bookings)
    {
        context.bookings.Add(bookings);
        context.SaveChanges();

        return Ok(bookings.booking_id);
    }
    
}