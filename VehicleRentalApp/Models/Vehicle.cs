namespace VehicleRentalApp.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string LicensePlate { get; set; }
        public decimal PricePerDay { get; set; }
        public bool IsAvailable { get; set; }
        public string OwnerId { get; set; }
    }
}
