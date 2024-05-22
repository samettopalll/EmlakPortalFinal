namespace EmlakPortal.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string PhotoUrl { get; set; }
        public int? PropertyID { get; set; }
        public Property Property { get; set; } 
        public int? LandID { get; set; }
        public Land Land { get; set; }
    }
}
