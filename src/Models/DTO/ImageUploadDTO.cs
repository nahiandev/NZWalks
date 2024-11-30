using System.ComponentModel.DataAnnotations;

namespace NZWalks.Models.DTO
{
    public class ImageUploadDTO
    {
        [Required]
        public IFormFile File { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
