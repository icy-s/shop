using Microsoft.AspNetCore.Http;
using shop.Core.Dto;

namespace shop.Core.Domain
{
    public class RealEstate
    {
        public Guid? Id { get; set; }
        public double? Area { get; set; }
        public string? Location { get; set; }
        public int? RoomNumber { get; set; }
        public string? BuildingType { get; set; }
        public IEnumerable<FileToDatabaseDto> Image { get; set; }
        = new List<FileToDatabaseDto>();
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
