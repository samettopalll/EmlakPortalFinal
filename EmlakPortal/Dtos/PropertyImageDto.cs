namespace EmlakPortal.Dtos
{
    public class PropertyImageDto
    {
        public PropertyDto Property { get; set; }
        public List<IFormFile> Images { get; set; }
    }
}
