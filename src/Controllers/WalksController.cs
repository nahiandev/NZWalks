﻿using AutoMapper;
using NZWalks.Models.Domain;
using NZWalks.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using NZWalks.Repository.Interfaces;

namespace NZWalks.Controllers
{
    [Route("endpoints/[controller]")]
    [ApiController]
    
    public class WalksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWalkRepository _walks;

        public WalksController(IMapper mapper, IWalkRepository walks)
        {
            _mapper = mapper;
            _walks = walks;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filter_property = null, 
            [FromQuery] string? query = null, [FromQuery] string? order_by = null, 
            [FromQuery] bool? is_ascending = true, [FromQuery] int page_number = 1, 
            [FromQuery] int page_size = 1000)
        {
            List<Walk>? walks = await _walks.GetAllAsync(filter_property, 
                query, order_by, is_ascending ?? true, 
                page_number, page_size);

            if (walks is null) return BadRequest("Invalid filter property or missing query for the specified filter.");

            if (walks.Count == 0) return NotFound("No walks found matching the specified criteria.");

            var mapped_walks = _mapper.Map<List<WalkDTO>>(walks);

            return Ok(mapped_walks);
        }

        [HttpGet]
        [Route("id:Guid")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var walk = await _walks.GetByIdAsync(id);
            if (walk is null) return NotFound();

            var mapped_walk = _mapper.Map<WalkDTO>(walk);

            return Ok(mapped_walk);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkDTO walk)
        {
            var dto_to_domain_walk = _mapper.Map<Walk>(walk);

            var saved_walk = await _walks.CreateAsync(dto_to_domain_walk);

            var mapped_walk = _mapper.Map<WalkDTO>(saved_walk);

            return Ok(mapped_walk);
        }

        [HttpPut]
        [Route("id:Guid")]
        public async Task<IActionResult> Update(Guid id, UpdateWalkDTO new_walk)
        {
            var converted_domain_walk = _mapper.Map<Walk>(new_walk);

            var updated_domain_walk = await _walks.UpdateAsync(id, converted_domain_walk);

            if (updated_domain_walk is null) BadRequest();

            var mapped_walk = _mapper.Map<WalkDTO>(updated_domain_walk);

            return Ok(mapped_walk);
        }


        [HttpDelete]
        [Route("id:Guid")]

        public async Task<IActionResult> Remove(Guid id)
        {
            var deleted_response = await _walks.RemoveAsync(id);

            if (deleted_response is null) return BadRequest();

            return NoContent();
        }
    }
}
