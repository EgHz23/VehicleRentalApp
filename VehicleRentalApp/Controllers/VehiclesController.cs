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
    vehicle.IsAvailable = true; // Vehicle is available by default
    var ownerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
    Console.WriteLine($"OwnerId Retrieved: {ownerId}");

    if (string.IsNullOrEmpty(ownerId))
    {
        TempData["Error"] = "Unable to determine the owner of this vehicle. Please log in again.";
        return RedirectToAction("Index", "Home");
    }

    vehicle.OwnerId = ownerId;
    Console.WriteLine($"Vehicle OwnerId Assigned: {vehicle.OwnerId}");

    // Remove validation error for OwnerId
    ModelState.Remove(nameof(vehicle.OwnerId));

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
            Console.WriteLine($"Vehicle being added: {vehicle.Brand}, OwnerId: {vehicle.OwnerId}");
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

        foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
        {
            Console.WriteLine($"Validation Error: {error.ErrorMessage}");
        }
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
        [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Delete(int id)
{
    var ownerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
    if (string.IsNullOrEmpty(ownerId))
    {
        TempData["Error"] = "Unauthorized action.";
        return RedirectToAction(nameof(MyVehicles));
    }

    var vehicle = await _context.Vehicles.FirstOrDefaultAsync(v => v.Id == id && v.OwnerId == ownerId);

    if (vehicle == null)
    {
        TempData["Error"] = "Vehicle not found or you do not have permission to delete it.";
        return RedirectToAction(nameof(MyVehicles));
    }

    try
    {
        _context.Vehicles.Remove(vehicle);
        await _context.SaveChangesAsync();
        TempData["Message"] = "Vehicle deleted successfully.";
    }
    catch (Exception ex)
    {
        TempData["Error"] = "An error occurred while deleting the vehicle.";
        Console.WriteLine($"Error: {ex.Message}");
    }

    return RedirectToAction(nameof(MyVehicles));
}
[HttpGet]
public async Task<IActionResult> Edit(int id)
{
    var ownerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
    if (string.IsNullOrEmpty(ownerId))
    {
        return Unauthorized("You must be logged in to edit a vehicle.");
    }

    var vehicle = await _context.Vehicles.FirstOrDefaultAsync(v => v.Id == id && v.OwnerId == ownerId);

    if (vehicle == null)
    {
        TempData["Error"] = "Vehicle not found or you do not have permission to edit it.";
        return RedirectToAction(nameof(MyVehicles));
    }

    return View(vehicle);
}

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Edit(int id, Vehicle vehicle, IFormFile? image)
{
    var ownerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
    if (string.IsNullOrEmpty(ownerId))
    {
        return Unauthorized("You must be logged in to edit a vehicle.");
    }

    var existingVehicle = await _context.Vehicles.FirstOrDefaultAsync(v => v.Id == id && v.OwnerId == ownerId);

    if (existingVehicle == null)
    {
        TempData["Error"] = "Vehicle not found or you do not have permission to edit it.";
        return RedirectToAction(nameof(MyVehicles));
    }

    if (ModelState.IsValid)
    {
        try
        {
            // Update vehicle details
            existingVehicle.Brand = vehicle.Brand;
            existingVehicle.Model = vehicle.Model;
            existingVehicle.Year = vehicle.Year;
            existingVehicle.LicensePlate = vehicle.LicensePlate;
            existingVehicle.PricePerDay = vehicle.PricePerDay;
            existingVehicle.IsAvailable = vehicle.IsAvailable;

            // Update the image if a new one is uploaded
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

                existingVehicle.ImagePath = $"/images/vehicles/{fileName}";
            }

            _context.Vehicles.Update(existingVehicle);
            await _context.SaveChangesAsync();

            TempData["Message"] = "Vehicle updated successfully!";
            return RedirectToAction(nameof(MyVehicles));
        }
        catch (Exception ex)
        {
            TempData["Error"] = "An error occurred while updating the vehicle.";
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    TempData["Error"] = "Please correct the errors in the form and try again.";
    return View(vehicle);
}

    }
    
}
