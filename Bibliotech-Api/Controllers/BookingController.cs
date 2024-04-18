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
        return Ok(context.booking);
    }

    [HttpPost]
    public IActionResult PostBooking([FromBody] Booking booking)
    {
        context.booking.Add(booking);
        context.SaveChanges();
        
        return Ok(booking.booking_id);
    }
}