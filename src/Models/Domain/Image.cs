using System.ComponentModel.DataAnnotations.Schema;

namespace NZWalks.Models.Domain
{
    public class Image
    {
        public Guid Id { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }
        public string? Description { get; set; }
        public string Extension { get; set; }
        public long SizeInBytes { get; set; }
        public string PathToFile { get; set; }
    }
}
