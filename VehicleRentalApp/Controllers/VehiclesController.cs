using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleRentalApp.Data;
using VehicleRentalApp.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims; // For ClaimTypes

namespace VehicleRentalApp.Controllers
{
    [Authorize] // Restrict to authenticated users by default
    public class VehiclesController : Controller
    {
        private readonly VehicleRentalContext _context;

        public VehiclesController(VehicleRentalContext context)
        {
            _context = context;
        }

        // Add a new vehicle
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Vehicle vehicle, IFormFile? image)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized("You must be logged in to add a vehicle.");
            }

            var ownerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(ownerId))
            {
                TempData["Error"] = "Unable to determine the owner of this vehicle.";
                return View(vehicle);
            }

            vehicle.OwnerId = ownerId; 
            vehicle.IsAvailable = true; // Vehicle is available by default

            if (ModelState.IsValid)
            {
                try
                {
                    // Process image upload
                    if (image != null && image.Length > 0)
                    {
                        var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/vehicles");
                        Directory.CreateDirectory(uploadsPath);

                        var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(image.FileName)}";
                        var filePath = Path.Combine(uploadsPath, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await image.CopyToAsync(stream);
                        }

                        vehicle.ImagePath = $"/images/vehicles/{fileName}";
                    }

                    _context.Vehicles.Add(vehicle);
                    await _context.SaveChangesAsync();

                    TempData["Message"] = "Vehicle added successfully!";
                    return RedirectToAction(nameof(MyVehicles));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "An error occurred while adding the vehicle.";
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            else
            {
                TempData["Error"] = "Please correct the errors in the form and try again.";
            }

            return View(vehicle);
        }

        // View all vehicles owned by the current user
        public async Task<IActionResult> MyVehicles()
        {
            var ownerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(ownerId))
            {
                return Unauthorized("You must be logged in to view your vehicles.");
            }

            var myVehicles = await _context.Vehicles
                .Where(v => v.OwnerId == ownerId)
                .ToListAsync();

            return View(myVehicles);
        }

        // View all available vehicles
        [AllowAnonymous] // Allow access to everyone
        public async Task<IActionResult> Index()
        {
            var availableVehicles = await _context.Vehicles
                .Where(v => v.IsAvailable)
                .ToListAsync();

            availableVehicles ??= new List<Vehicle>();

            return View(availableVehicles);
        }

        // View details of a specific vehicle
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles
                .FirstOrDefaultAsync(v => v.Id == id);

            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }
    }
}
