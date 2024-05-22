using EmlakPortal.Models;

namespace EmlakPortal.Dtos
{
    public class ImageDto
    {
        public int Id { get; set; }
        public string PhotoUrl { get; set; }
        public int? PropertyID { get; set; }
        public int? LandID { get; set; }
    }
}
