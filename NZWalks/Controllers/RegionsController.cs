﻿using Microsoft.AspNetCore.Mvc;
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
        private readonly NZWalksDbContext _context;
        private readonly IRegionRepository _regions;

        public RegionsController(NZWalksDbContext context, IRegionRepository regions)
        {
            _context = context;
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
        

        public IActionResult Create([FromBody] AddRegionDTO incoming_region)
        {
            Region domain_region = new()
            {
                Code = incoming_region.Code,
                Name = incoming_region.Name,
                RegionImageUrl = incoming_region.RegionImageUrl
            };

            _context.Regions.Add(domain_region);
            _context.SaveChanges();


            RegionDTO returned_region = new()
            { 
                
                Code = domain_region.Code, 
                Name = domain_region.Code, 
                RegionImageUrl = domain_region.RegionImageUrl 
            };

            return CreatedAtAction(nameof(GetById), new { id = domain_region.Id }, returned_region);
        }

        [HttpPut]
        [EnableRateLimiting("fixed")]
        [Route("{id:Guid}")]

        public IActionResult Update(Guid id, UpdateRegionDTO new_region)
        {
            var domain_region = _context.Regions.FirstOrDefault(x => x.Id == id);

            if (domain_region is null) return NotFound();

            domain_region.Code = new_region.Code;
            domain_region.Name = new_region.Name;
            domain_region.RegionImageUrl = new_region.RegionImageUrl;

            _context.SaveChanges();

            var updated_region = new RegionDTO()
            {
                Code = domain_region.Code,
                Name = domain_region.Name,
                RegionImageUrl = domain_region.RegionImageUrl
            };

            return Ok(updated_region);
        }


        [HttpDelete]
        [EnableRateLimiting("fixed")]
        [Route("{id:Guid}")]

        public IActionResult Delete([FromRoute] Guid id)
        {
            var domain_regions = _context.Regions;
            var region_to_delete = domain_regions.FirstOrDefault(y => y.Id == id);

            if (region_to_delete is null) return BadRequest();

            domain_regions.Remove(region_to_delete);
            _context.SaveChanges();

            return Ok(region_to_delete);
        }
    }
}
