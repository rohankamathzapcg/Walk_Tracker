namespace Backend.Models
{
    public class Walks
    {
        public int Id { get; set; }
        public string Name { get; set; }   
        public string Description { get; set; }
        public double Distance { get; set; }
        public string? ImageURL { get; set; }
        public int DifficultyId { get; set; }
        public int RegionId { get; set; }

        //Navigation Properties
        public Difficulty difficulty { get; set; }
        public Region region { get; set; }
    }
}
