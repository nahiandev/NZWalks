using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using NZWalks.Data;
using NZWalks.Models.Domain;
using NZWalks.Models.DTO;
using NZWalks.Repository;


namespace NZWalks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        
        private readonly IRegionRepository _regions;

        public RegionsController(IRegionRepository regions)
        {
           _regions = regions;
        }

        [HttpGet]
        [EnableRateLimiting("fixed")]
        public async Task<IActionResult> GetAll()
        {
            var domain_regions = await _regions.GetAllAsync();

            List<RegionDTO> dto_regions = [];

            foreach (var region in domain_regions)
            {
                dto_regions.Add(new()
                {
                    Code = region.Code,
                    Name = region.Name,
                    RegionImageUrl = region.RegionImageUrl
                });
            }

            return Ok(dto_regions);
        }

        [HttpGet]
        [EnableRateLimiting("fixed")]
        [Route("{id:Guid}")]

        public async Task<IActionResult> GetById(Guid id)
        {
            var domain_region = await _regions.GetByIdAsync(id);

            if (domain_region is null) return NotFound();

            RegionDTO dto_region = new()
            {
                Code = domain_region.Code,
                Name = domain_region.Name,
                RegionImageUrl = domain_region.RegionImageUrl
            };

            return Ok(dto_region);
        }

        [HttpPost]
        [EnableRateLimiting("fixed")]
        

        public async Task<IActionResult> Create([FromBody] AddRegionDTO incoming_region)
        {
            var domain_region = await _regions.CreateAsync(incoming_region);
            return CreatedAtAction(nameof(GetById), new { id = domain_region.Id }, new RegionDTO { Code = domain_region.Code, Name = domain_region.Name, RegionImageUrl = domain_region.RegionImageUrl });
        }

        [HttpPut]
        [EnableRateLimiting("fixed")]
        [Route("{id:Guid}")]

        public async Task<IActionResult> Update(Guid id, UpdateRegionDTO new_region)
        {
            var updated_region = await _regions.UpdateAsync(id, new_region);
            if (updated_region is null) return BadRequest();
            return Ok(updated_region);
        }


        [HttpDelete]
        [EnableRateLimiting("fixed")]
        [Route("{id:Guid}")]

        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var domain_regions = await _regions.DeleteAsync(id);
            
            
            if (domain_regions is null) return BadRequest();
            return NoContent();
        }


        
    }
}
