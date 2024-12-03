namespace VehicleRentalApp.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Rating
{
    public int Id { get; set; }

    [Required]
    public int VehicleId { get; set; }

    [Required]
    public string UserId { get; set; }

    [Required]
    [Range(1, 5)]
    public int Stars { get; set; }

    [StringLength(500)]
    public string? Comment { get; set; }

    public Vehicle Vehicle { get; set; }
}

}
