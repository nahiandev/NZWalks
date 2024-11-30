﻿using NZWalks.Data;
using NZWalks.Repository.Interfaces;
using NZWalks.Models.Domain;

namespace NZWalks.Repository.Implementations
{
    public class ImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IHttpContextAccessor _accessor;
        private readonly NZWalksRecordsDbContext _context;

        public ImageRepository(IWebHostEnvironment environment, IHttpContextAccessor accessor, NZWalksRecordsDbContext context)
        {
            _environment = environment;
            _accessor = accessor;
            _context = context;
        }
        public async Task<Image> UploadAsync(Image image)
        {
            var local_file_path = Path.Combine(_environment.ContentRootPath, "Images", $"{image.Name}{image.Extension}");

            //using FileStream stream = new(local_file_path, FileMode.Create);
            //await image.File.CopyToAsync(stream);

            await SaveFileAsync(image.File, local_file_path);


            var file_url = $"{_accessor.HttpContext!.Request.Scheme}://{_accessor.HttpContext.Request.Host}{_accessor.HttpContext.Request.PathBase}/Images/{image.Name}{image.Extension}";

            image.PathToFile = file_url;

            _context.Images.Add(image);
            await _context.SaveChangesAsync();

            return image;
        }

        private async Task SaveFileAsync(IFormFile file, string local_file_path)
        {
            using FileStream stream = new(local_file_path, FileMode.Create);
            await file.CopyToAsync(stream);
        }
    }
}