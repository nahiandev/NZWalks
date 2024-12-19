using Microsoft.AspNetCore.Mvc;
using NZWalks.Models.Domain;
using NZWalks.Models.DTO;
using NZWalks.Repository.Interfaces;

namespace NZWalks.Controllers
{
    [Route("endpoints/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository _repository;
        private readonly ILogger<ImagesController> _logger;
        private readonly IConfiguration _configuration;

        public ImagesController(IImageRepository repository, ILogger<ImagesController> logger, IConfiguration configuration)
        {
            _repository = repository;
            _logger = logger;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadDTO image_to_upload)
        {
            _logger.LogInformation("Starting Upload method");

            if (!UploadValidated(image_to_upload))
            {
                return BadRequest(ModelState);
            }

            var converted_domain_image = new Image()
            {
                File = image_to_upload.File,
                Extension = Path.GetExtension(image_to_upload.File.FileName),
                SizeInBytes = image_to_upload.File.Length,
                Name = image_to_upload.Name,
                Description = image_to_upload.Description
            };

            await _repository.UploadAsync(converted_domain_image);

            _logger.LogInformation("Ending Upload method");

            return Ok(converted_domain_image);
        }

        [HttpDelete]
        [Route("remove")]
        public async Task<IActionResult> Remove([FromForm] string name)
        {
            _logger.LogInformation("Starting Remove method");

            var removed_image = await _repository.RemoveAsync(name);

            if (removed_image is null)
            {
                Response.Headers.Append("X-Custom-Message", "Image not found");
                _logger.LogWarning("Image not found: {Name}", name);
                return NotFound("Image not found");
            }

            Response.Headers.Append("X-Custom-Message", "Image successfully removed");
            _logger.LogInformation("Image successfully removed: {Name}", name);
            return NoContent();
        }

        private bool UploadValidated(ImageUploadDTO image_to_upload)
        {
            var max_size = _configuration.GetValue<long>("ImageSettings:MaxSize", 10485760);
            var valid_extensions = _configuration.GetSection("ImageSettings:ValidExtensions").Get<List<string>>() ?? [".jpg", ".png", ".jpeg"];

            var incoming_file_extension = Path.GetExtension(image_to_upload.File.FileName);

            if (!valid_extensions.Contains(incoming_file_extension.ToLower()))
            {
                ModelState.AddModelError("File", "Invalid file type");
                return false;
            }

            if (image_to_upload.File.Length > max_size)
            {
                ModelState.AddModelError("File", "File size is too large");
                return false;
            }

            return true;
        }
    }
}
