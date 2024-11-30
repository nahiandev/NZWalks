using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.Models.Domain;
using NZWalks.Models.DTO;

namespace NZWalks.Controllers
{
    [Route("endpoints/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadDTO image_to_upload)
        {
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
            

            return Ok();
        }

        private bool UploadValidated(ImageUploadDTO image_to_upload)
        {
            List<string> valid_extensions = [".jpg", ".png", ".jpeg"];

            var incoming_file_extension = Path.GetExtension(image_to_upload.File.FileName);

            if (!valid_extensions.Contains(incoming_file_extension))
            {
                ModelState.AddModelError("File", "Invalid file type");
                return false;
            }

            if (image_to_upload.File.Length > 1048576)
            {
                ModelState.AddModelError("File", "File size is too large");
                return false;
            }

            return true;
        }
    }
}
