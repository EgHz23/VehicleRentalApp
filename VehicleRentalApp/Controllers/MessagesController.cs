using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleRentalApp.Data;
using VehicleRentalApp.Models;

namespace VehicleRentalApp.Controllers
{
    [Authorize]
    public class MessagesController : Controller
    {
        private readonly VehicleRentalContext _context;

        public MessagesController(VehicleRentalContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Send(int VehicleId, string OwnerId, string Content)
        {
            // Preverimo, če je uporabnik prijavljen
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized("You need to be logged in to send messages.");
            }

            if (string.IsNullOrWhiteSpace(Content))
            {
                TempData["Error"] = "Message content cannot be empty.";
                return RedirectToAction("Details", "Vehicles", new { id = VehicleId });
            }

            try
            {
                var message = new Message
                {
                    SenderId = User.Identity.Name, // Assuming the user is logged in
                    ReceiverId = OwnerId,
                    Content = Content.Trim(),
                    SentAt = DateTime.Now
                };

                _context.Messages.Add(message);
                await _context.SaveChangesAsync();

                TempData["Message"] = "Message sent successfully!";
                return RedirectToAction("Details", "Vehicles", new { id = VehicleId });
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while sending the message. Please try again.";
                // Log the exception (optional)
                Console.WriteLine($"Error: {ex.Message}");
                return RedirectToAction("Details", "Vehicles", new { id = VehicleId });
            }
        }
    }
}
