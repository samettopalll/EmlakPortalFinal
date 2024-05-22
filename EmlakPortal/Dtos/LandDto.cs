using System.ComponentModel;

namespace EmlakPortal.Dtos
{
    public class LandDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public double Price { get; set; }
        public double SquareMeters { get; set; }
        public string Location { get; set; }
        public int BlockNumber { get; set; }
        public int ParcelNumber { get; set; }

        [DefaultValue(false)]
        public bool IsSold { get; set; }
        public int CategoryID { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public string AppUserId { get; set; }
    }
}
