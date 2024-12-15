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

        public ImagesController(IImageRepository repository, ILogger<ImagesController> logger)
        {
            _repository = repository;
            _logger = logger;
        }


        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadDTO image_to_upload)
        {
            _logger.LogInformation("This is the starting of the Upload method");
         
            var validated = UploadValidated(image_to_upload);

            if (!validated) return BadRequest(ModelState);

            var converted_domain_image = new Image()
            {
                File = image_to_upload.File,
                Extension = Path.GetExtension(image_to_upload.File.FileName),
                SizeInBytes = image_to_upload.File.Length,
                Name = image_to_upload.Name,
                Description = image_to_upload.Description
            };
            
            await _repository.UploadAsync(converted_domain_image);

            _logger.LogInformation("This is the end of the Upload method");

            return Ok(converted_domain_image);
        }


        [HttpDelete]
        //[Route("{id}")]
        public async Task<IActionResult> Remove([FromForm] string name)
        {
            var removed_image = await _repository.RemoveAsync(name);

            if (removed_image is null)
            {
                Response.Headers.Append("X-Custom-Message", "Image not found");
                return BadRequest("Image not found");
            }

            Response.Headers.Append("X-Custom-Message", "Image successfully removed"); 
            return NoContent();
        }

        private bool UploadValidated(ImageUploadDTO image_to_upload, long max_size = 10485760)
        {
            List<string> valid_extensions = [".jpg", ".png", ".jpeg"];

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
