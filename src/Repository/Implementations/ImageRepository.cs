using NZWalks.Data;
using NZWalks.Repository.Interfaces;
using NZWalks.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace NZWalks.Repository.Implementations
{
    public class ImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IHttpContextAccessor _accessor;
        private readonly NZWalksRecordsDbContext _context;

        public ImageRepository(IWebHostEnvironment environment,
            IHttpContextAccessor accessor, NZWalksRecordsDbContext context)
        {
            _environment = environment;
            _accessor = accessor;
            _context = context;
        }

        public async Task<Image> RemoveAsync(string name)
        {
            var image_to_delete = await _context.Images.FirstOrDefaultAsync(img => img.Name == name);

            if (image_to_delete is null) return Task.FromResult<Image>(null!).Result;

            var local_file_path = Path.Combine(_environment.ContentRootPath, "StaticFiles", $"{image_to_delete.Name}{image_to_delete.Extension}");

            DeleteFile(local_file_path);

            _context.Images.Remove(image_to_delete);

            await _context.SaveChangesAsync();

            return image_to_delete;
        }

        public async Task<Image> UploadAsync(Image image)
        {
            var local_file_path = Path.Combine(_environment.ContentRootPath, "StaticFiles", $"{image.Name}{image.Extension.ToLower()}");

            await SaveFileAsync(image.File, local_file_path);

            var file_url = $"{_accessor.HttpContext!.Request.Scheme}://{_accessor.HttpContext.Request.Host}{_accessor.HttpContext.Request.PathBase}/images/{image.Name}{image.Extension.ToLower()}";

            image.PathToFile = file_url;

            _context.Images.Add(image);

            await _context.SaveChangesAsync();

            return image;
        }

        private void DeleteFile(string local_file_path)
        {
            if (File.Exists(local_file_path)) File.Delete(local_file_path);
        }
        private async Task SaveFileAsync(IFormFile file, string local_file_path)
        {
            using FileStream stream = new(local_file_path, FileMode.Create);
            await file.CopyToAsync(stream);
        }
    }
}
