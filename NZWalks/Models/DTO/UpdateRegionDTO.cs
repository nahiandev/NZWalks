using System.ComponentModel.DataAnnotations;

namespace NZWalks.Models.DTO
{
    public class UpdateRegionDTO
    {

        [Required]
        [MinLength(3, ErrorMessage = "No less than 3 characters allowed")]
        [MaxLength(3, ErrorMessage = "No more than 3 characters allowed")]
        public string Code { get; set; }
        [Required]
        [MinLength(1, ErrorMessage = "Enter al least 1 character")]
        [MaxLength(50, ErrorMessage = "No more than 50 characters allowed")]
        public required string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
