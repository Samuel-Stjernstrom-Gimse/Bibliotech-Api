using Bibliotech_Api.Access;
using Bibliotech_Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bibliotech_Api.Controllers;
[ApiController]
[Route("booking")]
public class BookingController(LocalDbContext context) : Controller
{
    
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(context.bookings);
    }

    [HttpPost]
    public IActionResult PostBooking([FromBody] Bookings bookings)
    {
        context.bookings.Add(bookings);
        context.SaveChanges();
        
        return Ok(bookings.booking_id);
    }
}