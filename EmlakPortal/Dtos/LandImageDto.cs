namespace EmlakPortal.Dtos
{
    public class LandImageDto
    {
        public LandDto Land { get; set; }
        public List<IFormFile> Images { get; set; }
    }
}
