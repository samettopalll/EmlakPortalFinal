namespace EmlakPortal.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public bool IsActive { get; set; }
        public List<Property> Properties { get; set; }
        public List<Land> Lands { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
