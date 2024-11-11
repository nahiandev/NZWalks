using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IMapper _mapper;

        public RegionsController(IRegionRepository regions, IMapper mapper)
        {
            _regions = regions;
            _mapper = mapper;
        }

        [HttpGet]

        public async Task<IActionResult> GetAll()
        {
            var domain_regions = await _regions.GetAllAsync();
            List<RegionDTO> dto_regions = _mapper.Map<List<RegionDTO>>(domain_regions);
            return Ok(dto_regions);
        }
        /*
            GET: api/Regions/id
            Concrete Implementation goes here
        */
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var domain_region = await _regions.GetByIdAsync(id);
            if (domain_region is null) return NotFound();

            RegionDTO dto_region = _mapper.Map<RegionDTO>(domain_region);
            return Ok(dto_region);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionDTO incoming_region)
        {
            var is_valid = ModelState.IsValid;

            if (is_valid)
            {
                var converted_domain_region = _mapper.Map<Region>(incoming_region);
                var new_domain_region = await _regions.CreateAsync(converted_domain_region);

                return CreatedAtAction(nameof(GetById),
                    new { id = new_domain_region.Id },
                    _mapper.Map<RegionDTO>(new_domain_region));
            }

            return BadRequest(ModelState);
        }
        
        [HttpDelete]
        [Route("deleteall")]
        public async Task<IActionResult> BulkDelete()
        {
            await _regions.BulkDeleteAsync();
            return NoContent();
        }

        [HttpGet]
        [Route("count")]
        public async Task<IActionResult> Count()
        {
            return Ok(await _regions.CountAsync());
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update(Guid id, UpdateRegionDTO new_region)
        {
            var is_valid = ModelState.IsValid;

            if (is_valid)
            {
                var temp_domain = _mapper.Map<Region>(new_region);
                var updated_region = await _regions.UpdateAsync(id, temp_domain);
                if (updated_region is null) return BadRequest();
                return Ok(updated_region);
            }

            return BadRequest(ModelState);
        }


        [HttpDelete]

        [Route("{id:Guid}")]

        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var domain_regions = await _regions.DeleteAsync(id);
            if (domain_regions is null) return BadRequest();
            return NoContent();
        }
    }
}
