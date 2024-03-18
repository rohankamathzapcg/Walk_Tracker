using System.ComponentModel.DataAnnotations;

namespace Backend.Models.DTOs
{
    public class AddDifficultyRequestDTO
    {
        [Required]
        public string Name { get; set; }
    }
}
