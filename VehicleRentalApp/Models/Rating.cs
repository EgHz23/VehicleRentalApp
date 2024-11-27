namespace VehicleRentalApp.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int VehicleId { get; set; }
        public string Comment { get; set; }
        public int Stars { get; set; }
    }
}
