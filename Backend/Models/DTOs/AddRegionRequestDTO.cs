namespace Backend.Models.DTOs
{
    // This DTO get the properties from the client
    public class AddRegionRequestDTO
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string? ImageURL { get; set; }
    }
}
