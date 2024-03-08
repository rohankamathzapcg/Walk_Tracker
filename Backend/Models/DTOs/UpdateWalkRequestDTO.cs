namespace Backend.Models.DTOs
{
    public class UpdateWalkRequestDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Distance { get; set; }
        public string? ImageURL { get; set; }
        public int DifficultyId { get; set; }
        public int RegionId { get; set; }
    }
}
