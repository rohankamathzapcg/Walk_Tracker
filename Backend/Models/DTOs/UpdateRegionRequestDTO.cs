namespace Backend.Models.DTOs
{
    public class UpdateRegionRequestDTO
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string? ImageURL { get; set; }
    }
}
