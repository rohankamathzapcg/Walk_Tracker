using System.ComponentModel.DataAnnotations;

namespace Backend.Models.DTOs
{
    // This DTO get the properties from the client
    public class AddRegionRequestDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [MinLength(4, ErrorMessage = "Code has to be Minimum of 4 Characters")]
        [MaxLength(4, ErrorMessage = "Code has to be Maximum of 4 Characters")]
        public string Code { get; set; }
        public string? ImageURL { get; set; }
    }
}
