using Microsoft.AspNetCore.Mvc;

namespace shop.Models.RealEstate
{
    public class RealEstateIndexViewModel : Controller
    {
        public Guid? Id { get; set; }
        public double? Area { get; set; }
        public string? Location { get; set; }
        public int? RoomNumber { get; set; }
        public string? BuildingType { get; set; }
    }
}
